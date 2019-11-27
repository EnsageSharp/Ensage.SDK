// <copyright file="snapfire_mortimer_kisses.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_snapfire
{
    using System.Linq;

    using SharpDX;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Geometry;

    public class snapfire_mortimer_kisses : CircleAbility, IHasModifier, IHasTargetModifier, IHasDot, IChannable
    {
        public snapfire_mortimer_kisses(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_impact");
            }
        }

        public string ModifierName { get; } = "modifier_snapfire_mortimer_kisses";
        public string TargetModifierName { get; } = "modifier_snapfire_lil_shredder_debuff";

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_linger_duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_damage") / 2f;
            }
        }

        public float TickRate { get; } = 0.5f;
        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
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

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }

        private Vector3 lastRedirectPosition;

        public bool RedirectGlobeDirection(Unit target)
        {
            if (target.Position.Distance2D(lastRedirectPosition) <= 50)
            {
                return false;
            }

            lastRedirectPosition = target.NetworkPosition;
            return Owner.Move(target.Position);
        }

        public float ChannelDuration
        {
            get
            {
                return IsChanneling ? Owner.GetModifierByName(ModifierName).RemainingTime + Owner.GetModifierByName(ModifierName).ElapsedTime : 0;
            }
        }

        public bool IsChanneling
        {
            get
            {
                return Owner.HasModifier(ModifierName);
            }
        }

        public float RemainingDuration
        {
            get
            {
                return IsChanneling ? Owner.GetModifierByName(ModifierName).RemainingTime : 0;
            }
        }
    }
}