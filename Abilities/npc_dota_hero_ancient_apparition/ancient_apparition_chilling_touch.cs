// <copyright file="ancient_apparition_chilling_touch.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>
namespace Ensage.SDK.Abilities.npc_dota_hero_ancient_apparition
{
    using System.Linq;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class ancient_apparition_chilling_touch : OrbAbility, IHasModifier
    {
        public ancient_apparition_chilling_touch(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return base.GetDamage() + this.RawDamage;
            }

            var reduction = this.Ability.GetDamageReduction(targets.First(), DamageType.Magical);
            var amplify = this.Ability.SpellAmplification();

            return base.GetDamage(targets) + DamageHelpers.GetSpellDamage(this.RawDamage, amplify, reduction);
        }

        public string ModifierName { get; } = "modifier_chilling_touch_slow";
    }
}