// <copyright file="Farm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.TargetSelector;

    [ExportOrbwalkingMode]
    internal class Farm : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Farm([Import] IOrbwalker orbwalker, [Import] ITargetSelectorManager targetSelector)
            : base(orbwalker, targetSelector, "Farm", 'V', false, false, false, false, true, true)
        {
        }
    }
}