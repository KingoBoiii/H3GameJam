using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollecter<TCollectable> : MonoBehaviour
    where TCollectable : ICollectable
{
    protected TCollectable Collectable { get; private set; }

    protected event Action<TCollectable> OnTriggerEnter;
    protected event Action<TCollectable> OnTriggerExit;
    
    //private ICollectable _collectable;
    //private Coroutine _lumenCollectCoroutine;

    //private void Update()
    //{
    //    //if (_collectable == null)
    //    //{
    //    //    return;
    //    //}

    //    //if (Input.GetKeyDown(KeyCode.E))
    //    //{
    //    //    if (_lumenCollectCoroutine != null)
    //    //    {
    //    //        StopCoroutine(_lumenCollectCoroutine);
    //    //        _lumenCollectCoroutine = null;
    //    //    }

    //    //    _lumenCollectCoroutine = StartCoroutine(CollectLumen((ILumenCollectable)_collectable));
    //    //    _collectable = null;
    //    //}
    //}

    //protected virtual void OnUpdate()
    //{
    //}

    //private IEnumerator CollectLumen(ILumenCollectable lumenCollectable)
    //{
    //    var lumenToCollect = lumenCollectable.Collect();
    //    var lastLumen = lumenToCollect;
    //    var playerLumenNotFull = true;

    //    while (lumenToCollect > 0.0f && playerLumenNotFull)
    //    {
    //        lumenToCollect -= _lumenCollectSpeed * Time.deltaTime;

    //        var lumenToAdd = lastLumen - lumenToCollect;

    //        playerLumenNotFull = _playerLumanResources.AddLumen(lumenToAdd);

    //        lumenCollectable.Lumen = lumenToCollect;
    //        lastLumen = lumenCollectable.Collect();

    //        if (!playerLumenNotFull)
    //        {
    //            lumenCollectable.Regen();
    //        }

    //        yield return null;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<ICollectable>(out var collectable))
        {
            return;
        }

        Collectable = (TCollectable)collectable;
        OnTriggerEnter?.Invoke(Collectable);

        Debug.Log($"Entered trigger: {other.name}");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<ICollectable>(out var collectable))
        {
            return;
        }

        OnTriggerExit?.Invoke(Collectable);
        Collectable = default;
        
        Debug.Log($"Exited trigger: {other.name}");
    }
}

