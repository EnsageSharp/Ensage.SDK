using SharpDX;

namespace Ensage.SDK.Abilities
{
    public abstract class ActiveAbility : BaseAbility, IActiveAbility
    {
        protected ActiveAbility(Ability ability) : base(ability)
        {
        }

        public void UseAbility(bool queued = false)
        {
            Ability.UseAbility(queued);
        }

        public void UseAbility(Unit target, bool queued = false)
        {
            Ability.UseAbility(target, queued);
        }

        public void UseAbility(Vector3 target, bool queued = false)
        {
            Ability.UseAbility(target, queued);
        }

        public float CastPoint
        {
            get
            {
                if (Ability is Item)
                    return 0.0f;

                var level = Ability.Level;
                if (level == 0)
                    return 0.0f;

                return Ability.GetCastPoint(level - 1);
            }
        }

        public virtual bool IsChanneling { get; } = false;
    }
}