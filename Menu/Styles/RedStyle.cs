// <copyright file="RedStyle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using System;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    using Ensage.SDK.Menu.Styles.Elements;
    using Ensage.SDK.Renderer;

    using NLog;

    [Export]
    [Export(typeof(IMenuStyle))]
    public class RedStyle : IMenuStyle
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [ImportingConstructor]
        public RedStyle([Import] IRendererManager renderer)
        {
            this.StyleConfig.SelectedLineColor = Color.DarkRed;
            this.LoadResources(renderer);
        }

        public string ArrowLeft { get; } = "menuStyle/darkred/left";

        public string ArrowLeftHover { get; } = "menuStyle/darkred/leftHover";

        public string ArrowRight { get; } = "menuStyle/darkred/right";

        public string ArrowRightHover { get; } = "menuStyle/darkred/rightHover";

        public string Checked { get; } = "menuStyle/darkred/checked";

        public string Item { get; } = "menuStyle/darkred/item";

        public string Menu { get; } = "menuStyle/darkred/menu";

        public string Name { get; } = "Dark Red";

        public string Slider { get; } = "menuStyle/darkred/slider";

        public StyleConfig StyleConfig { get; } = new StyleConfig();

        public string TitleBar { get; } = "menuStyle/darkred/titlebar";

        public string Unchecked { get; } = "menuStyle/darkred/unchecked";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LoadResources(IRendererManager renderer)
        {
            try
            {
                renderer.TextureManager.LoadFromResource(this.Menu, @"MenuStyle.darkred.menubg1.png");
                renderer.TextureManager.LoadFromResource(this.Item, @"MenuStyle.darkred.itembg1.png");
                renderer.TextureManager.LoadFromResource(this.TitleBar, @"MenuStyle.darkred.itembg1.png");
                renderer.TextureManager.LoadFromResource(this.ArrowLeft, @"MenuStyle.darkred.arrowleft.png");
                renderer.TextureManager.LoadFromResource(this.ArrowLeftHover, @"MenuStyle.darkred.arrowlefthover.png");
                renderer.TextureManager.LoadFromResource(this.ArrowRight, @"MenuStyle.darkred.arrowright.png");
                renderer.TextureManager.LoadFromResource(this.ArrowRightHover, @"MenuStyle.darkred.arrowrighthover.png");
                renderer.TextureManager.LoadFromResource(this.Checked, @"MenuStyle.darkred.circleshadow.png");
                renderer.TextureManager.LoadFromResource(this.Unchecked, @"MenuStyle.darkred.circleshadowgray.png");
                renderer.TextureManager.LoadFromResource(this.Slider, @"MenuStyle.darkred.sliderbgon.png");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}