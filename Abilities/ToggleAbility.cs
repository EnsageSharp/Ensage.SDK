// <copyright file="ToggleAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Reflection;

    

    using NLog;

    public abstract class ToggleAbility : ActiveAbility
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected ToggleAbility(Ability ability)
            : base(ability)
        {
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the ability is enabled or disabled.
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                return this.Ability.IsToggled;
            }

            set
            {
                if (!this.CanBeCasted)
                {
                    Log.Debug($"blocked {this}");
                    return;
                }

                var result = false;
                if (value)
                {
                    if (!this.Enabled)
                    {
                        result = this.Ability.ToggleAbility();
                    }
                }
                else
                {
                    if (this.Enabled)
                    {
                        result = this.Ability.ToggleAbility();
                    }
                }

                if (result)
                {
                    this.LastCastAttempt = Game.RawGameTime;
                }
            }
        }

        /// <summary>
        ///     Toggles the ability to the opposite of its current state.
        /// </summary>
        /// <returns>Will always return true.</returns>
        public override bool UseAbility()
        {
            this.Enabled = !this.Enabled;
            return true; // TODO: return if setter was successful?
        }
    }
}