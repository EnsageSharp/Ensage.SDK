namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using SharpDX;

    public class enigma_midnight_pulse : CircleAbility, IDotAbility
    {
        public enigma_midnight_pulse(Ability ability)
            : base(ability)
        {
        }

        public float Duration => this.Ability.GetAbilitySpecialData("duration");

        public string ModifierName { get; } = "TODO";

        public float TickDamage => this.Ability.GetAbilitySpecialData("damage_percent");

        public float TickRate { get; } = 1.0f;

        public float TotalDamage => this.TickDamage * this.Duration;

        public override float GetDamage(params Unit[] target)
        {
            var damagePercent = this.TickDamage / 100.0f;

            return target.Select(unit => (float)unit.MaximumHealth).Select(maxHealth => maxHealth * damagePercent).Sum();
        }

        public float GetTickDamage(params Unit[] target)
        {
            var damagePercent = this.TickDamage / 100.0f;

            return target.Select(unit => (float)unit.MaximumHealth).Select(maxHealth => maxHealth * damagePercent).Sum();
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}