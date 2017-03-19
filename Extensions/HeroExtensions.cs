namespace Ensage.SDK.Extensions
{
    public static class HeroExtensions
    {
        public static float AttackPoint(this Hero hero)
        {
            try
            {
                var attackAnimationPoint =
                    Game.FindKeyValues($"{hero.Name}/AttackAnimationPoint", KeyValueSource.Hero).FloatValue;

                return attackAnimationPoint / (1 + (hero.AttackSpeedValue() - 100) / 100);
            }
            catch (KeyValuesNotFoundException)
            {
                return 0;
            }
        }
    }
}