// <copyright file="Support.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.TargetSelector;

    [ExportOrbwalkingMode]
    internal class Support : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Support([Import] IOrbwalker orbwalker, [Import] ITargetSelectorManager targetSelector)
            : base(orbwalker, targetSelector, "Support", 'G', true, false, true, true, true, false)
        {
        }
    }
}