// <copyright file="invoker_quas.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class invoker_quas : ActiveAbility, IHasModifier
    {
        public invoker_quas(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                if (!this.IsReady)
                {
                    return false;
                }

                var owner = this.Owner;
                if (owner.IsStunned() || owner.IsSilenced())
                {
                    return false;
                }

                // skip LastCastAttempt check

                return true;
            }
        }

        public uint Level
        {
            get
            {
                var level = this.Ability.Level;
                if (this.Owner.HasAghanimsScepter())
                {
                    level++;
                }

                return level;
            }
        }

        public string ModifierName { get; } = "modifier_invoker_quas_instance";
    }
}