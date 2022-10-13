using System.Collections;
using UnityEngine;

internal class PlayerObjectiveHandler : MonoBehaviour
{
    [SerializeField] private float _transferSpeed = 1.0f;

    [SerializeField] private ParticleSystem _particleSystem;
    private PlayerLumenResource _playerLumenResource;
    private PlayerInteract _playerInteract;
    private ParticleSystemForceField _forceField;

    private Coroutine _transferCoroutine = null;

    private void Awake()
    {
        _playerLumenResource = GetComponent<PlayerLumenResource>();
        _playerInteract = GetComponent<PlayerInteract>();
        _forceField = GetComponent<ParticleSystemForceField>();
    }

    private void Start()
    {
        _playerInteract.OnInteractEvent += OnInteractFunc;
        _playerInteract.OnTriggerExitEvent += OnTriggerExitFunc;

        _particleSystem.Stop();
    }

    private void OnInteractFunc(IInteractable interactable)
    {
        if (interactable is not IObjective)
        {
            return;
        }

        var objective = _playerInteract.GetAs<IObjective>();

        if (objective is ILumenObjective lumenObjective)
        {
            _forceField.enabled = false;
            _particleSystem.Play();

            _transferCoroutine = StartCoroutine(TransferLumenToObjective(lumenObjective));
        }
    }

    private void OnTriggerExitFunc(IInteractable interactable)
    {
        if (interactable is not IObjective)
        {
            return;
        }

        if (_transferCoroutine != null)
        {
            StopCoroutine(_transferCoroutine);
            _transferCoroutine = null;
        }

        _forceField.enabled = true;
        _particleSystem.Stop();
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
        _particleSystem.Stop();
    }
}
