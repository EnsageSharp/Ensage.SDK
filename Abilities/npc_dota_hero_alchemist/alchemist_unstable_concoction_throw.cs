// <copyright file="alchemist_unstable_concoction_throw.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_alchemist
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    /// <summary>
    ///     This ability shouldn't be used directly. Use <see cref="alchemist_unstable_concoction" />.
    /// </summary>
    public class alchemist_unstable_concoction_throw : AreaOfEffectAbility, IHasTargetModifierTexture
    {
        public alchemist_unstable_concoction_throw(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("midair_explosion_radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("movement_speed");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "modifier_alchemist_unstable_concoction_throw" };
    }
}