using System.Linq;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    // ReSharper disable once InconsistentNaming
    public class vengefulspirit_magic_missile : TargetAbility
    {
        public vengefulspirit_magic_missile(Ability ability) : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = Ability.GetAbilitySpecialData("magic_missile_damage");

            var pierceTalentActive = false;
            var owner = Ability.Owner as Unit;
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
            if (!Ability.CanAffectTarget(target, pierceTalentActive))
                return 0;

            var amplify = Ability.GetSpellAmp();
            var reduction = Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }
    }
}
