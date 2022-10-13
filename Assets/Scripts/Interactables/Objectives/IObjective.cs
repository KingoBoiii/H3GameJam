public interface IObjective : IInteractable
{
    bool IsComplete { get; }

    void Complete();
}
