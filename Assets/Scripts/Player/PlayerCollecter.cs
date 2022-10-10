using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerLumenResource))]
public class PlayerCollecter : MonoBehaviour
{
    [SerializeField] private float _lumenCollectSpeed = 1.0f;

    private PlayerLumenResource _playerLumanResources;

    private ICollectable _collectable;
    private Coroutine _lumenCollectCoroutine;

    private void Awake()
    {
        _playerLumanResources = GetComponent<PlayerLumenResource>();
    }

    private void Update()
    {
        if (_collectable == null)
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

            _lumenCollectCoroutine = StartCoroutine(CollectLumen((ILumenCollectable)_collectable));
            //_playerResources.Collect(_collectable);
            _collectable = null;
        }
    }

    private IEnumerator CollectLumen(ILumenCollectable lumenCollectable)
    {
        var lumenToCollect = lumenCollectable.Collect();
        var lastLumen = lumenToCollect;

        while (lumenToCollect > 0.0f)
        {
            lumenToCollect -= _lumenCollectSpeed * Time.deltaTime;

            var lumenToAdd = lastLumen - lumenToCollect;

            _playerLumanResources.AddLumen(lumenToAdd);

            lumenCollectable.Lumen = lumenToCollect;
            lastLumen = lumenCollectable.Collect();

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<ICollectable>(out var collectable))
        {
            return;
        }

        _collectable = collectable;

        Debug.Log($"Entered trigger: {other.name}");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<ICollectable>(out var _))
        {
            return;
        }

        _collectable = null;

        Debug.Log($"Exited trigger: {other.name}");
    }
}

