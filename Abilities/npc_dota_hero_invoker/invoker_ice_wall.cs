// <copyright file="invoker_ice_wall.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class invoker_ice_wall : ActiveAbility, IInvokableAbility, IHasTargetModifier
    {
        private readonly InvokeHelper<invoker_ice_wall> invokeHelper;

        public invoker_ice_wall(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_ice_wall>(this);
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

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration", this.invokeHelper.Quas.Level);
            }
        }

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_quas, AbilityId.invoker_quas, AbilityId.invoker_exort };

        public string TargetModifierName { get; } = "modifier_invoker_ice_wall_slow_debuff";

        public bool Invoke(List<AbilityId> currentOrbs = null)
        {
            return this.invokeHelper.Invoke(currentOrbs);
        }

        public override bool UseAbility()
        {
            return this.Invoke() && base.UseAbility();
        }
    }
}