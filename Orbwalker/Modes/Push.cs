// <copyright file="Push.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Service;

    [ExportOrbwalkingMode]
    internal class Push : AttackOrbwalkingMode
    {
        [ImportingConstructor]
        public Push([Import] IServiceContext context)
            : base(context, "Push", 'T', false, true, false, true, false, true)
        {
        }
    }
}