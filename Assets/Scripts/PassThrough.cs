using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PassThrough : MonoBehaviour
{
    [SerializeField] private float EnableColliderAfter = 0.65f;

    private Collider2D _collider;
    private bool _playerOnPlatform;

    private void Awake()
    {
        _collider = GetComponent<TilemapCollider2D>();
    }

    private void Update()
    {
        if(_playerOnPlatform && Input.GetAxis("Vertical") < 0)
        {
            StopAllCoroutines();
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(EnableColliderAfter);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            _playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, false);
    }
}
