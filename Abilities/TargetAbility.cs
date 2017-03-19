// <copyright file="TargetAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

    using Ensage.SDK.Helpers;

    public abstract class TargetAbility : RangedAbility
    {
        protected TargetAbility(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var level = this.Ability.Level;
            if (level == 0)
            {
                return 0;
            }

            var damage = this.Ability.GetDamage(level - 1);

            var target = targets.First();
            if (!this.CanAffectTarget(target))
            {
                return 0;
            }

            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }
    }
}