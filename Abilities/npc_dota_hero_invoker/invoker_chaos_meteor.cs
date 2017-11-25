// <copyright file="invoker_chaos_meteor.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class invoker_chaos_meteor : LineAbility, IInvokableAbility, IHasDot
    {
        private readonly InvokeHelper<invoker_chaos_meteor> invokeHelper;

        public invoker_chaos_meteor(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_chaos_meteor>(this);
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("land_time");
            }
        }

        public override bool CanBeCasted
        {
            get
            {
                return base.CanBeCasted && this.invokeHelper.CanInvoke(!this.IsInvoked);
            }
        }

        public bool CanBeInvoked
        {
            get
            {
                if (this.IsInvoked)
                {
                    return true;
                }

                return this.invokeHelper.CanInvoke(false);
            }
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("area_of_effect");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("travel_distance", this.invokeHelper.Wex.Level);
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_dps");
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_exort, AbilityId.invoker_exort, AbilityId.invoker_wex };

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("travel_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_invoker_chaos_meteor_burn";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_interval");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "main_damage") * 2;
            }
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }

        public bool Invoke(List<AbilityId> currentOrbs = null)
        {
            return this.invokeHelper.Invoke(currentOrbs);
        }

        public override bool UseAbility(Unit target)
        {
            return this.Invoke() && base.UseAbility(target);
        }

        public override bool UseAbility(Vector3 position)
        {
            return this.Invoke() && base.UseAbility(position);
        }
    }
}