// <copyright file="InvokeHelper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    internal class InvokeHelper<T>
        where T : ActiveAbility, IInvokableAbility
    {
        private readonly T invokableAbility;

        private readonly invoker_invoke invoke;

        private readonly HashSet<ActiveAbility> myOrbs = new HashSet<ActiveAbility>();

        private readonly Dictionary<string, AbilityId> orbModifiers = new Dictionary<string, AbilityId>(3);

        private readonly Unit owner;

        private float invokeTime;

        public InvokeHelper(T ability)
        {
            this.invokableAbility = ability;
            this.owner = ability.Owner;

            var wexAbility = this.owner.GetAbilityById(AbilityId.invoker_wex) ?? EntityManager<Ability>.Entities.FirstOrDefault(x => x.IsValid && x.Id == AbilityId.invoker_wex);
            if (wexAbility != null)
            {
                this.Wex = new invoker_wex(wexAbility);
                this.orbModifiers.Add(this.Wex.ModifierName, this.Wex.Ability.Id);
                this.myOrbs.Add(this.Wex);
            }

            var quasAbility = this.owner.GetAbilityById(AbilityId.invoker_quas) ?? EntityManager<Ability>.Entities.FirstOrDefault(x => x.IsValid && x.Id == AbilityId.invoker_quas);
            if (quasAbility != null)
            {
                this.Quas = new invoker_quas(quasAbility);
                this.orbModifiers.Add(this.Quas.ModifierName, this.Quas.Ability.Id);
                this.myOrbs.Add(this.Quas);
            }

            var exortAbility = this.owner.GetAbilityById(AbilityId.invoker_exort)
                               ?? EntityManager<Ability>.Entities.FirstOrDefault(x => x.IsValid && x.Id == AbilityId.invoker_exort);
            if (exortAbility != null)
            {
                this.Exort = new invoker_exort(exortAbility);
                this.orbModifiers.Add(this.Exort.ModifierName, this.Exort.Ability.Id);
                this.myOrbs.Add(this.Exort);
            }

            var invokeAbility = this.owner.GetAbilityById(AbilityId.invoker_invoke);
            if (invokeAbility != null)
            {
                this.invoke = new invoker_invoke(invokeAbility);
            }
        }

        public invoker_exort Exort { get; }

        public bool IsInvoked
        {
            get
            {
                if (!this.invokableAbility.Ability.IsHidden)
                {
                    return true;
                }

                return (this.invokeTime + 0.5f) > Game.RawGameTime;
            }
        }

        public invoker_quas Quas { get; }

        public invoker_wex Wex { get; }

        public bool CanInvoke(bool checkAbilityManaCost)
        {
            if (this.IsInvoked)
            {
                return true;
            }

            if (this.invoke?.CanBeCasted != true)
            {
                return false;
            }

            if (checkAbilityManaCost && this.owner.Mana < (this.invoke.ManaCost + this.invokableAbility.ManaCost))
            {
                return false;
            }

            return true;
        }

        public bool Invoke(List<AbilityId> currentOrbs)
        {
            if (this.IsInvoked)
            {
                return true;
            }

            if (this.invoke?.CanBeCasted != true)
            {
                return false;
            }

            var orbs = currentOrbs ?? this.owner.Modifiers.Where(x => !x.IsHidden && this.orbModifiers.ContainsKey(x.Name)).Select(x => this.orbModifiers[x.Name]).ToList();
            var missingOrbs = this.GetMissingOrbs(orbs);

            foreach (var id in missingOrbs)
            {
                var orb = this.myOrbs.FirstOrDefault(x => x.Ability.Id == id && x.CanBeCasted);
                if (orb == null)
                {
                    return false;
                }

                if (!orb.UseAbility())
                {
                    return false;
                }
            }

            var invoked = this.invoke.UseAbility();
            if (invoked)
            {
                this.invokeTime = Game.RawGameTime;
            }

            return invoked;
        }

        private IEnumerable<AbilityId> GetMissingOrbs(List<AbilityId> castedOrbs)
        {
            var orbs = castedOrbs.ToList();
            var missing = this.invokableAbility.RequiredOrbs.Where(x => !orbs.Remove(x)).ToList();

            if (!missing.Any())
            {
                return Enumerable.Empty<AbilityId>();
            }

            castedOrbs.RemoveRange(0, Math.Max((castedOrbs.Count - this.invokableAbility.RequiredOrbs.Length) + missing.Count, 0));
            castedOrbs.AddRange(missing);

            return missing.Concat(this.GetMissingOrbs(castedOrbs));
        }
    }
}