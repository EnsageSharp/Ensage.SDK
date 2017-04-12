// <copyright file="OrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Menu;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector;

    public abstract class OrbwalkingMode
    {
        protected OrbwalkingMode(IServiceContext context, MenuFactory parent, string name, TargetSelectorConfig config)
        {
            this.Context = context;
            this.Factory = parent.Menu(name);
            this.TargetSelector = new OrbwalkingTargetSelector(context, config);
        }

        public IServiceContext Context { get; }

        public MenuFactory Factory { get; }

        public OrbwalkingTargetSelector TargetSelector { get; }

        public virtual IEnumerable<Unit> GetTargets()
        {
            return this.TargetSelector.GetTargets();
        }

        [Export(typeof(OrbwalkingMode))]
        public class Combo : OrbwalkingMode
        {
            [ImportingConstructor]
            public Combo(IServiceContext context, OrbwalkerConfig parent)
                : base(context, parent.Factory, nameof(Combo), new TargetSelectorConfig(parent.Factory, nameof(Combo), true, false, false, false, false, false))
            {
            }
        }

        [Export(typeof(OrbwalkingMode))]
        public class Farm : OrbwalkingMode
        {
            [ImportingConstructor]
            public Farm(IServiceContext context, OrbwalkerConfig parent)
                : base(context, parent.Factory, nameof(Farm), new TargetSelectorConfig(parent.Factory, nameof(Farm), false, false, false, false, true, true))
            {
            }
        }

        [Export(typeof(OrbwalkingMode))]
        public class Mixed : OrbwalkingMode
        {
            [ImportingConstructor]
            public Mixed(IServiceContext context, OrbwalkerConfig parent)
                : base(context, parent.Factory, nameof(Mixed), new TargetSelectorConfig(parent.Factory, nameof(Mixed), true, false, false, false, true, true))
            {
            }
        }

        [Export(typeof(OrbwalkingMode))]
        public class Push : OrbwalkingMode
        {
            [ImportingConstructor]
            public Push(IServiceContext context, OrbwalkerConfig parent)
                : base(context, parent.Factory, nameof(Push), new TargetSelectorConfig(parent.Factory, nameof(Push), false, true, true, true, false, false))
            {
            }
        }

        [Export(typeof(OrbwalkingMode))]
        public class Support : OrbwalkingMode
        {
            [ImportingConstructor]
            public Support(IServiceContext context, OrbwalkerConfig parent)
                : base(context, parent.Factory, nameof(Support), new TargetSelectorConfig(parent.Factory, nameof(Support), true, false, false, false, true, false))
            {
            }
        }
    }
}