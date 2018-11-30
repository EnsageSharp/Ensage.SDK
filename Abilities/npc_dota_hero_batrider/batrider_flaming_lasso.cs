// <copyright file="batrider_flaming_lasso.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System.Linq;
using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities.npc_dota_hero_batrider
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class batrider_flaming_lasso : RangedAbility, IHasTargetModifier, IHasModifier, IHasDot
    {
        public batrider_flaming_lasso(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string ModifierName { get; } = "modifier_batrider_flaming_lasso_self";

        public string TargetModifierName { get; } = "modifier_batrider_flaming_lasso";

        public float DamageDuration
        {
            get
            {
                return this.Duration;
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public float TickRate { get; } = 1.0f;

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
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}