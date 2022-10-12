using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector3 _parallaxMovementScale;
    
    private Transform _cameraTransform;

    private void Start()
    {
        var camera = Camera.main;
        _cameraTransform = camera.transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Scale(_cameraTransform.position, _parallaxMovementScale);
    }
}
