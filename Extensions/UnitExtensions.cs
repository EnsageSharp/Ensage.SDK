// <copyright file="UnitExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System;
    using System.Collections.Generic;	
    using System.Linq;

    using SharpDX;

    public static class UnitExtensions
    {
        public static float AttackPoint(this Unit unit)
        {
            try
            {
                var attackAnimationPoint =
                    Game.FindKeyValues($"{unit.Name}/AttackAnimationPoint", unit is Hero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;

                return attackAnimationPoint / (1 + (unit.AttackSpeedValue() - 100) / 100);
            }
            catch (KeyValuesNotFoundException)
            {
                return 0;
            }
        }

        public static float AttackRange(this Unit unit, Unit target = null)
        {
            var result = (float)(unit.AttackRange + unit.HullRadius);

            if (target != null)
            {
                result += target.HullRadius;
            }

            if(unit is Creep)
            {
                result += 15f;
            }

            if(unit is Hero)
            {
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
                if (psiBlades != null && psiBlades.Level > 0)
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
            }
            
            // test for modifiers with bonus range TODO
            return result;
        }

        public static float AttackSpeedValue(this Unit unit)
        {
            // TODO modifiers like ursa overpower
            var attackSpeed = Math.Max(20, unit.AttackSpeedValue);
            return Math.Min(attackSpeed, 600);
        }

        public static bool CanAttack(this Unit unit)
        {
            return unit.AttackCapability != AttackCapability.None && !unit.IsDisarmed();
        }

        public static float FindRotationAngle(this Unit unit, Vector3 pos)
        {
            var angle = Math.Abs(Math.Atan2(pos.Y - unit.Position.Y, pos.X - unit.Position.X) - unit.RotationRad);

            if (angle > Math.PI)
            {
                angle = Math.PI * 2 - angle;
            }

            return (float)angle;
        }

        public static Ability GetAbilityById(this Unit unit, AbilityId abilityId)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.Id == 0);
        }

        /// <summary>
        /// Returns the damage an auto-attack would do to the target
        /// </summary>
        /// <param name="source">The attacker</param>
        /// <param name="target">The target</param>
        /// <returns></returns>
        public static float GetAttackDamage(this Unit source, Unit target, bool useMinimumDamage = false)
        {
            float damage = (!useMinimumDamage ? source.DamageAverage : source.MinimumDamage) + source.BonusDamage;
            var mult = 1f;
            var damageType = source.AttackDamageType;
            var armorType = target.ArmorType;

            if (damageType == AttackDamageType.Hero && armorType == ArmorType.Structure)
            {
                mult *= .5f;
            }
            else if (damageType == AttackDamageType.Basic && armorType == ArmorType.Hero)
            {
                mult *= .75f;
            }
            else if (damageType == AttackDamageType.Basic && armorType == ArmorType.Structure)
            {
                mult *= .7f;
            }
            else if (damageType == AttackDamageType.Pierce && armorType == ArmorType.Hero)
            {
                mult *= .5f;
            }
            else if (damageType == AttackDamageType.Pierce && armorType == ArmorType.Basic)
            {
                mult *= 1.5f;
            }
            else if (damageType == AttackDamageType.Pierce && armorType == ArmorType.Structure)
            {
                mult *= .35f;
            }
            else if (damageType == AttackDamageType.Siege && armorType == ArmorType.Hero)
            {
                mult *= .85f;
            }
            else if (damageType == AttackDamageType.Siege && armorType == ArmorType.Structure)
            {
                mult *= 1.50f;
            }

            if (target.IsNeutral || target is Creep)
            {
                // TODO test IsNeutral -> quelling blade bonus applied?
                var isMelee = source.IsMelee;
                var quellingBlade = source.GetItemById(AbilityId.item_quelling_blade)
                                    ?? source.GetItemById(AbilityId.item_iron_talon);
                if (quellingBlade != null)
                {
                    damage += quellingBlade.GetAbilitySpecialData(isMelee ? "damage_bonus" : "damage_bonus_ranged");
                }
            }

            var armor = target.Armor;

            mult *= (1 - 0.06f * armor / (1 + 0.06f * Math.Abs(armor)));

            return damage * mult;
        }

        /// <summary>
        /// Returns the amount of time that it would take for the auto-attack to hit the target
        /// </summary>
        /// <param name="source">The attacker</param>
        /// <param name="target">The target</param>
        /// <returns></returns>
        public static float GetAutoAttackArrivalTime(this Unit source, Unit target, bool takeRotationTimeIntoAccount = true)
        {
            var result = GetProjectileArrivalTime(source, target, source.AttackPoint(), source.IsMelee ? float.MaxValue : source.ProjectileSpeed(), takeRotationTimeIntoAccount);

            if (!(source is Tower))
            {
                result -= 0.05f; //:broscience:
            }

            return result;
        }

        /// <summary>
        /// Returns the amount of time that it would take for a missile to hit the target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="delay"></param>
        /// <param name="missileSpeed"></param>
        /// <param name="takeRotationIntoAccount"></param>
        /// <returns></returns>
        public static float GetProjectileArrivalTime(this Unit source, Unit target, float delay, float missileSpeed, bool takeRotationTimeIntoAccount = true)
        {
            var result = 0f;

            //rotation time
            result += takeRotationTimeIntoAccount ? source.TurnTime(target.NetworkPosition) : 0f;

            //delay
            result += delay;

            //time that takes to the missile to reach the target
            if (missileSpeed != float.MaxValue)
            {
                result += source.Distance2D(target) / missileSpeed;
            }

            return result;
        }

        public static Item GetItemById(this Unit unit, AbilityId abilityId)
        {
            if(!unit.HasInventory)
            {
                return null;
            }
            return unit.Inventory.Items.FirstOrDefault(x => x != null && x.IsValid && x.Id == abilityId);
        }

        public static bool HasModifier(this Unit unit, string modifierName)
        {
            return unit.Modifiers.Any(modifier => modifier.Name == modifierName);
        }

        public static bool HasModifiers(this Unit unit, IEnumerable<string> modifierNames, bool hasAll = true)
        {
            var counter = 0;
            
            foreach (var modifier in unit.Modifiers)
            {
                if(modifierNames.Contains(modifier.Name))
                {
                    counter++;
                    if(hasAll)
                    {
                        return true;
                    }
                }
            }

            return hasAll ? counter == modifierNames.Count() : false;
        }

        public static Vector3 InFront(this Unit unit, float distance)
        {
            var v = unit.Position + unit.Vector3FromPolarAngle() * distance;
            return new Vector3(v.X, v.Y, 0);
        }

        /// <summary>
        /// returns true if source is directly facing to target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsDirectlyFacing(this Unit source, Unit target)
        {
            var vector1 = target.NetworkPosition - source.NetworkPosition;
            var diff = Math.Abs(Math.Atan2(vector1.Y, vector1.X) - source.RotationRad);
            return diff < 0.025f;
        }

        public static bool IsDisarmed(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.Disarmed);
        }

        public static bool IsInAttackRange(this Unit source, Unit target)
        {
            return source.IsInRange(target, source.AttackRange(target), true);
        }

        /// <summary>
        /// Returns if the distance to target is lower than range
        /// </summary>
        /// <param name="sourcePosition"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsInRange(this Unit source, Unit target, float range, bool centerToCenter = false)
        {
            return source.NetworkPosition.IsInRange(target, centerToCenter ? range : Math.Max(0, range - source.HullRadius - target.HullRadius));
        }

        public static bool IsInvulnerable(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.Invulnerable);
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

        public static bool IsStunned(this Unit unit)
        {
            return unit.UnitState.HasFlag(UnitState.Stunned);
        }

        public static bool IsValidOrbwalkingTarget(this Unit attacker, Unit target)
        {
            return target.IsValid && target.IsVisible && target.IsAlive && target.IsSpawned && !target.IsIllusion &&
                   attacker.IsInAttackRange(target) && !target.IsInvulnerable();
        }

        /// <summary>
        /// Returns the units auto attacks projectile speed
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float ProjectileSpeed(this Unit unit)
        {
            try
            {
                return Game.FindKeyValues($"{unit.Name}/ProjectileSpeed", unit is Hero ? KeyValueSource.Hero : KeyValueSource.Unit).IntValue;
            }
            catch (KeyValuesNotFoundException)
            {
                return 0;
            }
        }

        public static float TurnRate(this Unit unit, bool currentTurnRate = true)
        {
            try
            {
                var turnRate =
                    Game.FindKeyValues($"{unit.Name}/MovementTurnRate", unit is Hero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;

                if(currentTurnRate)
                {
                    if (unit.HasModifier("modifier_medusa_stone_gaze_slow"))
                    {
                        turnRate *= 0.65f;
                    }

                    if (unit.HasModifier("modifier_batrider_sticky_napalm"))
                    {
                        turnRate *= 0.3f;
                    }
                }

                return turnRate;
            }
            catch (KeyValuesNotFoundException)
            {
                return 0.5f;
            }
        }

        public static float TurnTime(this Unit unit, Vector3 position)
        {
            return TurnTime(unit, unit.FindRotationAngle(position));
        }

        public static float TurnTime(this Unit unit, Vector2 position)
        {
            return TurnTime(unit, unit.FindRotationAngle(position.ToVector3()));
        }

        public static float TurnTime(this Unit unit, float angle)
        {
            if (unit.ClassId == ClassId.CDOTA_Unit_Hero_Wisp)
            {
                return 0;
            }

            if (angle <= 0.5f)
            {
                return 0;
            }

            return 0.03f / unit.TurnRate(unit is Hero) * angle;
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