namespace Geekbrains
{
    public interface IInteractable : IAction
    {
        bool IsInteractable { get; }
    }
}
