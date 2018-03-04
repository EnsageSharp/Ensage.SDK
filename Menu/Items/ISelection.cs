// <copyright file="ISelection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    public interface ISelection
    {
        int SelectedIndex { get; }

        object Value { get; }

        object[] Values { get; }

        int DecrementSelectedIndex();

        int IncrementSelectedIndex();
    }

    public interface ISelection<out T> : ISelection
    {
        new T Value { get; }

        new T[] Values { get; }
    }
}