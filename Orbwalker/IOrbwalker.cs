namespace Ensage.SDK.Orbwalker
{
    using System;

    using SharpDX;

    public interface IOrbwalker : IDisposable
    {
        event EventHandler<EventArgs> Attacked;

        event EventHandler<EventArgs> Attacking;

        void Orbwalk(Vector3 position);

        void Orbwalk(Unit target);
    }
}