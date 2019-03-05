// <copyright file="MenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Renderer;

    

    using PlaySharp.Toolkit.Helper.Annotations;
    using NLog;

    using SharpDX;

    public class MenuItemEntry : MenuBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public MenuItemEntry(string name, View view, IRenderer renderer, MenuConfig menuConfig, ValueBinding valueBinding)
            : this(name, null, view, renderer, menuConfig, valueBinding)
        {
            this.AssignDefaultValue();
        }

        public MenuItemEntry(string name, [CanBeNull] string textureKey, View view, IRenderer renderer, MenuConfig menuConfig, ValueBinding valueBinding)
            : base(name, textureKey, view, renderer, menuConfig, valueBinding)
        {
            this.ValueBinding = valueBinding;
            this.AssignDefaultValue();
        }

        public object DefaultValue { get; set; }

        public string Tooltip { get; set; }

        public Vector2 ValueSize { get; set; }

        public Vector2 TooltipSize { get; set; }

        public object Value
        {
            get
            {
                return this.ValueBinding.Value;
            }

            set
            {
                this.ValueBinding.Value = value;
            }
        }

        public ValueBinding ValueBinding { get; }

        public void AssignDefaultValue()
        {
            if (this.Value is ICloneable clone)
            {
                this.DefaultValue = clone.Clone();
            }
            else if (this.Value.GetType().IsClass)
            {
                throw new Exception($"{this.Value.GetType().Name} doesn't implement {nameof(ICloneable)}");
            }
            else
            {
                this.DefaultValue = this.Value;
            }
        }

        public override void Draw()
        {
            this.View.Draw(this);
            this.View.DrawTooltip(this);
        }

        public override void OnClick(MouseButtons buttons, Vector2 clickPosition)
        {
            this.View.OnClick(this, buttons, clickPosition);
        }

        public override void OnMouseMove(MouseButtons buttons, Vector2 position)
        {
            this.View.OnMouseMove(this, buttons, position);
        }

        public override void Reset()
        {
            Log.Debug($"Resetting {this.Value} to {this.DefaultValue}");
            if (this.DefaultValue is ICloneable clone)
            {
                this.Value = clone.Clone();
            }
            else
            {
                this.Value = this.DefaultValue;
            }
        }
    }
}