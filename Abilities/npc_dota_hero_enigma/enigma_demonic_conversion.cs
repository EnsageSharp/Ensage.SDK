namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    using Ensage.SDK.Helpers;

    public class enigma_demonic_conversion : TargetAbility
    {
        public enigma_demonic_conversion(Ability ability)
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

            // TODO target flag

            return target.MaximumHealth; // we insta deny the target
        }
    }
}