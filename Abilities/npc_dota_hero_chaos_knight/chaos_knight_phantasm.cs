// <copyright file="chaos_knight_phantasm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chaos_knight
{
    using Ensage.SDK.Extensions;

    public class chaos_knight_phantasm : ActiveAbility
    {
        public chaos_knight_phantasm(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("illusion_duration");
            }
        }
    }
}