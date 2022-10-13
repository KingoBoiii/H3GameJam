using UnityEngine;

internal class DoorObjective : LumenObjective
{
    [SerializeField] private Door _door;

    public override void Complete()
    {
        Debug.Log("Complete door objective");
        _door.Open();
    }
}
