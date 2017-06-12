// <copyright file="vengefulspirit_magic_missile.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using Ensage.SDK.Extensions;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once StyleCop.SA1300
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
                var damage = this.Ability.GetAbilitySpecialData("magic_missile_damage");

                var damageTalent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_vengeful_spirit_1);
                if (damageTalent != null && damageTalent.Level > 0)
                {
                    damage += damageTalent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}