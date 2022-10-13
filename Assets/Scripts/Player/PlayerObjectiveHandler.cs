using System.Collections;
using UnityEngine;

internal class PlayerObjectiveHandler : MonoBehaviour
{
    [SerializeField] private float _transferSpeed = 1.0f;

    private PlayerLumenResource _playerLumenResource;
    private PlayerInteract _playerInteract;

    private Coroutine _transferCoroutine = null;

    private void Awake()
    {
        _playerLumenResource = GetComponent<PlayerLumenResource>();
        _playerInteract = GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        _playerInteract.OnInteractEvent += OnInteractFunc;
        _playerInteract.OnTriggerExitEvent += OnTriggerExitFunc;
    }

    private void OnInteractFunc(IInteractable interactable)
    {
        if(interactable is not IObjective)
        {
            return;
        }

        var objective = _playerInteract.GetAs<IObjective>();

        if(objective is ILumenObjective lumenObjective)
        {
            _transferCoroutine = StartCoroutine(TransferLumenToObjective(lumenObjective));
        }
    }

    private void OnTriggerExitFunc(IInteractable interactable)
    {
        if (interactable is not IObjective)
        {
            return;
        }

        if(_transferCoroutine != null)
        {
            StopCoroutine(_transferCoroutine);
            _transferCoroutine = null;
        }
    }

    private IEnumerator TransferLumenToObjective(ILumenObjective objective)
    {
        var lastLumenToTransfer = 0.0f;

        while (!objective.IsComplete && !_playerLumenResource.IsEmpty)
        {
            var lumenToTransfer = (_transferSpeed * Time.deltaTime) - lastLumenToTransfer;

            lastLumenToTransfer = lumenToTransfer;

            _playerLumenResource.RemoveLumen(lumenToTransfer);
            objective.AddLumen(lumenToTransfer);

            yield return null;
        }

        if (objective.IsComplete)
        {
            objective.Complete();
        }
    }
}
