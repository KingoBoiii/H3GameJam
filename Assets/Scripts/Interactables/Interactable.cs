using UnityEngine;

internal abstract class Interactable : MonoBehaviour, IInteractable
{
    private InteractableLabel _interactableLabel;

    private void Awake()
    {
        _interactableLabel = GetComponent<InteractableLabel>();
    }

    public abstract void Interact();

    public void OnInteractableEnter()
    {
        _interactableLabel.ShowLabel();
    }

    public void OnInteractableExit()
    {
        _interactableLabel.HideLabel();
    }
}
