using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    using Ensage.SDK.Extensions;

    public  class drow_ranger_marksmanship : PassiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public drow_ranger_marksmanship(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_drow_ranger_marksmanship";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}
