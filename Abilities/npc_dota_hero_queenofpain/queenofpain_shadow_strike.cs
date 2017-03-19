using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class queenofpain_shadow_strike : TargetAbility, IDotAbility
    {
        public queenofpain_shadow_strike(Ability ability)
            : base(ability)
        {
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var target = targets.First();
            if (!this.CanAffectTarget(target))
            {
                return 0;
            }

            var damage = this.TickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public override float GetDamage(params Unit[] targets)
        {
            var target = targets.First();
            if (!this.CanAffectTarget(target))
            {
                return 0;
            }

            var damage = this.Ability.GetAbilitySpecialData("strike_damage");
            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction) + this.GetTickDamage(target);
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }

        public override float Speed => this.Ability.GetAbilitySpecialData("projectile_speed");

        public float Duration => this.Ability.GetAbilitySpecialData("duration_tooltip");

        public string ModifierName { get; }

        public float TickRate { get; }

        public float TickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("duration_damage");
                return damage;
            }
        }
    }
}
