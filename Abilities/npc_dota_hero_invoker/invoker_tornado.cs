// <copyright file="invoker_tornado.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    using SharpDX;

    public class invoker_tornado : LineAbility, IInvokableAbility, IHasTargetModifier
    {
        private readonly InvokeHelper<invoker_tornado> invokeHelper;

        public invoker_tornado(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_tornado>(this);
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

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

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "lift_duration", this.invokeHelper.Wex.Level);
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

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("travel_distance", this.invokeHelper.Wex.Level);
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_wex, AbilityId.invoker_wex, AbilityId.invoker_quas };

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("travel_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_invoker_tornado";

        protected override float RawDamage
        {
            get
            {
                var baseDamage = this.Ability.GetAbilitySpecialData("base_damage");
                var wexDamage = this.Ability.GetAbilitySpecialData("wex_damage", this.invokeHelper.Wex.Level);

                return baseDamage + wexDamage;
            }
        }

        public bool Invoke(List<AbilityId> currentOrbs = null)
        {
            return this.invokeHelper.Invoke(currentOrbs);
        }

        public override bool UseAbility(Unit target)
        {
            return this.Invoke() && base.UseAbility(target);
        }

        public override bool UseAbility(Vector3 position)
        {
            return this.Invoke() && base.UseAbility(position);
        }
    }
}