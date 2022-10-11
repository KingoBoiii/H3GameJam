using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollecter<TCollectable> : MonoBehaviour
    where TCollectable : ICollectable
{
    protected TCollectable Collectable { get; private set; }

    protected event Action<TCollectable> OnTriggerEnter;
    protected event Action<TCollectable> OnTriggerExit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<ICollectable>(out var collectable))
        {
            return;
        }

        Collectable = (TCollectable)collectable;
        OnTriggerEnter?.Invoke(Collectable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<ICollectable>(out var collectable))
        {
            return;
        }

        OnTriggerExit?.Invoke(Collectable);
        Collectable = default;
    }
}

