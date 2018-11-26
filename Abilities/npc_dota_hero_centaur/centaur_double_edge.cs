// <copyright file="centaur_double_edge.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System.Linq;
using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities.npc_dota_hero_centaur
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class centaur_double_edge : AreaOfEffectAbility, IHasHealthCost
    {
        public centaur_double_edge(Ability ability)
            : base(ability)
        {
        }

        public float HealthCost
        {
            get
            {
                var damage = this.GetDamage(this.Owner);
                var health = this.Owner.Health;

                if (health > damage)
                {
                    return damage;
                }

                return health - 1;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("edge_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var owner = this.Owner as Hero;
            var damageFromStrength = (Ability.GetAbilitySpecialDataWithTalent(this.Owner, "strength_damage") / 100f) * owner.TotalStrength;
            var damage = this.RawDamage + damageFromStrength;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }
    }
}