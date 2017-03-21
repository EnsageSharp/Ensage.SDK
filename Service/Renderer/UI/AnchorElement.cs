// <copyright file="AnchorElement.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.UI
{
    using System;
    using System.Reflection;
    using System.Windows.Forms;

    using Ensage.SDK.Service.Renderer.D2D;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using MouseEventArgs = Ensage.SDK.Service.Input.MouseEventArgs;

    public class AnchorElement : FrameworkElement
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AnchorElement()
        {
            this.Width = 100;
            this.Height = 100;
            this.MouseMove += this.OnMouseMove;
            this.MouseEnter += this.OnMouseEnter;
            this.MouseLeave += this.OnMouseLeave;
        }

        internal override void FireDraw(ID2DRenderer context)
        {
            if (!this.Visible)
            {
                return;
            }

            context.DrawRectangle(this.Position.X, this.Position.Y, this.Width, this.Height, 0, context.Brushes["Red"]);
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            this.Visible = true;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (args.Buttons == MouseButtons.Left)
            {
                this.Center = args.Position;
            }
        }
    }
}