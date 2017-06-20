// <copyright file="item_pipe.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_pipe : ActiveAbility, IAreaOfEffectAbility, IHasModifier, IAuraAbility
    {
        public item_pipe(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_pipe_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }

        public string ModifierName { get; } = "modifier_item_pipe_barrier";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("barrier_radius");
            }
        }
    }
}