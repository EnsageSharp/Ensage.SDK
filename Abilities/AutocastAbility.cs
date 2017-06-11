// <copyright file="AutocastAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class AutocastAbility : RangedAbility
    {
        protected AutocastAbility(Ability abiltity)
            : base(abiltity)
        {
        }

        public bool Enabled
        {
            get
            {
                return this.Ability.IsAutoCastEnabled;
            }

            set
            {
                if (value)
                {
                    if (!this.Enabled)
                    {
                        this.Ability.ToggleAutocastAbility();
                    }
                }
                else
                {
                    if (this.Enabled)
                    {
                        this.Ability.ToggleAutocastAbility();
                    }
                }
            }
        }
    }
}