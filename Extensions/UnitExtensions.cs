// <copyright file="UnitExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Abilities;
    using Ensage.SDK.Helpers;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public static class UnitExtensions
    {
        private static readonly HashSet<string> ChannelAnimations =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "death_ward_anim",
                "powershot_cast_anim",
                "rearm1_anim",
                "warlock_cast3_upheaval",
                "warlock_cast3_upheaval_channel_anim",
                "cast_channel_shackles_anim",
                "channel_shackles",
                "sand_king_epicast_anim",
                "cast4_tricks_trade",
                "life drain_anim",
                "pudge_dismember_start",
                "pudge_dismember_mid_anim",
                "cast1_FortunesEnd_anim_anim",
                "cast04_spring",
                "Illuminate_anim",
                "cast1_echo_stomp_anim",
                "cast4_black_hole_anim",
                "freezing_field_anim_10s",
                "fiends_grip_cast_anim",
                "fiends_grip_loop_anim",
                "drain_anim"
            };

        private static readonly HashSet<string> DisableModifiers =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "modifier_shadow_demon_disruption",
                "modifier_obsidian_destroyer_astral_imprisonment_prison",
                "modifier_eul_cyclone",
                "modifier_invoker_tornado",
                "modifier_bane_nightmare",
                "modifier_shadow_shaman_shackles",
                "modifier_crystal_maiden_frostbite",
                "modifier_ember_spirit_searing_chains",
                "modifier_axe_berserkers_call",
                "modifier_lone_druid_spirit_bear_entangle_effect",
                "modifier_meepo_earthbind",
                "modifier_naga_siren_ensnare",
                "modifier_storm_spirit_electric_vortex_pull",
                "modifier_treant_overgrowth",
                "modifier_cyclone",
                "modifier_sheepstick_debuff",
                "modifier_shadow_shaman_voodoo",
                "modifier_lion_voodoo",
                "modifier_sheepstick",
                "modifier_brewmaster_storm_cyclone",
                "modifier_puck_phase_shift",
                "modifier_dark_troll_warlord_ensnare",
                "modifier_invoker_deafening_blast_knockback",
                "modifier_pudge_meat_hook"
            };

        private static readonly HashSet<string> EtherealModifiers =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "modifier_ghost_state",
                "modifier_item_ethereal_blade_ethereal",
                "modifier_pugna_decrepify",
                "modifier_necrolyte_sadist_active"
            };

        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static float AttackPoint(this Unit unit)
        {
            try
            {
                var attackAnimationPoint = Game.FindKeyValues($"{unit.Name}/AttackAnimationPoint", unit is Hero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;

                return attackAnimationPoint / (1 + ((unit.AttackSpeedValue() - 100) / 100));
            }
            catch (KeyValuesNotFoundException)
            {
                Log.Warn($"Missing AttackAnimationPoint for {unit.Name}");
                return 0;
            }
        }

        [CanBeNull]
        public static Modifier GetModifierByName(this Unit unit, string name)
        {
            return unit.Modifiers.FirstOrDefault(x => x.Name == name);
        }

        [CanBeNull]
        public static Modifier GetModifierByTextureName(this Unit unit, string name)
        {
            return unit.Modifiers.FirstOrDefault(x => x.TextureName == name);
        }

        public static float AttackRange(this Unit unit, Unit target = null)
        {
            var result = unit.AttackRange + unit.HullRadius;

            if (target != null)
            {
                result += target.HullRadius;
            }

            if (unit is Creep)
            {
                result += 15f;
            }

            var hero = unit as Hero;
            if (hero != null)
            {
                if (hero.IsRanged)
                {
                    // test for talents with bonus range
                    foreach (var ability in hero.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_attack_range_")))
                    {
                        result += ability.GetAbilitySpecialData("value");
                    }

                    // test for items with bonus range
                    var bonusRangeItem = hero.GetItemById(AbilityId.item_dragon_lance) ?? hero.GetItemById(AbilityId.item_hurricane_pike);
                    if (bonusRangeItem != null)
                    {
                        result += bonusRangeItem.GetAbilitySpecialData("base_attack_range");
                    }
                }

                switch (hero.HeroId)
                {
                    case HeroId.npc_dota_hero_sniper:
                        var sniperTakeAim = hero.GetAbilityById(AbilityId.sniper_take_aim);
                        if (sniperTakeAim?.Level > 0)
                        {
                            result += sniperTakeAim.GetAbilitySpecialData("bonus_attack_range");
                        }

                        break;

                    case HeroId.npc_dota_hero_templar_assassin:
                        var psiBlades = hero.GetAbilityById(AbilityId.templar_assassin_psi_blades);
                        if (psiBlades?.Level > 0)
                        {
                            result += psiBlades.GetAbilitySpecialData("bonus_attack_range");
                        }

                        break;

                    case HeroId.npc_dota_hero_enchantress:
                        var impetus = hero.GetAbilityById(AbilityId.enchantress_impetus);
                        if (impetus?.Level > 0 && hero.HasAghanimsScepter())
                        {
                            result += impetus.GetAbilitySpecialData("bonus_attack_range_scepter");
                        }

                        break;

                    case HeroId.npc_dota_hero_terrorblade:
                        var metamorphosis = hero.GetAbilityById(AbilityId.terrorblade_metamorphosis);
                        if (metamorphosis != null && hero.HasModifier("modifier_terrorblade_metamorphosis"))
                        {
                            result += metamorphosis.GetAbilitySpecialData("bonus_range");
                        }

                        break;

                    case HeroId.npc_dota_hero_dragon_knight:
                        var dragonForm = hero.GetAbilityById(AbilityId.dragon_knight_elder_dragon_form);
                        if (dragonForm != null && hero.HasModifier("modifier_dragon_knight_dragon_form"))
                        {
                            result += dragonForm.GetAbilitySpecialData("bonus_attack_range");
                        }

                        break;

                    case HeroId.npc_dota_hero_winter_wyvern:
                        var arcticBurn = hero.GetAbilityById(AbilityId.winter_wyvern_arctic_burn);
                        if (arcticBurn != null && hero.HasModifier("modifier_winter_wyvern_arctic_burn_flight"))
                        {
                            result += arcticBurn.GetAbilitySpecialData("attack_range_bonus");
                        }

                        break;

                    case HeroId.npc_dota_hero_troll_warlord:
                        var trollMeleeForm = hero.GetAbilityById(AbilityId.troll_warlord_berserkers_rage);
                        if (trollMeleeForm != null && hero.HasModifier("modifier_troll_warlord_berserkers_rage"))
                        {
                            result -= trollMeleeForm.GetAbilitySpecialData("bonus_range");
                        }

                        break;

                    case HeroId.npc_dota_hero_lone_druid:
                        var druidMeleeForm = hero.GetAbilityById(AbilityId.lone_druid_true_form);
                        if (druidMeleeForm != null && hero.HasModifier("modifier_lone_druid_true_form"))
                        {
                            // no special data
                            result -= 400;
                        }

                        break;
                }
            }

            return result;
        }

        public static float AttackSpeedValue(this Unit unit)
        {
            // TODO are there other modifiers like this one
            if (unit.GetAbilityById(AbilityId.ursa_overpower) != null && unit.HasModifier("modifier_ursa_overpower"))
            {
                return 600;
            }

            var attackSpeed = Math.Max(20, unit.AttackSpeedValue);
            return Math.Min(attackSpeed, 600);
        }

        public static float CalculateSpellDamage(this Hero source, Unit target, DamageType damageType, float amount)
        {
            switch (damageType)
            {
                case DamageType.Magical:
                    return (1 - target.MagicDamageResist) * (1 + source.GetSpellAmplification()) * amount;
                case DamageType.Physical:
                    return (1 - target.DamageResist) * (1 + source.GetSpellAmplification()) * amount;
                case DamageType.HealthRemoval:
                    return amount;
                case DamageType.Pure:
                    return amount;
            }

            return amount;
        }

        public static bool CanAttack(this Unit unit)
        {
            return unit.AttackCapability != AttackCapability.None && !unit.IsDisarmed();
        }

        public static bool CanAttack(this Unit attacker, Unit target)
        {
            if (target == null || !target.IsValid || !target.IsAlive || !target.IsVisible || !target.IsSpawned || target.IsInvulnerable())
            {
                return false;
            }

            if (attacker.Team == target.Team)
            {
                if (target is Creep)
                {
                    return target.HealthPercent() < 0.5;
                }

                if (target is Hero)
                {
                    return target.HealthPercent() < 0.25;
                }

                if (target is Building)
                {
                    return target.HealthPercent() < 0.10;
                }
            }

            return true;
        }

        public static bool CanCastAbilities(this Unit unit, params BaseAbility[] abilities)
        {
            var myMana = unit.Mana;
            foreach (var ability in abilities.OfType<ActiveAbility>())
            {
                if (!ability.CanBeCasted)
                {
                    return false;
                }

                myMana -= ability.Ability.ManaCost;
                if (myMana < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Gets the direction unit vector
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Vector3 Direction(this Unit unit, float length = 1f)
        {
            var rotation = unit.NetworkRotationRad;
            return new Vector3((float)Math.Cos(rotation) * length, (float)Math.Sin(rotation) * length, unit.NetworkPosition.Z);
        }

        public static Vector2 Direction2D(this Unit unit, float length = 1f)
        {
            var rotation = unit.NetworkRotationRad;
            return new Vector2((float)Math.Cos(rotation) * length, (float)Math.Sin(rotation) * length);
        }

        public static float FindRotationAngle(this Unit unit, Vector3 pos)
        {
            var angle = Math.Abs(Math.Atan2(pos.Y - unit.Position.Y, pos.X - unit.Position.X) - unit.NetworkRotationRad);

            if (angle > Math.PI)
            {
                angle = (Math.PI * 2) - angle;
            }

            return (float)angle;
        }

        [CanBeNull]
        public static Ability GetAbilityById(this Unit unit, AbilityId abilityId)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.Id == abilityId);
        }

        /// <summary>
        ///     Returns the damage an auto-attack would do to the target
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
                mult *= 2.50f;
            }

            if (target.IsNeutral || target is Creep && source.IsEnemy(target))
            {
                var isMelee = source.IsMelee;

                // quelling blade and talon does not stack
                var bonusDmgItem = source.GetItemById(AbilityId.item_quelling_blade) ?? source.GetItemById(AbilityId.item_iron_talon);
                if (bonusDmgItem != null)
                {
                    damage += bonusDmgItem.GetAbilitySpecialData(isMelee ? "damage_bonus" : "damage_bonus_ranged");
                }

                // apply percentage bonus damage from battle fury to base dmg
                var battleFury = source.GetItemById(AbilityId.item_bfury);
                if (battleFury != null)
                {
                    mult *= battleFury.GetAbilitySpecialData(isMelee ? "quelling_bonus" : "quelling_bonus_ranged") / 100.0f; // 160 | 125
                }
            }

            var armor = target.Armor;

            mult *= 1 - ((0.06f * armor) / (1 + (0.06f * Math.Abs(armor))));

            return damage * mult;
        }

        /// <summary>
        ///     Returns the amount of time that it would take for the auto-attack to hit the target
        /// </summary>
        /// <param name="source">The attacker</param>
        /// <param name="target">The target</param>
        /// <returns></returns>
        public static float GetAutoAttackArrivalTime(this Unit source, Unit target, bool takeRotationTimeIntoAccount = true)
        {
            var result = GetProjectileArrivalTime(source, target, source.AttackPoint(), source.IsMelee ? float.MaxValue : source.ProjectileSpeed(), takeRotationTimeIntoAccount);

            if (!(source is Tower))
            {
                result -= 0.05f; // :broscience:
            }

            return result;
        }

        [CanBeNull]
        public static Item GetItemById(this Unit unit, AbilityId abilityId)
        {
            if (!unit.HasInventory)
            {
                return null;
            }

            return unit.Inventory.Items.FirstOrDefault(x => x != null && x.IsValid && x.Id == abilityId);
        }

        /// <summary>
        ///     Returns the amount of time that it would take for a missile to hit the target
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

            // rotation time
            result += takeRotationTimeIntoAccount ? source.TurnTime(target.NetworkPosition) : 0f;

            // delay
            result += delay;

            // time that takes to the missile to reach the target
            if (missileSpeed != float.MaxValue)
            {
                result += source.Distance2D(target) / missileSpeed;
            }

            return result;
        }

        public static float GetSpellAmplification(this Unit source)
        {
            var spellAmp = 0.0f;

            var hero = source as Hero;
            if (hero != null)
            {
                spellAmp += hero.TotalIntelligence / 14.0f / 100.0f;
            }

            var aether = source.GetItemById(AbilityId.item_aether_lens);
            if (aether != null)
            {
                spellAmp += aether.AbilitySpecialData.First(x => x.Name == "spell_amp").Value / 100.0f;
            }

            var talents = source.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_spell_amplify_"));
            foreach (var talent in talents)
            {
                spellAmp += talent.AbilitySpecialData.First(x => x.Name == "value").Value / 100.0f;
            }

            return spellAmp;
        }

        public static bool HasAghanimsScepter(this Unit unit)
        {
            return unit.HasAnyModifiers("modifier_item_ultimate_scepter", "modifier_item_ultimate_scepter_consumed");
        }

        public static bool HasAnyModifiers(this Unit unit, params string[] modifierNames)
        {
            return unit.Modifiers.Any(x => modifierNames.Contains(x.Name));
        }

        public static bool HasModifier(this Unit unit, string modifierName)
        {
            return unit.Modifiers.Any(modifier => modifier.Name == modifierName);
        }

        public static bool HasModifiers(this Unit unit, IEnumerable<string> modifierNames, bool hasAll = true)
        {
            return hasAll ? modifierNames.All(x => unit.Modifiers.Any(y => y.Name == x)) : unit.Modifiers.Any(x => modifierNames.Contains(x.Name));
        }

        public static float HealthPercent(this Unit unit)
        {
            return (float)unit.Health / unit.MaximumHealth;
        }

        public static float ImmobileDuration(this Unit unit)
        {
            var result = 0f;
            Modifier relevantModifier = null;
            foreach (var modifier in unit.Modifiers)
            {
                if (!modifier.IsStunDebuff && !DisableModifiers.Contains(modifier.Name))
                {
                    continue;
                }

                var remainingTime = modifier.RemainingTime;
                if (remainingTime <= result)
                {
                    continue;
                }

                relevantModifier = modifier;
                result = remainingTime;
            }

            if (result == 0)
            {
                return 0;
            }

            if (relevantModifier.Name == "modifier_eul_cyclone" || relevantModifier.Name == "modifier_invoker_tornado")
            {
                result += 0.07f;
            }

            return result;
        }

        public static Vector3 InFront(this Unit unit, float distance)
        {
            var v = unit.Position + (unit.Vector3FromPolarAngle() * distance);
            return new Vector3(v.X, v.Y, 0);
        }

        public static bool IsAlly(this Unit unit, Unit target)
        {
            return unit.Team == target.Team;
        }

        public static bool IsAttackImmune(this Unit unit)
        {
            return (unit.UnitState & UnitState.AttackImmune) == UnitState.AttackImmune;
        }

        public static bool IsChannelAnimation(this Animation animation)
        {
            return ChannelAnimations.Contains(animation.Name);
        }

        /// <summary>
        ///     Check if the unit is channeling a spell
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsChanneling(this Unit unit)
        {
            if (ChannelAnimations.Contains(unit.Animation.Name))
            {
                return true;
            }

            return unit.Spellbook.Spells.Any(s => s.IsChanneling);
        }

        /// <summary>
        ///     returns true if source is directly facing to pos
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static bool IsDirectlyFacing(this Unit source, Vector3 pos)
        {
            var vector1 = pos - source.NetworkPosition;
            var diff = Math.Abs(Math.Atan2(vector1.Y, vector1.X) - source.RotationRad);
            return diff < 0.025f;
        }

        /// <summary>
        ///     returns true if source is directly facing to target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsDirectlyFacing(this Unit source, Unit target)
        {
            return source.IsDirectlyFacing(target.NetworkPosition);
        }

        public static bool IsDisarmed(this Unit unit)
        {
            return (unit.UnitState & UnitState.Disarmed) == UnitState.Disarmed;
        }

        public static bool IsEnemy(this Unit unit, Entity target)
        {
            return unit.Team != target.Team;
        }

        public static bool IsEthereal(this Unit unit)
        {
            return unit.HasModifiers(EtherealModifiers, false);
        }

        public static bool IsInAttackRange(this Unit source, Unit target, float bonusAttackRange = 0.0f)
        {
            return source.IsInRange(target, source.AttackRange(target) + bonusAttackRange, true);
        }

        /// <summary>
        ///     Returns if the distance to target is lower than range
        /// </summary>
        /// <param name="sourcePosition"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsInRange(this Unit source, Unit target, float range, bool centerToCenter = false)
        {
            return source.NetworkPosition.IsInRange(target, centerToCenter ? range : Math.Max(0, range + source.HullRadius + target.HullRadius));
        }

        public static bool IsInvisible(this Unit unit)
        {
            return (unit.UnitState & UnitState.Invisible) == UnitState.Invisible;
        }

        public static bool IsInvulnerable(this Unit unit)
        {
            return (unit.UnitState & UnitState.Invulnerable) == UnitState.Invulnerable;
        }

        public static bool IsLinkensProtected(this Unit unit)
        {
            var linkens = unit.GetItemById(AbilityId.item_sphere);
            return linkens != null && linkens.Cooldown <= 0 || unit.HasModifier("modifier_item_sphere_target");
        }

        public static bool IsMagicImmune(this Unit unit)
        {
            return (unit.UnitState & UnitState.MagicImmune) == UnitState.MagicImmune;
        }

        public static bool IsMuted(this Unit unit)
        {
            return (unit.UnitState & UnitState.Muted) == UnitState.Muted;
        }

        public static bool IsRealUnit(this Unit unit)
        {
            return unit.UnitType != 0 && (unit.UnitState & UnitState.FakeAlly) == UnitState.FakeAlly;
        }

        public static bool IsReflectingAbilities(this Unit unit)
        {
            if (unit.HasModifier("modifier_item_lotus_orb_active"))
            {
                return true;
            }

            var spellShield = unit.GetAbilityById(AbilityId.antimage_spell_shield);
            if (spellShield?.Cooldown <= 0 && unit.HasAghanimsScepter())
            {
                return true;
            }

            return false;
        }

        public static bool IsRooted(this Unit unit)
        {
            return (unit.UnitState & UnitState.Rooted) == UnitState.Rooted;
        }

        public static bool IsRotating(this Unit unit)
        {
            return unit.RotationDifference != 0;
        }

        public static IEnumerable<TEntity> GetUnitsInRange<TEntity>(this Unit unit, float range)
            where TEntity : Unit, new()
        {
            var handle = unit.Handle;
            var pos = unit.NetworkPosition;
            var sqrRange = range * range;

            return EntityManager<TEntity>
                .Entities.Where(e => e.Handle != handle && e.IsVisible && e.IsAlive && pos.DistanceSquared(e.NetworkPosition) < sqrRange)
                .OrderBy(e => pos.DistanceSquared(e.NetworkPosition));
        }

        public static IEnumerable<TEntity> GetEnemiesInRange<TEntity>(this Unit unit, float range)
            where TEntity : Unit, new()
        {
            var handle = unit.Handle;
            var team = unit.Team;
            var pos = unit.NetworkPosition;
            var sqrRange = range * range;

            return EntityManager<TEntity>
                .Entities.Where(e => e.Handle != handle && e.IsVisible && e.IsAlive && e.Team != team && pos.DistanceSquared(e.NetworkPosition) < sqrRange)
                .OrderBy(e => pos.DistanceSquared(e.NetworkPosition));
        }

        public static bool IsSilenced(this Unit unit)
        {
            return (unit.UnitState & UnitState.Silenced) == UnitState.Silenced;
        }

        public static bool IsStunned(this Unit unit)
        {
            return (unit.UnitState & UnitState.Stunned) == UnitState.Stunned;
        }

        public static bool IsValidOrbwalkingTarget(this Unit attacker, Unit target, float bonusAttackRange = 0.0f)
        {
            return target.IsValid &&
                   target.IsVisible &&
                   target.IsAlive &&
                   target.IsSpawned &&
                   !target.IsIllusion &&
                   attacker.IsInAttackRange(target, bonusAttackRange) &&
                   !target.IsInvulnerable() &&
                   !target.IsAttackImmune();
        }

        /// <summary>
        ///     Checks if the target is valid (alive, visible, spawned and not invulnerable)
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="range"></param>
        /// <param name="checkTeam"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static bool IsValidTarget(this Unit unit, float range = float.MaxValue, bool checkTeam = true, Vector3? from = null)
        {
            if (unit == null || !unit.IsValid || !unit.IsAlive || !unit.IsVisible || !unit.IsSpawned || unit.IsInvulnerable())
            {
                return false;
            }

            if (checkTeam && unit.Team == ObjectManager.LocalHero.Team)
            {
                return false;
            }

            if (range != float.MaxValue)
            {
                return @from == null ? ObjectManager.LocalHero.IsInRange(unit, range) : ((Vector3)@from).IsInRange(unit, range);
            }

            return true;
        }

        /// <summary>
        ///     Returns the units auto attacks projectile speed
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
                Log.Warn($"Missing ProjectileSpeed for {unit.Name}");
                return 0;
            }
        }

        public static float TurnRate(this Unit unit, bool currentTurnRate = true)
        {
            try
            {
                var turnRate = 0.0f;
                if (unit.IsNeutral)
                {
                    turnRate = 0.5f;
                }
                else
                {
                    turnRate = Game.FindKeyValues($"{unit.Name}/MovementTurnRate", unit is Hero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
                }
                
                if (currentTurnRate)
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
                Log.Warn($"Missing MovementTurnRate for {unit.Name}");
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

            return (0.03f / unit.TurnRate()) * angle;
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
