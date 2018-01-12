// <copyright file="lina_laguna_blade.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lina
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class lina_laguna_blade : RangedAbility, IHasTargetModifier
    {
        public lina_laguna_blade(Ability ability)
            : base(ability)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return DamageType.Pure;
                }

                return base.DamageType;
            }
        }

        public string TargetModifierName { get; } = "modifier_lina_laguna_blade";
    }
}