// <copyright file="chaos_knight_reality_rift.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chaos_knight
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chaos_knight_reality_rift : RangedAbility, IHasTargetModifier
    {
        public chaos_knight_reality_rift(Ability ability)
            : base(ability)
        {
        }

        public override SpellPierceImmunityType PiercesSpellImmunity
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_chaos_knight);
                if (talent?.Level > 0)
                {
                    return SpellPierceImmunityType.EnemiesYes;
                }

                return base.PiercesSpellImmunity;
            }
        }

        public string TargetModifierName { get; } = "modifier_chaos_knight_reality_rift";
    }
}