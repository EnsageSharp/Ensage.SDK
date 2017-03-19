using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using Ensage.SDK.Extensions;

    public class enigma_black_hole : CircleAbility, IDotAbility
    {
        public enigma_black_hole(Ability ability)
            : base(ability)
        {
        }

        public override float Radius => this.Ability.GetAbilitySpecialData("pull_radius");

        public override float GetDamage(params Unit[] target)
        {
            throw new NotImplementedException();
        }

        public float GetTickDamage(params Unit[] target)
        {
            throw new NotImplementedException();
        }

        public float GetTotalDamage(params Unit[] target)
        {
            throw new NotImplementedException();
        }

        public float Duration => this.Ability.GetAbilitySpecialData("duration");

        public string ModifierName { get; } = "TODO";

        public float TickRate => this.Ability.GetAbilitySpecialData("tick_rate");

        public float TickDamage { get; }
    }
}
