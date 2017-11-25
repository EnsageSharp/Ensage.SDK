// <copyright file="invoker_ghost_walk.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class invoker_ghost_walk : ActiveAbility, IInvokableAbility, IHasModifier, IHasTargetModifier, IAreaOfEffectAbility
    {
        private readonly InvokeHelper<invoker_ghost_walk> invokeHelper;

        public invoker_ghost_walk(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_ghost_walk>(this);
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Invisible;

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
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public string ModifierName { get; } = "modifier_invoker_ghost_walk_self";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("area_of_effect");
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_quas, AbilityId.invoker_quas, AbilityId.invoker_wex };

        public string TargetModifierName { get; } = "modifier_invoker_ghost_walk_enemy";

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