namespace Ensage.SDK.Extensions
{
    public static class AnimationExtensions
    {
        public static bool IsAttackAnimation(this Animation animation)
        {
            return animation.Name.Contains("attack"); // TODO confirm
        }
    }
}
