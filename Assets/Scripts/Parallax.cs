using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector3 _parallaxMovementScale;
    
    private Transform _cameraTransform;

    private float length = 0.0f;
    private float startPos = 0.0f;

    private void Start()
    {
        var camera = Camera.main;
        _cameraTransform = camera.transform;

        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (_cameraTransform.position.x * (1 - _parallaxMovementScale.x));
        float dist = (_cameraTransform.position.x * _parallaxMovementScale.x);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
