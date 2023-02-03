using Assets.Materials.Resources.Scripts;

using System.Collections.Generic;

using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] private Collider2D _groundCollider;

    [SerializeField] protected float _speed;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _jumpSpeed;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    protected bool IsGrounded => _groundCollider.OverlapCollider(new ContactFilter2D(), new List<Collider2D>()) > 1;
    protected Vector2 Position => transform.position;
    protected Direction Direction => _spriteRenderer.flipX ? Direction.Left : Direction.Right;

    protected void Awake()
    {
        if (_groundCollider == null)
            throw new MissingReferenceException();

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
    protected virtual void Flip(Direction direction)
    {
        if(direction != Direction.None)
            _spriteRenderer.flipX = direction == Direction.Left;
    }
    protected virtual void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
    }
}
