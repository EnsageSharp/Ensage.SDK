// <copyright file="venomancer_poison_sting.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_venomancer
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class venomancer_poison_sting : OrbAbility, IHasDot, IHasModifier
    {
        public venomancer_poison_sting(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public override bool Enabled
        {
            get
            {
                return this.Ability.Level > 0;
            }

            set
            {
            }
        }

        public bool HasInitialDamage { get; } = true;

        public string ModifierName { get; } = "modifier_venomancer_poison_sting_applier";

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_venomancer_poison_sting";

        public float TickRate { get; } = 1.0f;

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return base.GetDamage() + this.RawDamage;
            }

            var reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            var amplify = this.Ability.SpellAmplification();

            return base.GetDamage(targets) + DamageHelpers.GetSpellDamage(this.RawTickDamage, amplify, reduction);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First());
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}