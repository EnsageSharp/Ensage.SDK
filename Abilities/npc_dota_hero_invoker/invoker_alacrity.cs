// <copyright file="invoker_alacrity.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class invoker_alacrity : RangedAbility, IInvokableAbility, IHasModifier
    {
        private readonly InvokeHelper<invoker_alacrity> invokeHelper;

        public invoker_alacrity(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_alacrity>(this);
        }

        public float BonusDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "bonus_damage", this.invokeHelper.Exort.Level);
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

        public string ModifierName { get; } = "modifier_invoker_alacrity";

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_wex, AbilityId.invoker_wex, AbilityId.invoker_exort };

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