using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetBool("Open", true);
    }

    public void Close()
    {
        _animator.SetBool("Open", false);
    }
}
