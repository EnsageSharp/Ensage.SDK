// <copyright file="rattletrap_hookshot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rattletrap
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Prediction.Collision;

    public class rattletrap_hookshot : LineAbility, IHasTargetModifier
    {
        public rattletrap_hookshot(Ability ability)
            : base(ability)
        {
        }

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.AllUnits;

        public string TargetModifierName { get; } = "modifier_stunned";

        protected override string RadiusName { get; } = "latch_radius";

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}