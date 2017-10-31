using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.Items
{
    class item_nullifier : RangedAbility, IHasTargetModifier
    {
        public item_nullifier(Item item)
            : base(item)
        {
        }

        public override float Speed
        {
            get
            {
                return Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_nullifier_debuff"; //todo

    }
}
