using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _jumpSpeed;
    private readonly float _isGroundedRadius = 0.1f;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    protected bool IsGrounded
    {
        get
        {
            var position = new Vector2(_spriteRenderer.gameObject.transform.position.x, _spriteRenderer.gameObject.transform.position.y);
            return Physics2D.OverlapCircleAll(position, _isGroundedRadius).Length > 1;
        }
    }

    protected Vector2 Position => transform.position;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
    }

    protected virtual void Move(float horizontalMove)
    {
        _rigidbody.velocity = new Vector2(horizontalMove * _speed, _rigidbody.velocity.y);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"><see langword="true"/> - Right. <see langword="false"/> - Left</param>
    protected virtual void Flip(bool direction)
    {
        _spriteRenderer.flipX = direction;
    }
    protected virtual void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
    }
}
