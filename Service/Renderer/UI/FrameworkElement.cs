// <copyright file="FrameworkElement.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.UI
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Service.Input;
    using Ensage.SDK.Service.Renderer.D2D;

    using SharpDX;

    public abstract class FrameworkElement : PropertyChangeBase
    {
        private List<FrameworkElement> children;

        private object dataContext;

        private bool hasFocus;

        private float height;

        private Vector2 position;

        private bool visible = true;

        private float width;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<MouseEventArgs> MouseClick;

        public event EventHandler MouseEnter;

        public event EventHandler MouseLeave;

        public event EventHandler<MouseEventArgs> MouseMove;

        public virtual Vector2 Center
        {
            get
            {
                return new Vector2(this.Position.X + this.Width / 2, this.Position.Y + this.Height / 2);
            }

            set
            {
                this.Position = new Vector2(value.X - this.Width / 2, value.Y - this.Height / 2);
            }
        }

        public virtual List<FrameworkElement> Children
        {
            get
            {
                return this.GetField(ref this.children);
            }

            set
            {
                this.SetField(ref this.children, value);
            }
        }

        public virtual object DataContext
        {
            get
            {
                return this.GetField(ref this.dataContext);
            }

            set
            {
                this.SetField(ref this.dataContext, value);
            }
        }

        public virtual bool HasFocus
        {
            get
            {
                return this.GetField(ref this.hasFocus);
            }

            set
            {
                this.SetField(ref this.hasFocus, value);
            }
        }

        public virtual float Height
        {
            get
            {
                return this.GetField(ref this.height);
            }

            set
            {
                this.SetField(ref this.height, value);
            }
        }

        public FrameworkElement Parent { get; protected set; }

        public virtual Vector2 Position
        {
            get
            {
                return this.GetField(ref this.position);
            }

            set
            {
                this.SetField(ref this.position, value);
            }
        }

        public virtual bool Visible
        {
            get
            {
                return this.GetField(ref this.visible);
            }

            set
            {
                this.SetField(ref this.visible, value);
            }
        }

        public virtual float Width
        {
            get
            {
                return this.GetField(ref this.width);
            }

            set
            {
                this.SetField(ref this.width, value);
            }
        }

        internal virtual void FireDraw(ID2DRenderer context)
        {
        }

        internal void FireKeyDown(KeyEventArgs args)
        {
            this.KeyDown?.Invoke(this, args);
        }

        internal void FireKeyUp(KeyEventArgs args)
        {
            this.KeyUp?.Invoke(this, args);
        }

        internal void FireMouseClick(MouseEventArgs args)
        {
            this.MouseClick?.Invoke(this, args);
        }

        internal void FireMouseEnter()
        {
            if (!this.HasFocus)
            {
                this.MouseEnter?.Invoke(this, EventArgs.Empty);
                this.HasFocus = true;
            }
        }

        internal void FireMouseLeave()
        {
            if (this.HasFocus)
            {
                this.HasFocus = false;
                this.MouseLeave?.Invoke(this, EventArgs.Empty);
            }
        }

        internal void FireMouseMove(MouseEventArgs args)
        {
            this.MouseMove?.Invoke(this, args);
        }
    }
}