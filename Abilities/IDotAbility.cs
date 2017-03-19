namespace Ensage.SDK.Abilities
{
    public interface IDotAbility
    {
        float Duration { get; }

        string ModifierName { get; }

        float TickDamage { get; }

        float TickRate { get; }

        float GetTickDamage(params Unit[] target);

        float GetTotalDamage(params Unit[] target);
    }
}