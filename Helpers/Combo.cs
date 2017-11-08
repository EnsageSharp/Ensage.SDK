﻿// <copyright file="Combo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Abilities;
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Abilities.npc_dota_hero_zuus;
    using Ensage.SDK.Extensions;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    [PublicAPI]
    public class Combo
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<BaseAbility> abilities;

        private readonly List<ActiveAbility> activeAbilities;

        [CanBeNull]
        private readonly zuus_static_field staticField;

        public Combo(params BaseAbility[] abilities)
        {
            this.abilities = abilities.Where(x => x != null).ToList();

            // special dmg abilities
            this.staticField = this.abilities.FirstOrDefault(x => x.Ability.Id == AbilityId.zuus_static_field) as zuus_static_field;
            if (this.staticField != null)
            {
                this.abilities.Remove(this.staticField);
            }

            this.activeAbilities = this.abilities.OfType<ActiveAbility>().ToList();
        }

        [NotNull]
        public IReadOnlyCollection<BaseAbility> Abilities
        {
            get
            {
                return this.abilities.AsReadOnly();
            }
        }

        /// <summary>
        ///     Gets or sets the minimum delay between actions (if ping is below).
        /// </summary>
        public int MinimumPingDelay { get; set; } = 50;

        /// <summary>
        ///     Gets or sets a value indicating whether the user input is blocked while <see cref="Execute" /> is executed.
        /// </summary>
        public bool UserInputDisabled { get; set; } = false;

        /// <summary>
        ///     Executes the combo.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="token"></param>
        /// <returns>Returns "true" when the full combo was executed, or "false" when it was canceled.</returns>
        public async Task<bool> Execute([NotNull] Unit target, CancellationToken token = default(CancellationToken))
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (!this.activeAbilities.Any())
            {
                return true;
            }

            var userInputDisabled = this.UserInputDisabled;
            try
            {
                if (userInputDisabled)
                {
                    Player.OnExecuteOrder += this.OnExecuteOrder;
                }

                foreach (var ability in this.activeAbilities)
                {
                    if (ability.CanBeCasted && ability.CanHit(target) && ability.UseAbility(target))
                    {
                        int delay;

                        // wait for projectile to hit, so that the amplifier is actually applied for the rest of the combo
                        if (ability is IHasDamageAmplifier && ability.Speed != float.MaxValue)
                        {
                            delay = ability.GetHitTime(target);
                        }
                        else
                        {
                            delay = ability.GetCastDelay(target);
                        }

                        var diff = (int)(this.MinimumPingDelay - Game.Ping);
                        if (diff > 0)
                        {
                            delay += diff;
                        }

                        await Task.Delay(delay, token);
                    }
                }

                return true;
            }
            catch (OperationCanceledException)
            {
            }
            catch (EntityNotFoundException)
            {
            }
            finally
            {
                if (userInputDisabled)
                {
                    Player.OnExecuteOrder -= this.OnExecuteOrder;
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns the damage of the combo.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isReady">Only includes abilities of the combo which is ready.</param>
        /// <param name="canHit">Only includes abilities of the combo which can hit.</param>
        /// <returns></returns>
        public float GetDamage([NotNull] Unit target, bool isReady = true, bool canHit = true)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var damage = 0.0f;
            var physicalDmgModifier = 1f;
            var magicDmgModifier = 1f;
            var pureDmgModifier = 1f;
            var health = (float)target.Health;
            foreach (var ability in this.abilities)
            {
                if (isReady && !ability.IsReady)
                {
                    continue;
                }

                var active = ability as ActiveAbility;
                if (active != null && canHit && !active.CanHit(target))
                {
                    continue;
                }

                var amplifier = ability as IHasDamageAmplifier;
                if (amplifier != null)
                {
                    // only apply amplifier extra damage if target is not already under the effect
                    var modifier = ability as IHasTargetModifier;
                    if (modifier == null || !target.HasModifier(modifier.TargetModifierName))
                    {
                        if (amplifier.AmplifierType.HasFlag(DamageType.Physical))
                        {
                            physicalDmgModifier *= 1 + amplifier.DamageAmplification;
                        }
                        if (amplifier.AmplifierType.HasFlag(DamageType.Magical))
                        {
                            magicDmgModifier *= 1 + amplifier.DamageAmplification;
                        }
                        if (amplifier.AmplifierType.HasFlag(DamageType.Pure))
                        {
                            pureDmgModifier *= 1 + amplifier.DamageAmplification;
                        }
                    }
                }

                if (this.staticField != null && this.staticField.CanBeCasted && ability.Item == null && ability is ActiveAbility)
                {
                    damage += this.staticField.GetDamage(target, magicDmgModifier - 1, Math.Max(0, health - damage));
                }

                var currentHealth = Math.Max(0, health - damage);
                if (ability.DamageType == DamageType.Physical)
                {
                    damage += ability.GetDamage(target, physicalDmgModifier - 1, currentHealth);
                }
                else if (ability.DamageType == DamageType.Magical)
                {
                    damage += ability.GetDamage(target, magicDmgModifier - 1, currentHealth);
                }
                else if (ability.DamageType == DamageType.Pure)
                {
                    damage += ability.GetDamage(target, pureDmgModifier - 1, currentHealth);
                }
                else
                {
                    damage += ability.GetDamage(target, 0, currentHealth);
                }
            }

            return damage;
        }

        /// <summary>
        ///     Returns the health of the target after executing the combo.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isReady">Only includes abilities of the combo which is ready.</param>
        /// <param name="canHit">Only includes abilities of the combo which can hit.</param>
        /// <returns></returns>
        public float GetEstimatedHealth([NotNull] Unit target, bool isReady = true, bool canHit = true)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var executionTime = this.GetExecutionTime(target, isReady, canHit) / 1000.0f;
            return Math.Min((float)target.MaximumHealth, ((float)target.Health + (executionTime * target.HealthRegeneration)) - this.GetDamage(target, isReady, canHit));
        }

        /// <summary>
        ///     Calculates the execution time of the combo.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isReady">Only includes abilities of the combo which is ready.</param>
        /// <param name="canHit">Only includes abilities of the combo which can hit.</param>
        /// <returns>Time in ms.</returns>
        public float GetExecutionTime([CanBeNull] Unit target, bool isReady = true, bool canHit = true)
        {
            var time = 0.0f;
            if (this.activeAbilities.Any())
            {
                if (target != null && target.IsVisible)
                {
                    time += this.activeAbilities.First().Owner.TurnTime(target.Position) * 1000.0f;
                }

                foreach (var ability in this.activeAbilities)
                {
                    if (isReady && !ability.IsReady)
                    {
                        continue;
                    }

                    var active = ability as ActiveAbility;
                    if (active != null && canHit && !active.CanHit(target))
                    {
                        continue;
                    }

                    time += ability.GetCastDelay();
                }
            }

            return time;
        }

        /// <summary>
        ///     Returns true when the target is in range for the whole combo.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsInRange([NotNull] Unit target)
        {
            return this.activeAbilities.All(x => x.CanHit(target));
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (args.IsPlayerInput)
            {
                args.Process = false;
            }
        }
    }
}