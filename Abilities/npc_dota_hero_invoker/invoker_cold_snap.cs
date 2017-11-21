// <copyright file="invoker_cold_snap.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class invoker_cold_snap : RangedAbility, IInvokableAbility, IHasTargetModifier
    {
        private readonly InvokeHelper<invoker_cold_snap> invokeHelper;

        public invoker_cold_snap(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_cold_snap>(this);
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "duration", this.invokeHelper.Quas.Level);
            }
        }

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_quas, AbilityId.invoker_quas, AbilityId.invoker_quas };

        public string TargetModifierName { get; } = "modifier_invoker_cold_snap";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("freeze_damage", this.invokeHelper.Quas.Level);
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
    }
}