// <copyright file="AbilityDetector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Service;

    [Export(typeof(IAbilityDetector))]
    public class AbilityDetector : ControllableService, IAbilityDetector
    {
        private readonly HashSet<DetectedAbility> abilities = new HashSet<DetectedAbility>();

        [ImportingConstructor]
        public AbilityDetector([Import] IServiceContext context)
        {
            this.Context = context;
        }

        /// <summary>
        ///     Called whenever a hero has casted an ability.
        /// </summary>
        public event EventHandler<AbilityEventArgs> AbilityCasted;

        /// <summary>
        ///     Called whenever a hero starts to cast an ability.
        /// </summary>
        public event EventHandler<AbilityEventArgs> AbilityCastStarted;

        public IEnumerable<DetectedAbility> ActiveAbilities
        {
            get
            {
                return this.abilities;
            }
        }

        public IServiceContext Context { get; }

        protected override void OnActivate()
        {
            Entity.OnBoolPropertyChange += this.Entity_OnBoolPropertyChange;
            Entity.OnFloatPropertyChange += this.Entity_OnFloatPropertyChange;

            UpdateManager.SubscribeService(this.Cleanup, 100);
        }

        protected override void OnDeactivate()
        {
            Entity.OnBoolPropertyChange -= this.Entity_OnBoolPropertyChange;
            Entity.OnFloatPropertyChange -= this.Entity_OnFloatPropertyChange;

            UpdateManager.Unsubscribe(this.Cleanup);
        }

        private void Cleanup()
        {
            var time = Game.GameTime;
            this.abilities.RemoveWhere(e => !e.Ability.IsValid || (e.DetectionTime + 250) > time);
        }

        private void Entity_OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            if (args.PropertyName != "m_bInAbilityPhase")
            {
                return;
            }

            var ability = sender as Ability;
            if (ability == null)
            {
                return;
            }

            if (args.NewValue)
            {
                var detected = this.abilities.FirstOrDefault(e => e.Ability == ability);
                if (detected == null)
                {
                    return;
                }

                this.AbilityCastStarted?.Invoke(this, new AbilityEventArgs(detected));
                this.abilities.Remove(detected);
            }
            else
            {
                this.abilities.Add(new DetectedAbility(ability));
            }
        }

        private void Entity_OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            if (args.PropertyName != "m_fCooldown")
            {
                return;
            }

            var ability = sender as Ability;
            if (ability == null)
            {
                return;
            }

            var detected = this.abilities.FirstOrDefault(e => e.Ability == ability);
            if (detected == null)
            {
                return;
            }

            this.abilities.Remove(detected);

            if (args.NewValue > 0.0f)
            {
                this.AbilityCasted?.Invoke(this, new AbilityEventArgs(detected));
            }
        }
    }
}