// <copyright file="pudge_rot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pudge
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class pudge_rot : ToggleAbility, IAreaOfEffectAbility, IHasDot, IHasHealthCost, IHasModifier
    {
        public pudge_rot(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration { get; } = 0.5f;

        public bool HasInitialDamage { get; } = false;

        /// <summary>
        ///     Gets the health cost for each tick of the dot.
        /// </summary>
        public float HealthCost
        {
            get
            {
                if (Owner.Health == 1)
                {
                    return 0;
                }
                var amplify = this.Owner.GetSpellAmplification();
                var reduction = this.Ability.GetDamageReduction(this.Owner, this.DamageType);
                return DamageHelpers.GetSpellDamage(this.RawTickDamage, amplify, reduction);
            }
        }

        public string ModifierName { get; } = "modifier_pudge_rot";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rot_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "rot_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_pudge_rot";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rot_tick");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            return targets.All(x => x.Distance2D(this.Owner) < (this.CastRange + this.Radius));
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}