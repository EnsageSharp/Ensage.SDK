// <copyright file="zuus_cloud.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class zuus_cloud : RangedAbility, IAreaOfEffectAbility
    {
        private readonly zuus_lightning_bolt lightningBolt;

        public zuus_cloud(Ability ability)
            : base(ability)
        {
            var bolt = this.Owner.GetAbilityById(AbilityId.zuus_lightning_bolt);
            if (bolt != null)
            {
                this.lightningBolt = new zuus_lightning_bolt(bolt);
            }
        }

        public override float CastRange
        {
            get
            {
                return float.MaxValue;
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cloud_radius");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (this.lightningBolt == null)
            {
                return 0;
            }

            var rawLightningDamage = this.lightningBolt.GetDamage();
            var reduction = 0.0f;

            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            // no spell amp on cloud
            return DamageHelpers.GetSpellDamage(rawLightningDamage, 0, reduction);
        }
    }
}
