using Assets.Materials.Resources.Scripts;
using Assets.Scripts.Characters.Enums;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected int _maxHealth = 50;
    [SerializeField] protected float _jumpForce = 0.75f;
    protected float _groundCheckRadius = 0.85f;

    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;

    protected bool IsGrounded { get; private set; } 
    protected Vector2 Position => transform.position;
    protected AnimationsState State
    {
        get => (AnimationsState)_animator.GetInteger(Constants.AnimationState);
        set => _animator.SetInteger(Constants.AnimationState, (int)value);
    }


    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircleAll(Position, _groundCheckRadius).Length > 1;
    }

    //private void Update()
    //{
    //    if (IsGrounded) State = AnimationsState.Idle;
    //    else State = AnimationsState.Jump;
    //}

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
    }
    
    protected virtual void Move(float horizontalMove)
    {
        if (IsGrounded) State = AnimationsState.Move;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"><see langword="true"/> - Right. <see langword="false"/> - Left</param>
    protected abstract void Flip(bool direction);
    protected abstract void Jump();
}
