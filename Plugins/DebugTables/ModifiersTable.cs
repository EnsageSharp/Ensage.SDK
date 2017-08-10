// <copyright file="ModifiersTable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins.DebugTables
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;

    [Export(typeof(Table))]
    public sealed class ModifiersTable : Table
    {
        [ImportingConstructor]
        public ModifiersTable([Import] IServiceContext context)
            : base("Modifiers")
        {
            this.Owner = context.Owner;
        }

        private Unit Owner { get; }

        internal override IReadOnlyList<TableColumn> OnUpdate()
        {
            var data = this.Owner.Modifiers.Where(e => e.IsValid).ToArray();

            return new[]
                   {
                       data.Select(e => e.Name).ToColumn("Name"),
                       data.Select(e => e.Name).ToColumn("Texture"),
                       data.Select(e => e.RemainingTime.ToString("N3")).ToColumn("Remaining"),
                       data.Select(e => e.Duration.ToString("N3")).ToColumn("Duration"),
                       data.Select(e => e.StackCount.ToString()).ToColumn("Stacks"),
                       data.Select(this.GetFlags).ToColumn("Flags"),
                   };
        }

        private string GetFlags(Modifier modifier)
        {
            var sb = new StringBuilder();

            if (modifier.IsAura)
            {
                sb.Append("Aura ");
            }

            if (modifier.IsDebuff)
            {
                sb.Append("Debuff ");
            }

            if (modifier.IsHidden)
            {
                sb.Append("Hidden ");
            }

            if (modifier.IsPurgable)
            {
                sb.Append("Purgeable ");
            }

            if (modifier.IsStunDebuff)
            {
                sb.Append("Stun ");
            }

            return sb.ToString();
        }
    }
}