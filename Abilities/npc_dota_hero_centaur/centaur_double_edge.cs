// <copyright file="centaur_double_edge.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "edge_damage");
            }
        }
    }
}