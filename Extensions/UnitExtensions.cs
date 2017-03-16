// <copyright file="UnitExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Linq;

    public static class UnitExtensions
    {
        public static float AttackRange(this Unit unit)
        {
            var result = (float)unit.AttackRange;

            // test for items with bonus range
            var bonusRangeItem = unit.GetItemById(AbilityId.item_dragon_lance)
                                 ?? unit.GetItemById(AbilityId.item_hurricane_pike);
            if (bonusRangeItem != null)
            {
                result += bonusRangeItem.GetAbilitySpecialData("base_attack_range");
            }

            // test for abilities with bonus range
            var sniperTakeAim = unit.GetAbilityById(AbilityId.sniper_take_aim);
            if (sniperTakeAim != null && sniperTakeAim.Level > 0)
            {
                result += sniperTakeAim.GetAbilitySpecialData("bonus_attack_range");
            }

            var psiBlades = unit.GetAbilityById(AbilityId.templar_assassin_psi_blades);
            if (sniperTakeAim != null && sniperTakeAim.Level > 0)
            {
                result += psiBlades.GetAbilitySpecialData("bonus_attack_range");
            }

            // test for talents with bonus range
            foreach (var ability in unit.Spellbook.Spells.Where(x => x.Name.StartsWith("special_bonus_attack_range_")))
            {
                if (ability.Level > 0)
                {
                    result += ability.GetAbilitySpecialData("value");
                }
            }

            // test for modifiers with bonus range TODO
            return result;
        }

        public static Ability GetAbilityById(this Unit unit, AbilityId abilityId)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.AbilityId == 0);
        }

        public static float GetAttackDamage(this Unit unit, Unit target)
        {
            var damage = (float)unit.MinimumDamage + unit.BonusDamage;

            if (target.IsNeutral || target is Creep)
            {
                // TODO test IsNeutral -> quelling blade bonus applied?
                var isMelee = unit.IsMelee;
                var quellingBlade = unit.GetItemById(AbilityId.item_quelling_blade)
                                    ?? unit.GetItemById(AbilityId.item_iron_talon);
                if (quellingBlade != null)
                {
                    damage += quellingBlade.GetAbilitySpecialData(isMelee ? "damage_bonus" : "damage_bonus_ranged");
                }
            }

            // TODO: desolator -armor modifier
            damage *= 1.0f - target.DamageResist;
            return damage;
        }

        public static Item GetItemById(this Unit unit, AbilityId abilityId)
        {
            return unit.Inventory.Items.FirstOrDefault(x => x.AbilityId == abilityId);
        }

        public static bool IsMagicImmune(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.MagicImmune);
        }

        public static bool IsRealUnit(this Unit unit)
        {
            return unit.UnitType != 0 && !unit.UnitState.HasFlag(UnitState.FakeAlly);
        }
    }
}