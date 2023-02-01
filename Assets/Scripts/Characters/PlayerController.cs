using System;

using UnityEngine;

namespace Assets.Materials.Resources.Scripts.Characters
{
    public class PlayerController : BaseController
    {
        [SerializeField] private bool _secondJumpAvailable = false;
        [SerializeField] private float _hangTime = 0.6f; //seconds
        [SerializeField] private float _jumpBufferLength = 0.5f; //seconds

        private bool _secondJumpActive;
        private float _hangTimer;
        private float _jumpBufferCounter;

        private new void Awake()
        {
            base.Awake();

            _speed = 6.5f;
            _maxHealth = 50;
            _jumpSpeed = 10.5f;

            _secondJumpActive = _secondJumpAvailable;
            _hangTimer = _hangTime;
            _jumpBufferCounter = _jumpBufferLength;
        }

        private void Update()
        {
            CheckGround();

            var horizontalMove = Input.GetAxis(Constants.Axes[(int)Axis2D.X]);

            if (horizontalMove != 0 || _rigidbody.velocity.x != 0)
            {
                Move(horizontalMove);
                Flip(horizontalMove == 0 ? _spriteRenderer.flipX : horizontalMove < 0);
            }

            CheckJumpBuffer();

            if ((_jumpBufferCounter >= 0.0f && _hangTimer > 0.0f) || (Input.GetKeyDown(KeyCode.Space) && _secondJumpActive))
            {                
                Jump();
            }

            if (Input.GetKeyUp(KeyCode.Space) && _rigidbody.velocity.y > 0)
                StopJump();
        }

        private void CheckJumpBuffer()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpBufferCounter = _jumpBufferLength;
                return;
            }

            _jumpBufferCounter -= Time.deltaTime;
        }

        private void CheckGround()
        {
            if (IsGrounded)
            {
                _secondJumpActive = _secondJumpAvailable;
                _hangTimer = _hangTime;

                return;
            }

            _hangTimer -= Time.deltaTime;
        }

        protected override void Jump()
        {
            if (!IsGrounded || _hangTimer <= 0)
            {
                _secondJumpActive = false;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            }

            _jumpBufferCounter = 0;
            base.Jump();
        }

        private void StopJump()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }

    }
}
