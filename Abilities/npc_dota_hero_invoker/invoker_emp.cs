// <copyright file="invoker_emp.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class invoker_emp : CircleAbility, IInvokableAbility
    {
        private readonly InvokeHelper<invoker_emp> invokeHelper;

        public invoker_emp(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_emp>(this);
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("delay");
            }
        }

        public override bool CanBeCasted
        {
            get
            {
                return base.CanBeCasted && this.invokeHelper.CanInvoke(!this.IsInvoked);
            }
        }

        public bool CanBeInvoked
        {
            get
            {
                if (this.IsInvoked)
                {
                    return true;
                }

                return this.invokeHelper.CanInvoke(false);
            }
        }

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("area_of_effect");
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_wex, AbilityId.invoker_wex, AbilityId.invoker_wex };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_burned", this.invokeHelper.Wex.Level);
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var damagePercentage = this.Ability.GetAbilitySpecialData("damage_per_mana_pct") / 100f;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                var manaBurn = Math.Min(target.Mana, damage) * damagePercentage;
                totalDamage += DamageHelpers.GetSpellDamage(manaBurn, amplify, reduction);
            }

            return totalDamage;
        }

        public bool Invoke(List<AbilityId> currentOrbs = null)
        {
            return this.invokeHelper.Invoke(currentOrbs);
        }

        public override bool UseAbility(Vector3 position)
        {
            return this.Invoke() && base.UseAbility(position);
        }

        public override bool UseAbility(Unit target)
        {
            return this.Invoke() && base.UseAbility(target);
        }
    }
}