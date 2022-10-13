using UnityEngine;

public interface ILumenObjective : IObjective
{
    bool AddLumen(float lumen);
}

internal abstract class LumenObjective : Objective, ILumenObjective
{
    [SerializeField] private float _requiredLumen;

    [field: SerializeField] protected float CurrentLumen { get; private set; }
    public override bool IsComplete => CurrentLumen >= _requiredLumen;

    public bool AddLumen(float lumen)
    {
        CurrentLumen += lumen;
        return IsComplete;
    }
}
