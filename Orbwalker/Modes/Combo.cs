// <copyright file="Combo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.TargetSelector;

    [ExportOrbwalkingMode]
    internal class Combo : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Combo([Import] IOrbwalkerManager orbwalker, [Import] ITargetSelectorManager targetSelector)
            : base(orbwalker, targetSelector, "Combo", 32, true, false, false, false, false, false)
        {
        }
    }
}