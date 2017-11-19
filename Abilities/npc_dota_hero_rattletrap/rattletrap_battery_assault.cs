// <copyright file="rattletrap_battery_assault.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rattletrap
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class rattletrap_battery_assault : ActiveAbility, IAreaOfEffectAbility, IHasDot, IHasTargetModifierTexture, IHasModifier
    {
        public rattletrap_battery_assault(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public string ModifierName { get; } = "modifier_rattletrap_battery_assault";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.RawDamage;
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public string[] TargetModifierTextureName { get; } = { "rattletrap_battery_assault" };

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "interval");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damage = base.RawDamage;

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_clockwerk_3);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public float GetTickDamage(params Unit[] targets)
        {
            return this.GetDamage(targets);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}