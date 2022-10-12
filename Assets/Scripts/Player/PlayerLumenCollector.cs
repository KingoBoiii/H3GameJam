using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerLumenResource))]
internal class PlayerLumenCollector : MonoBehaviour
{
    [SerializeField] private float _lumenCollectSpeed = 1.0f;

    private PlayerLumenResource _playerLumanResources;
    private PlayerInteract _playerInteract;

    private Coroutine _lumenCollectCoroutine;

    private void Awake()
    {
        _playerLumanResources = GetComponent<PlayerLumenResource>();
        _playerInteract = GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        _playerInteract.OnInteractEvent += OnInteractFunc;
        _playerInteract.OnTriggerExitEvent += OnTriggerExitFunc;
    }

    private void OnInteractFunc(IInteractable interactable)
    {
        if (interactable is not ILumenCollectable)
        {
            return;
        }
        
        var lumenCollectable = interactable as ILumenCollectable;
        _lumenCollectCoroutine = StartCoroutine(CollectLumen(lumenCollectable));
    }

    private void OnTriggerExitFunc(IInteractable interactable)
    {
        if(interactable is not ILumenCollectable)
        {
            return;
        }

        if (_lumenCollectCoroutine != null)
        {
            StopCoroutine(_lumenCollectCoroutine);
            _lumenCollectCoroutine = null;
        }

        var lumenCollectable = interactable as ILumenCollectable;
        lumenCollectable.Regen();
    }

    private IEnumerator CollectLumen(ILumenCollectable lumenCollectable)
    {
        var lumenToCollect = lumenCollectable.Collect();
        var lastLumen = lumenToCollect;
        var playerLumenNotFull = true;

        while (lumenToCollect > 0.0f && playerLumenNotFull)
        {
            lumenToCollect -= _lumenCollectSpeed * Time.deltaTime;

            var lumenToAdd = lastLumen - lumenToCollect;

            playerLumenNotFull = _playerLumanResources.AddLumen(lumenToAdd);

            lumenCollectable.Lumen = lumenToCollect;
            lastLumen = lumenCollectable.Collect();

            if (!playerLumenNotFull)
            {
                lumenCollectable.Regen();
            }

            yield return null;
        }
    }
}
