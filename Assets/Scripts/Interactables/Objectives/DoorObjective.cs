using UnityEngine;

public class DoorObjective : MonoBehaviour, IInteractable
{
    [SerializeField] private Door _door;
    
    public void Interact()
    {
        _door.Open();
    }
}
