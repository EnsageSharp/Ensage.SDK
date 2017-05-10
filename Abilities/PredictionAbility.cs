// <copyright file="PredictionAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Prediction;

    using SharpDX;

    public class PredictionAbility
    {
        public SpellDamageDelegate DamageDelegate;

        private AbilityCastType castType = AbilityCastType.Undefined;

        private float delay = -1f;

        private float radius;

        private float range = -1f;

        private List<AbilitySpecialData> specialData;

        private float speed = -1f;

        public PredictionAbility(Hero owner, Ability ability, IPrediction prediction)
        {
            this.Owner = owner;
            this.Ability = ability;
            this.Prediction = prediction;
        }

        public PredictionAbility(Hero owner, AbilityId abilityId, IPrediction prediction)
            : this(owner, owner.GetAbilityById(abilityId), prediction)
        {
        }

        public delegate float SpellDamageDelegate(PredictionAbility predictionAbility, Hero caster, Unit target);

        public Ability Ability { get; }

        public bool AreaOfEffect { get; set; }

        public AbilityBehavior Behavior => this.Ability.AbilityBehavior;

        public float BonusRange
        {
            get
            {
                var bonusRange = 0.0f;

                // items
                var aetherLense = this.Owner.GetItemById(AbilityId.item_aether_lens);
                if (aetherLense != null)
                {
                    bonusRange += aetherLense.GetAbilitySpecialData("cast_range_bonus");
                }

                // talents
                foreach (var talent in this.Owner.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_")))
                {
                    bonusRange += talent.GetAbilitySpecialData("value");
                }

                return bonusRange;
            }
        }

        public AbilityCastType CastType
        {
            get
            {
                if (this.castType == AbilityCastType.Undefined)
                {
                    if (this.Behavior.HasFlag(AbilityBehavior.Passive))
                    {
                        this.castType = AbilityCastType.Passive;
                    }
                    else if (this.Behavior.HasFlag(AbilityBehavior.Toggle))
                    {
                        this.castType = AbilityCastType.Toggle;
                    }
                    else if (this.Behavior.HasFlag(AbilityBehavior.Aura))
                    {
                        this.castType = AbilityCastType.Aura;
                    }
                    else if (this.Behavior.HasFlag(AbilityBehavior.Point) ||
                             this.Behavior.HasFlag(AbilityBehavior.OptionalLocationTarget))
                    {
                        this.castType = AbilityCastType.Position;
                    }
                    else if (this.Behavior.HasFlag(AbilityBehavior.RuneTarget) || this.Behavior.HasFlag(AbilityBehavior.UnitTarget))
                    {
                        this.castType = AbilityCastType.Unit;
                    }
                    else if (this.Behavior.HasFlag(AbilityBehavior.NoTarget))
                    {
                        this.castType = AbilityCastType.NoTarget;
                    }
                }

                return this.castType;
            }

            set
            {
                this.castType = value;
            }
        }

        public bool Collision { get; set; }

        public float Cooldown => this.Ability.Cooldown;

        public float Damage
        {
            get
            {
                if (this.DamageSpecialDataKey != null)
                {
                    return this.SpecialData(this.DamageSpecialDataKey);
                }

                try
                {
                    return this.SpecialData("damage");
                }
                catch (Exception e)
                {
                }

                return this.Ability.GetDamage(this.Ability.Level - 1);
            }
        }

        public string DamageSpecialDataKey { get; set; }

        public DamageType DamageType => this.Ability.DamageType;

        public float Delay
        {
            get
            {
                if (this.delay != -1f)
                {
                    return this.delay;
                }

                if (this.DelaySpecialDataKey != null)
                {
                    return this.SpecialData(this.DelaySpecialDataKey);
                }

                return this.Ability.GetCastPoint(this.Ability.Level - 1);
            }

            set
            {
                this.delay = value;
            }
        }

        public string DelaySpecialDataKey { get; set; }

        public bool IsChanneled
        {
            get
            {
                return this.Behavior.HasFlag(AbilityBehavior.Channeled);
            }
        }

        public float LastCastAttemptTime { get; private set; }

        public Hero Owner { get; set; }

        public IPrediction Prediction { get; }

        public PredictionSkillshotType PredictionSkillshotType { get; set; }

        public float Radius
        {
            get
            {
                if (this.RadiusSpecialDataKey != null)
                {
                    return this.SpecialData(this.RadiusSpecialDataKey);
                }

                return this.radius;
            }

            set
            {
                this.radius = value;
            }
        }

        public string RadiusSpecialDataKey { get; set; }

        public float Range
        {
            get
            {
                if (this.range != -1f)
                {
                    return this.range + this.BonusRange;
                }

                if (this.RangeSpecialDataKey != null)
                {
                    return this.SpecialData(this.RangeSpecialDataKey) + this.BonusRange;
                }

                return this.Ability.GetRange(this.Ability.Level - 1) + this.BonusRange;
            }

            set
            {
                this.range = value;
            }
        }

        public string RangeSpecialDataKey { get; set; }

        public List<AbilitySpecialData> SpecialDataList
        {
            get
            {
                if (this.specialData == null)
                {
                    this.specialData = this.Ability.AbilitySpecialData.ToList();
                }

                return this.specialData;
            }
        }

        public float Speed
        {
            get
            {
                if (this.speed != -1f)
                {
                    return this.speed;
                }

                if (this.SpeedSpecialDataKey != null)
                {
                    return this.SpecialData(this.SpeedSpecialDataKey);
                }

                return float.MaxValue;
            }

            set
            {
                this.speed = value;
            }
        }

        public string SpeedSpecialDataKey { get; set; }

        public float TimeSinceLastCastAttemptTime
        {
            get
            {
                return Game.RawGameTime - this.LastCastAttemptTime;
            }
        }

        public bool CanKill(Unit target)
        {
            return this.GetDamage(target) > target.Health;
        }

        public float GetDamage(Unit target)
        {
            if (this.DamageDelegate != null)
            {
                return this.DamageDelegate(this, this.Owner, target);
            }

            return this.Owner.CalculateSpellDamage(target, this.DamageType, this.Damage);
        }

        public bool IsInRange(Unit target)
        {
            return this.Owner.IsInRange(target, this.Range);
        }

        public bool IsReady()
        {
            return this.Ability.AbilityState == AbilityState.Ready;
        }

        public float SpecialData(string name, uint level = 0)
        {
            return this.Ability.GetAbilitySpecialData(name, level);
        }

        public override string ToString()
        {
            var result = string.Empty;
            result += "=========================== " + this.Ability.Name + " (" + this.Ability.AbilitySlot + ") ====================================\n";
            result += "SpellData ====================\n";
            result += "CastType = " + this.CastType + "\n";
            result += "Behaviour = " + this.Behavior + "\n";
            result += "Delay = " + this.Delay + "\n";
            result += "Range = " + this.Range + "\n";
            result += "BonusRange = " + this.BonusRange + "\n";
            result += "Speed = " + this.Speed + "\n";
            result += "Radius = " + this.Radius + "\n";
            result += "Damage = " + this.Damage + "\n";
            result += "DamageType = " + this.DamageType + "\n";
            result += "Special Data ====================\n";
            foreach (var specialData in this.SpecialDataList)
            {
                result += specialData.Name + " = " + specialData.Value + "\n";
            }

            result += "====================================================================================\n";
            return result;
        }

        public bool Use()
        {
            return this.UseInternal();
        }

        public bool Use(Unit target)
        {
            return this.UseInternal(target);
        }

        public bool Use(Vector3 position)
        {
            return this.UseInternal(null, position);
        }

        private bool UseInternal(Unit target = null, Vector3? position = null)
        {
            var now = Game.RawGameTime;

            if ((now - this.LastCastAttemptTime) < 0.1f)
            {
                return true;
            }

            if (!this.IsReady())
            {
                return false;
            }

            var casted = false;

            if (position == null && target == null)
            {
                this.Ability.UseAbility();
                casted = true;
            }
            else if (position != null)
            {
                this.Ability.UseAbility((Vector3)position);
                casted = true;
            }
            else if (target != null)
            {
                if (this.CastType == AbilityCastType.Unit)
                {
                    this.Ability.UseAbility(target);
                    casted = true;
                }
                else if (this.CastType == AbilityCastType.Position)
                {
                    var input = new PredictionInput(this.Owner, target, this.Delay, this.Speed, this.Radius, this.PredictionSkillshotType, this.AreaOfEffect);
                    var output = this.Prediction.GetPrediction(input);

                    if (output.HitChance >= HitChance.Medium)
                    {
                        this.Ability.UseAbility(output.CastPosition);
                        casted = true;
                    }
                }
            }

            this.LastCastAttemptTime = now;
            return casted;
        }
    }
}