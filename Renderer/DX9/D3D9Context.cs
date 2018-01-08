// <copyright file="D3D9Context.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;

    using SharpDX.Direct3D9;

    [Export(typeof(ID3D9Context))]
    public sealed class D3D9Context : ID3D9Context
    {
        public D3D9Context()
        {
            if (Drawing.RenderMode != RenderMode.Dx9)
            {
                throw new WrongRenderModeException(RenderMode.Dx9, Drawing.RenderMode);
            }

            Drawing.OnEndScene += this.OnEndScene;
            Drawing.OnPreReset += this.OnPreReset;
            Drawing.OnPostReset += this.OnPostReset;
        }

        public event EventHandler Draw;

        public event EventHandler PostReset;

        public event EventHandler PreReset;

        public Device Device
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Drawing.Direct3DDevice9;
            }
        }

        public void Dispose()
        {
            Drawing.OnEndScene -= this.OnEndScene;
            Drawing.OnPreReset -= this.OnPreReset;
            Drawing.OnPostReset -= this.OnPostReset;
        }

        private void OnEndScene(EventArgs args)
        {
            this.Draw?.Invoke(this, args);
        }

        private void OnPostReset(EventArgs args)
        {
            this.PostReset?.Invoke(this, args);
        }

        private void OnPreReset(EventArgs args)
        {
            this.PreReset?.Invoke(this, args);
        }
    }
}