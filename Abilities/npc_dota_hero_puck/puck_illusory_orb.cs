// <copyright file="puck_illusory_orb.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_puck
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class puck_illusory_orb : LineAbility
    {
        public puck_illusory_orb(Ability ability)
            : base(ability)
        {
        }

        public override float CastRange
        {
            get
            {
                var range = this.Ability.GetAbilitySpecialData("max_distance");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_puck);
                if (talent != null && talent.Level > 0)
                {
                    range *= (talent.GetAbilitySpecialData("value") / 100) + 1;
                }

                return range + this.Owner.BonusCastRange();
            }
        }

        public override float Speed
        {
            get
            {
                var speed = this.Ability.GetAbilitySpecialData("orb_speed");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_puck);
                if (talent?.Level > 0)
                {
                    speed *= (talent.GetAbilitySpecialData("value") / 100) + 1;
                }

                return speed;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}