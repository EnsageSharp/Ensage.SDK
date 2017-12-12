namespace Ensage.SDK.Menu.Items
{
    public interface ISelection<out T>
    {
        int SelectedIndex { get; }

        T Value { get; }

        T[] Values { get; }

        int DecrementSelectedIndex();

        int IncrementSelectedIndex();
    }
}