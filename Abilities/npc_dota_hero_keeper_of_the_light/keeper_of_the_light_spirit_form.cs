// <copyright file="keeper_of_the_light_spirit_form.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class keeper_of_the_light_spirit_form : ActiveAbility, IHasModifier
    {
        public keeper_of_the_light_spirit_form(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && this.Ability.IsActivated;
            }
        }

        public bool IsSpiritFormActive
        {
            get
            {
                return this.Owner.HasModifier(this.ModifierName);
            }
        }

        public string ModifierName { get; } = "modifier_keeper_of_the_light_spirit_form";
    }
}