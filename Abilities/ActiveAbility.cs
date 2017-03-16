// <copyright file="ActiveAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using SharpDX;

    public abstract class ActiveAbility : BaseAbility, IActiveAbility
    {
        protected ActiveAbility(Ability ability)
            : base(ability)
        {
        }

        public float CastPoint
        {
            get
            {
                if (this.Ability is Item)
                {
                    return 0.0f;
                }

                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0.0f;
                }

                return this.Ability.GetCastPoint(level - 1);
            }
        }

        public virtual bool IsChanneling { get; } = false;

        public void UseAbility(bool queued = false)
        {
            this.Ability.UseAbility(queued);
        }

        public void UseAbility(Unit target, bool queued = false)
        {
            this.Ability.UseAbility(target, queued);
        }

        public void UseAbility(Vector3 target, bool queued = false)
        {
            this.Ability.UseAbility(target, queued);
        }
    }
}