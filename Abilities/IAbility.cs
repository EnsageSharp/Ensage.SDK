namespace Ensage.SDK.Abilities
{
    public interface IAbility
    {
        Ability Ability { get; }

        float GetDamage(params Unit[] target);
    }
}