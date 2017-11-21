// <copyright file="IInvokableAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_invoker
{
    public interface IInvokableAbility
    {
        AbilityId[] RequiredOrbs { get; }
    }
}