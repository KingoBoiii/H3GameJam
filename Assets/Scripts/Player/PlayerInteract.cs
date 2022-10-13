using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public event Action<IInteractable> OnInteractEvent;
    public event Action<IInteractable> OnTriggerEnterEvent;
    public event Action<IInteractable> OnTriggerExitEvent;
 
    private IInteractable _interactable;

    public T GetAs<T>()
    {
        return (T)_interactable;
    }

    private void Update()
    {
        if (_interactable == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactable.Interact();
            OnInteractEvent?.Invoke(_interactable);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IInteractable>(out var interactable))
        {
            return;
        }

        _interactable = interactable;
        _interactable.OnInteractableEnter();
        OnTriggerEnterEvent?.Invoke(_interactable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<IInteractable>(out var _))
        {
            return;
        }

        OnTriggerExitEvent?.Invoke(_interactable);
        _interactable.OnInteractableExit();
        _interactable = default;
    }
}
