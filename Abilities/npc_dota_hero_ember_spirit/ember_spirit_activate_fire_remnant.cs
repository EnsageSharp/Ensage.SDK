// <copyright file="ember_spirit_activate_fire_remnant.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    /// <summary>
    ///     This ability should be only used to activate a remnant. Use <see cref="ember_spirit_fire_remnant" />.
    /// </summary>
    public class ember_spirit_activate_fire_remnant : AreaOfEffectAbility, IHasModifier
    {
        public ember_spirit_activate_fire_remnant(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Owner.HasModifier(this.ModifierName) && base.CanBeCasted && !this.Owner.IsRooted();
            }
        }

        public string ModifierName { get; } = "modifier_ember_spirit_fire_remnant_timer";

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }
    }
}