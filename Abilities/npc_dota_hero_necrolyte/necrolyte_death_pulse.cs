// <copyright file="slardar_sprint.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

using System.Linq;
using Ensage;
using Ensage.SDK.Abilities;
using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;
using PlaySharp.Toolkit.Helper.Annotations;

namespace wtf.Sdk.Abilities.npc_dota_hero_necrolyte
{
    public class necrolyte_death_pulse : AreaOfEffectAbility
    {
        public necrolyte_death_pulse(Ability ability)
            : base(ability)
        {
        }

        public virtual float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("area_of_effect");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < (this.CastRange + this.Radius));
        }

    }
}