// <copyright file="DebugTableConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Collections.Generic;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class DebugTableConfig
    {
        public DebugTableConfig(MenuFactory factory)
        {
            this.Factory = factory.Menu("Debug Table");
            this.Toggle = this.Factory.Item("Toggle Drawing", new KeyBind(0x65, KeyBindType.Toggle));
        }

        public MenuFactory Factory { get; }

        public MenuItem<KeyBind> Toggle { get; }

        public void Update(IEnumerable<string> @select)
        {
            foreach (var entry in @select)
            {
                this.Factory.Item(entry, true);
            }
        }
    }
}