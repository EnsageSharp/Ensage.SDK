// <copyright file="invoker_forge_spirit.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Extensions;

    public class invoker_forge_spirit : ActiveAbility, IInvokableAbility
    {
        private readonly InvokeHelper<invoker_forge_spirit> invokeHelper;

        public invoker_forge_spirit(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_forge_spirit>(this);
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
                return this.Ability.GetAbilitySpecialData("spirit_duration", this.invokeHelper.Quas.Level);
            }
        }

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_exort, AbilityId.invoker_exort, AbilityId.invoker_quas };

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