// <copyright file="vengefulspirit_magic_missile.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    // ReSharper disable once InconsistentNaming
    public class vengefulspirit_magic_missile : TargetAbility
    {
        public vengefulspirit_magic_missile(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.Ability.GetAbilitySpecialData("magic_missile_damage");

            var pierceTalentActive = false;
            var owner = this.Ability.Owner as Unit;
            if (owner != null)
            {
                var damageTalent = owner.GetAbilityById(AbilityId.special_bonus_unique_vengeful_spirit_1);
                if (damageTalent != null && damageTalent.Level > 0)
                {
                    damage += damageTalent.GetAbilitySpecialData("value");
                }

                var pierceTalent = owner.GetAbilityById(AbilityId.special_bonus_unique_vengeful_spirit_3);
                if (pierceTalent != null && pierceTalent.Level > 0)
                {
                    pierceTalentActive = true;
                }
            }

            var target = targets.First();
            if (!this.Ability.CanAffectTarget(target, pierceTalentActive))
            {
                return 0;
            }

            var amplify = this.Ability.GetSpellAmp();
            var reduction = this.Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }
    }
}