// <copyright file="invoker_deafening_blast.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Extensions;

    using SharpDX;

    public class invoker_deafening_blast : LineAbility, IInvokableAbility
    {
        private readonly InvokeHelper<invoker_deafening_blast> invokeHelper;

        public invoker_deafening_blast(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_deafening_blast>(this);
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Disarmed;

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

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius_end");
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
                return this.Ability.GetAbilitySpecialData("radius_start");
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_quas, AbilityId.invoker_wex, AbilityId.invoker_exort };

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("travel_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage", this.invokeHelper.Exort.Level);
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