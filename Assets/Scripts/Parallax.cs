using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxEffect;

    private Transform _cameraTransform;

    private float _length;
    private float _startPos;

    private void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;

        var camera = Camera.main;
        _cameraTransform = camera.transform;
    }

    private void FixedUpdate()
    {
        float temp = (_cameraTransform.position.x * (1 - _parallaxEffect));
        float distance = (_cameraTransform.position.x * _parallaxEffect);

        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        if (temp > _startPos + _length)
        {
            _startPos += _length;
        }
        else if (temp < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
