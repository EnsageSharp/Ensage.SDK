// <copyright file="ISlider.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    public interface ISlider
    {
        object MaxValue { get; }

        object MinValue { get; }

        object Value { get; }

        object Clone();

        bool Load(object data);
    }

    public interface ISlider<out T> : ISlider
    {
        T Value { get; }

        T MinValue { get; }

        T MaxValue { get; }
    }
}