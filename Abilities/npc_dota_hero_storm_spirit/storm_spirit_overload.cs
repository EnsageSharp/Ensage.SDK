// <copyright file="storm_spirit_overload.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_storm_spirit
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class storm_spirit_overload : PassiveAbility, IHasModifier, IHasTargetModifier, IAreaOfEffectAbility
    {
        public storm_spirit_overload(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_storm_spirit_overload";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("overload_aoe");
            }
        }

        public string TargetModifierName { get; } = "modifier_storm_spirit_overload_debuff";

        protected override float RawDamage
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                var damage = (float)this.Ability.GetDamage(level - 1);
                return damage;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            var owner = this.Owner;
            if (target == null)
            {
                return this.RawDamage + owner.MinimumDamage + owner.BonusDamage;
            }

            var attackDamage = owner.GetAttackDamage(target);

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            var abilityDamage = DamageHelpers.GetSpellDamage(this.RawDamage, amplify, reduction);

            return attackDamage + abilityDamage;
        }
    }
}