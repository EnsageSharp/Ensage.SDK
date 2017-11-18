// <copyright file="vengefulspirit_magic_missile.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using Ensage.SDK.Extensions;

    public class vengefulspirit_magic_missile : RangedAbility
    {
        public vengefulspirit_magic_missile(Ability ability)
            : base(ability)
        {
        }

        public override SpellPierceImmunityType PiercesSpellImmunity
        {
            get
            {
                var pierceTalent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_vengeful_spirit_3);
                if (pierceTalent != null && pierceTalent.Level > 0)
                {
                    return SpellPierceImmunityType.EnemiesYes;
                }

                return base.PiercesSpellImmunity;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("magic_missile_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "magic_missile_damage");
            }
        }
    }
}