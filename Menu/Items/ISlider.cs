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
    }

    public interface ISlider<out T> : ISlider
    {
        new T Value { get; }

        new T MinValue { get; }

        new T MaxValue { get; }
    }
}