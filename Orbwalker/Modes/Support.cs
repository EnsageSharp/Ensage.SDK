// <copyright file="Support.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Service;

    [ExportOrbwalkingMode]
    internal class Support : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Support([Import] IServiceContext context)
            : base(context, "Support", 'G', true, false, true, true, true, false)
        {
        }
    }
}