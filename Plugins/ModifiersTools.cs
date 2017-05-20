// <copyright file="ModifiersTools.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;
    using System.Text;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using SharpDX;

    [ExportPlugin("Modifiers Tools", StartupMode.Manual, description: "Display Modifiers of your current Hero")]
    public class ModifiersTools : Plugin
    {
        [ImportingConstructor]
        public ModifiersTools([Import] IServiceContext context)
        {
            this.Owner = context.Owner;
        }

        private Unit Owner { get; }

        protected override void OnActivate()
        {
            Drawing.OnDraw += this.OnDraw;
        }

        protected override void OnDeactivate()
        {
            Drawing.OnDraw -= this.OnDraw;
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
                sb.Append("Purgable ");
            }

            if (modifier.IsStunDebuff)
            {
                sb.Append("Stun ");
            }

            return sb.ToString();
        }

        private void OnDraw(EventArgs args)
        {
            var x = 1000;
            var y = 130;
            var flag = FontFlags.DropShadow | FontFlags.AntiAlias;
            var color = Color.Beige;
            var size = new Vector2(22);
            var name = "Arial";
            var nameSize = Vector2.Zero;

            foreach (var modifier in this.Owner.Modifiers)
            {
                var textSize = Drawing.MeasureText(modifier.Name, name, size, flag);
                if (textSize[0] > nameSize[0])
                {
                    nameSize = textSize;
                }
            }

            nameSize = new Vector2(nameSize[0] + 20);

            Drawing.DrawText($"Name", name, new Vector2(x, y), size, color, flag);
            Drawing.DrawText($"Remaining", name, new Vector2(x + nameSize[0], y), size, color, flag);
            Drawing.DrawText($"Duration", name, new Vector2(x + nameSize[0] + 100, y), size, color, flag);
            Drawing.DrawText($"Stacks", name, new Vector2(x + nameSize[0] + 200, y), size, color, flag);
            Drawing.DrawText($"Flags", name, new Vector2(x + nameSize[0] + 260, y), size, color, flag);
            y += 25;

            foreach (var modifier in this.Owner.Modifiers)
            {
                Drawing.DrawText($"{modifier.Name}", name, new Vector2(x, y), size, color, flag);
                Drawing.DrawText($"{modifier.RemainingTime:n3}", name, new Vector2(x + nameSize[0], y), size, color, flag);
                Drawing.DrawText($"{modifier.Duration:n3}", name, new Vector2(x + nameSize[0] + 100, y), size, color, flag);
                Drawing.DrawText($"{modifier.StackCount}", name, new Vector2(x + nameSize[0] + 200, y), size, color, flag);
                Drawing.DrawText($"{this.GetFlags(modifier)}", name, new Vector2(x + nameSize[0] + 260, y), size, color, flag);

                y += 25;
            }
        }
    }
}