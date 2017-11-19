// <copyright file="bounty_hunter_shuriken_toss.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bounty_hunter
{
    using Ensage.SDK.Extensions;

    public class bounty_hunter_shuriken_toss : RangedAbility
    {
        public bounty_hunter_shuriken_toss(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "bonus_damage");
            }
        }
    }
}