// <copyright file="juggernaut_healing_ward.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_juggernaut
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class juggernaut_healing_ward : RangedAbility, IAreaOfEffectAbility, IHasHealthRestore
    {
        public juggernaut_healing_ward(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("healing_ward_aura_radius");
            }
        }

        public float TotalHealthRestore
        {
            get
            {
                var hpPercentage = this.Owner.MaximumHealth * (this.Ability.GetAbilitySpecialData("healing_ward_heal_amount") / 100f);
                return hpPercentage * this.Duration;
            }
        }
    }
}