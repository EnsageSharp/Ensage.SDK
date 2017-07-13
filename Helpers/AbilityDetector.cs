// <copyright file="AbilityDetector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;

    public delegate void AbilityEventHandler(Unit sender, AbilityEventArgs e);

    public class AbilityEventArgs : EventArgs
    {
        // public AbilityEventArgs(ActiveAbility ability)
        // {
        // this.Ability = ability;
        // }   
        public AbilityEventArgs(Ability ability)
        {
            this.Ability = ability;
        }

        public Ability Ability { get; }
    }

    public class AbilityDetector
    {
        private static readonly HashSet<Ability> AbilitySet = new HashSet<Ability>();

        static AbilityDetector()
        {
            Entity.OnBoolPropertyChange += Entity_OnBoolPropertyChange;
            Entity.OnFloatPropertyChange += Entity_OnFloatPropertyChange;
        }

        /// <summary>
        ///     Called whenever a hero starts to cast an ability.
        /// </summary>
        public static event AbilityEventHandler AbilityCastStarted;

        /// <summary>
        ///     Called whenever a hero has casted an ability.
        /// </summary>
        public static event AbilityEventHandler AbilityCasted;

        private static void Entity_OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            var ability = sender as Ability;
            if (ability != null && args.PropertyName == "m_bInAbilityPhase")
            {
                if (args.NewValue)
                {
                    AbilitySet.Remove(ability);

                    var caster = ability.Owner as Unit;
                    AbilityCastStarted?.Invoke(caster, new AbilityEventArgs(ability));
                }
                else
                {
                    AbilitySet.Add(ability);
                }
            }
        }

        private static void Entity_OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            Ability ability = sender as Ability;
            if (ability != null && AbilitySet.Contains(ability) && args.PropertyName == "m_fCooldown")
            {
                AbilitySet.Remove(ability);

                if (args.NewValue > 0.0f)
                {
                    var caster = ability.Owner as Unit;
                    AbilityCasted?.Invoke(caster, new AbilityEventArgs(ability));
                }
            }
        }
    }
}