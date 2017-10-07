// <copyright file="zuus_thundergods_wrath.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class zuus_thundergods_wrath : ActiveAbility, IAreaOfEffectAbility
    {
        private readonly zuus_static_field staticField;

        public zuus_thundergods_wrath(Ability ability)
            : base(ability)
        {
            var passive = this.Owner.GetAbilityById(AbilityId.zuus_static_field);
            if (passive != null)
            {
                this.staticField = new zuus_static_field(passive);
            }
        }

        public float Radius
        {
            get
            {
                return float.MaxValue;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                if (target.IsInvisible())
                {
                    continue;
                }

                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage + (this.staticField?.GetDamage(targets) ?? 0);
        }
    }
}