// <copyright file="dark_willow_bramble_maze.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_willow
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class dark_willow_bramble_maze : CircleAbility, IHasDot
    {
        public dark_willow_bramble_maze(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("initial_creation_delay") + this.Ability.GetAbilitySpecialData("latch_creation_delay");
            }
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("latch_duration");
            }
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("placement_duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return (this.Ability.GetAbilitySpecialData("latch_damage") / this.DamageDuration) * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_dark_willow_bramble_maze";

        public float TickRate { get; } = 0.5f;

        protected override string RadiusName { get; } = "placement_range";

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}