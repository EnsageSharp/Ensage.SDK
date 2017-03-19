namespace Ensage.SDK.Orbwalker
{
    using System;

    public interface IOrbwalker : IDisposable
    {
        event EventHandler<EventArgs> Attacked;

        event EventHandler<EventArgs> Attacking;
    }
}