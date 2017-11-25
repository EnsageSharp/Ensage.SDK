// <copyright file="invoker_sun_strike.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System.Collections.Generic;

    using Ensage.SDK.Extensions;

    using SharpDX;

    public class invoker_sun_strike : CircleAbility, IInvokableAbility
    {
        private readonly InvokeHelper<invoker_sun_strike> invokeHelper;

        public invoker_sun_strike(Ability ability)
            : base(ability)
        {
            this.invokeHelper = new InvokeHelper<invoker_sun_strike>(this);
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

        public bool CataclysmCanBeCasted
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_invoker_4);
                if (talent?.Level > 0)
                {
                    return this.CanBeCasted;
                }

                return false;
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

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_exort, AbilityId.invoker_exort, AbilityId.invoker_exort };

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

        public override bool UseAbility()
        {
            return this.Invoke() && base.UseAbility();
        }

        public override bool UseAbility(Vector3 position)
        {
            return this.Invoke() && base.UseAbility(position);
        }
    }
}