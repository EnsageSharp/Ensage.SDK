// <copyright file="Support.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;

    [ExportOrbwalkingMode]
    internal class Support : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Support([Import] IOrbwalker orbwalker)
            : base(orbwalker, "Support", 'G', true, false, true, true, true, false)
        {
        }
    }
}