// <copyright file="BaseAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    public abstract class BaseAbility : IAbility
    {
        protected BaseAbility(Ability ability)
        {
            this.Ability = ability;
        }

        public Ability Ability { get; }

        public virtual bool IsItem { get; } = false;

        public virtual float Range { get; } = 0.0f;

        public virtual bool CanAffectTarget(Unit target, bool pierceImmunityOverwrite = false)
        {
            // check if target is in the correct team
            var teamType = this.Ability.TargetTeamType;
            if (teamType.HasFlag(TargetTeamType.None))
            {
                return false;
            }

            var team = this.Ability.Owner.Team;
            var targetTeam = target.Team;

            if (!teamType.HasFlag(TargetTeamType.Allied) && team == targetTeam)
            {
                return false;
            }

            if (!teamType.HasFlag(TargetTeamType.Enemy) && team != targetTeam)
            {
                return false;
            }
           
            if (pierceImmunityOverwrite)
            {
                return true;
            }

            // check if target is magic immune
            var pierceType = this.Ability.SpellPierceImmunityType;
            switch (pierceType)
            {
                case SpellPierceImmunityType.None:
                case SpellPierceImmunityType.AlliesNo:
                case SpellPierceImmunityType.EnemiesNo:
                    return !target.IsMagicImmune();

                case SpellPierceImmunityType.AlliesYes:
                    return targetTeam == team;

                case SpellPierceImmunityType.EnemiesYes:
                    return targetTeam != team;
            }


            return true;
        }

        public virtual bool CanHitTarget(Unit target)
        {
            return false;
        }

        public abstract float GetDamage(params Unit[] target);
    }
}