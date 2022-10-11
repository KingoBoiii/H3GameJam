using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerLumenResource))]
public class PlayerLumenCollector : PlayerCollecter<ILumenCollectable>
{
    [SerializeField] private float _lumenCollectSpeed = 1.0f;

    private PlayerLumenResource _playerLumanResources;

    private Coroutine _lumenCollectCoroutine;

    private void Awake()
    {
        _playerLumanResources = GetComponent<PlayerLumenResource>();
    }

    private void Start()
    {
        OnTriggerExit += PlayerLumenCollector_OnTriggerExit;
    }

    private void PlayerLumenCollector_OnTriggerExit(ILumenCollectable collectable)
    {
        if (_lumenCollectCoroutine != null)
        {
            StopCoroutine(_lumenCollectCoroutine);
            _lumenCollectCoroutine = null;
        }

        collectable.Regen();
    }

    private void Update()
    {
        if(Collectable == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_lumenCollectCoroutine != null)
            {
                StopCoroutine(_lumenCollectCoroutine);
                _lumenCollectCoroutine = null;
            }

            _lumenCollectCoroutine = StartCoroutine(CollectLumen(Collectable));
        }
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
