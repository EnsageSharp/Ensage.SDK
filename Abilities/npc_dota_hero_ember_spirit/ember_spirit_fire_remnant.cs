// <copyright file="ember_spirit_fire_remnant.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    using Ensage.SDK.Extensions;

    using SharpDX;

    public class ember_spirit_fire_remnant : CircleAbility, IHasModifier
    {
        public ember_spirit_fire_remnant(Ability ability)
            : base(ability)
        {
            var activeAbility = this.Owner.GetAbilityById(AbilityId.ember_spirit_activate_fire_remnant);
            this.ActivateFireRemnant = new ember_spirit_activate_fire_remnant(activeAbility);
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

        public override float Speed
        {
            get
            {
                return (this.Ability.GetAbilitySpecialData("speed_multiplier") / 100) * this.Owner.MovementSpeed;
            }
        }

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