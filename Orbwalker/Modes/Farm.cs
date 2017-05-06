// <copyright file="Farm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;

    [ExportOrbwalkingMode]
    internal class Farm : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Farm([Import] IOrbwalker orbwalker)
            : base(orbwalker, "Farm", 'V', false, false, false, false, true, true)
        {
        }
    }
}