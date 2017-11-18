// <copyright file="troll_warlord_whirling_axes_melee.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_troll_warlord
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class troll_warlord_whirling_axes_melee : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public troll_warlord_whirling_axes_melee(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return this.Ability.IsActivated && base.IsReady;
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("max_range");
            }
        }

        public string TargetModifierName { get; } = "modifier_troll_warlord_whirling_axes_blind";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}