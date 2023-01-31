using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected int _maxHealth = 50;
    [SerializeField] protected float _jumpForce = 0.75f;
    [SerializeField] protected float _groundCheckRadius = 1.6f;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    protected bool IsGrounded => Physics2D.OverlapCircleAll(Position, _groundCheckRadius).Length > 1;
    protected Vector2 Position => transform.position;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
    }

    protected abstract void Move(float horizontalMove);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"><see langword="true"/> - Right. <see langword="false"/> - Left</param>
    protected abstract void Flip(bool direction);
    protected abstract void Jump(float verticalMove);
}
