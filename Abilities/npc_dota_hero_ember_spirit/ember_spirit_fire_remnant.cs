// <copyright file="ember_spirit_fire_remnant.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Extensions;

    using SharpDX;

    public class ember_spirit_fire_remnant : CircleAbility, IHasModifier
    {
        public ember_spirit_fire_remnant(Ability ability)
            : base(ability)
        {
            this.ActivateFireRemnant = this.AbilityFactory.Value.GetAbility<ember_spirit_activate_fire_remnant>();
        }

        public ember_spirit_activate_fire_remnant ActivateFireRemnant { get; }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string ModifierName { get; } = "modifier_ember_spirit_fire_remnant_charge_counter";

        [Import(typeof(AbilityFactory))]
        private Lazy<AbilityFactory> AbilityFactory { get; set; }

        public override bool UseAbility(Unit target)
        {
            return this.ActivateFireRemnant.CanBeCasted && this.ActivateFireRemnant.UseAbility(target.Position);
        }

        public override bool UseAbility(Vector3 position)
        {
            return this.ActivateFireRemnant.CanBeCasted && this.ActivateFireRemnant.UseAbility(position);
        }
    }
}