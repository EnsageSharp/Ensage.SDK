// <copyright file="UnitExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System;
    using System.Linq;

    using SharpDX;

    public static class UnitExtensions
    {
        public static float AttackPoint(this Unit unit)
        {
            try
            {
                var attackAnimationPoint =
                    Game.FindKeyValues($"{unit.Name}/AttackAnimationPoint", KeyValueSource.Unit).FloatValue;

                return attackAnimationPoint / (1 + (unit.AttackSpeedValue() - 100) / 100);
            }
            catch (KeyValuesNotFoundException)
            {
                return 0;
            }
        }

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

        public static float AttackSpeedValue(this Unit hero)
        {
            // TODO modifiers like ursa overpower
            var attackSpeed = Math.Max(20, hero.AttackSpeedValue);
            return Math.Min(attackSpeed, 600);
        }

        public static Ability GetAbilityById(this Unit unit, AbilityId abilityId)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.Id == 0);
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
            return unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
        }

        public static Vector3 InFront(this Unit unit, float distance)
        {
            var v = unit.Position + unit.Vector3FromPolarAngle() * distance;
            return new Vector3(v.X, v.Y, 0);
        }

        public static bool IsMagicImmune(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.MagicImmune);
        }

        public static bool IsMuted(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.Muted);
        }

        public static bool IsRealUnit(this Unit unit)
        {
            return unit.UnitType != 0 && !unit.UnitState.HasFlag(UnitState.FakeAlly);
        }

        public static bool IsSilenced(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.Silenced);
        }

        public static Vector2 Vector2FromPolarAngle(this Unit unit, float delta = 0f, float radial = 1f)
        {
            var diff = MathUtil.DegreesToRadians(unit.RotationDifference);
            var alpha = unit.NetworkRotationRad + diff;
            return SharpDXExtensions.FromPolarCoordinates(radial, alpha + delta);
        }

        public static Vector3 Vector3FromPolarAngle(this Unit unit, float delta = 0f, float radial = 1f)
        {
            return Vector2FromPolarAngle(unit, delta, radial).ToVector3();
        }
    }
}