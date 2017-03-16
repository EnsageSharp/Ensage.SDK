using SharpDX;

namespace Ensage.SDK.Abilities
{
    public interface IActiveAbility
    {
        float CastPoint { get; }
        bool IsChanneling { get; }

        void UseAbility(bool queued = false);
        void UseAbility(Vector3 target, bool queued = false);
        void UseAbility(Unit target, bool queued = false);
    }
}