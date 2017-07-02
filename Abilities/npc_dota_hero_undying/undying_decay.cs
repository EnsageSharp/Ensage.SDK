// <copyright file="undying_decay.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_undying
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class undying_decay : CircleAbility, IHasTargetModifier, IHasModifier
    {
        public undying_decay(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_undying_decay_buff_counter";

        public override float Speed { get; } = float.MaxValue;

        public string TargetModifierName { get; } = "modifier_undying_decay_debuff_counter";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("decay_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Ability.SpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                // TODO: test if debuff already applied and apply manually before if not yet added
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}
