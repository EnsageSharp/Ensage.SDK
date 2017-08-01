// <copyright file="ModifiersTools.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;
    using System.Text;
    using System.Windows.Input;

    using Ensage.SDK.Input;
    using Ensage.SDK.Renderer;
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
            this.Input = context.Input;
        }

        private IInputManager Input { get; }

        private int OffsetY { get; set; } = 100;

        private int OffsetX { get; set; } = 100;

        private Unit Owner { get; }

        protected override void OnActivate()
        {
            Drawing.OnDraw += this.OnDraw;

            this.Input.RegisterHotkey("MoveUp", Key.Up, this.MoveUp);
            this.Input.RegisterHotkey("MoveDown", Key.Down, this.MoveDown);
            this.Input.RegisterHotkey("MoveLeft", Key.Left, this.MoveLeft);
            this.Input.RegisterHotkey("MoveRight", Key.Right, this.MoveRight);
        }

        protected override void OnDeactivate()
        {
            Drawing.OnDraw -= this.OnDraw;

            this.Input.UnregisterHotkey("MoveUp");
            this.Input.UnregisterHotkey("MoveDown");
            this.Input.UnregisterHotkey("MoveLeft");
            this.Input.UnregisterHotkey("MoveRight");
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

        private void MoveDown(KeyEventArgs args)
        {
            this.OffsetY += 10;
        }

        private void MoveLeft(KeyEventArgs args)
        {
            this.OffsetX -= 10;
        }

        private void MoveRight(KeyEventArgs args)
        {
            this.OffsetX += 10;
        }

        private void MoveUp(KeyEventArgs args)
        {
            this.OffsetY -= 10;
        }

        private void OnDraw(EventArgs args)
        {
            var x = this.OffsetX;
            var y = this.OffsetY;
            var flag = FontFlags.DropShadow | FontFlags.AntiAlias;
            var color = Color.Beige;
            var size = new Vector2(22);
            var name = "Arial";
            var nameSize = Vector2.Zero;
            var textureSize = Vector2.Zero;

            foreach (var modifier in this.Owner.Modifiers)
            {
                var textSize = Drawing.MeasureText(modifier.Name, name, size, flag);
                if (textSize[0] > nameSize[0])
                {
                    nameSize = textSize;
                }
            }

            foreach (var modifier in this.Owner.Modifiers)
            {
                var textSize = Drawing.MeasureText(modifier.TextureName, name, size, flag);
                if (textSize[0] > textureSize[0])
                {
                    textureSize = textSize;
                }
            }

            nameSize = new Vector2(nameSize[0] + 20);
            textureSize = new Vector2(textureSize[0] + 20);
            var spanSize = nameSize[0] + textureSize[0];

            Drawing.DrawText($"Name", name, new Vector2(x, y), size, color, flag);
            Drawing.DrawText($"Texture", name, new Vector2(x + nameSize[0], y), size, color, flag);

            Drawing.DrawText($"Remaining", name, new Vector2(x + spanSize, y), size, color, flag);
            Drawing.DrawText($"Duration", name, new Vector2(x + spanSize + 100, y), size, color, flag);
            Drawing.DrawText($"Stacks", name, new Vector2(x + spanSize + 200, y), size, color, flag);
            Drawing.DrawText($"Flags", name, new Vector2(x + spanSize + 270, y), size, color, flag);
            y += 25;

            foreach (var modifier in this.Owner.Modifiers)
            {
                Drawing.DrawText($"{modifier.Name}", name, new Vector2(x, y), size, color, flag);
                Drawing.DrawText($"{modifier.TextureName}", name, new Vector2(x + nameSize[0], y), size, color, flag);

                Drawing.DrawText($"{modifier.RemainingTime:n3}", name, new Vector2(x + spanSize, y), size, color, flag);
                Drawing.DrawText($"{modifier.Duration:n3}", name, new Vector2(x + spanSize + 100, y), size, color, flag);
                Drawing.DrawText($"{modifier.StackCount}", name, new Vector2(x + spanSize + 200, y), size, color, flag);
                Drawing.DrawText($"{this.GetFlags(modifier)}", name, new Vector2(x + spanSize + 270, y), size, color, flag);

                y += 25;
            }
        }
    }
}