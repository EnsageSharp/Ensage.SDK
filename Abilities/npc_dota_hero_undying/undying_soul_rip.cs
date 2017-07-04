// <copyright file="undying_soul_rip.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_undying
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class undying_soul_rip : RangedAbility, IAreaOfEffectAbility
    {
        public undying_soul_rip(Ability ability)
            : base(ability)
        {
        }

        public int MaxUnits
        {
            get
            {
                return (int)this.Ability.GetAbilitySpecialData("max_units");
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_unit");
            }
        }

        public int GetAffectedUnitCount(Unit target)
        {
            var count = EntityManager<Unit>.Entities.Count(
                x => x.IsVisible &&
                     x.IsAlive &&
                     x != target &&
                     x != this.Owner &&
                     !(x is Building) &&
                     x.IsRealUnit() &&
                     x.Distance2D(this.Owner) < this.Radius);
            return Math.Min(count, this.MaxUnits);
        }

        /// <summary>
        ///     Returns negative damage for an allied target.
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var target = targets.First();

            var damage = this.RawDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            var count = this.GetAffectedUnitCount(target);

            if (this.Owner.IsEnemy(target))
            {
                return DamageHelpers.GetSpellDamage(damage * count, amplify, reduction);
            }

            return DamageHelpers.GetSpellDamage(damage * count, amplify);
        }
    }
}