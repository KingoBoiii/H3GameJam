using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float JumpForce = 1.0f;

    [SerializeField] private Transform CeilingCheck;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundObjects;
    [SerializeField] private float CheckRadius = 1.0f;

    private Vector2 _velocity;
    private bool _isJumping = false;
    private bool _isGrounded;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        FlipSprite();
    }

    private void HandleInput()
    {
        _velocity.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isJumping = true;
        }

        _animator.SetFloat("VelocityX", Mathf.Abs(_velocity.x));
    }

    private void FlipSprite()
    {
        if (_velocity.x > 0.01f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_velocity.x < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundObjects);

        Move();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_velocity.x * Speed, _rigidbody.velocity.y);

        if (_isJumping)
        {
            _rigidbody.AddForce(new Vector2(0.0f, JumpForce));
        }
        _isJumping = false;
    }
}
