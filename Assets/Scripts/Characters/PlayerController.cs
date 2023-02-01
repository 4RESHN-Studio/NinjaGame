using UnityEngine;

namespace Assets.Materials.Resources.Scripts.Characters
{
    public class PlayerController : BaseController
    {
        private bool _secondJumpActive = true;
        private new void Awake()
        {
            base.Awake();

            _speed = 6.5f;
            _maxHealth = 50;
            _jumpForce = 10.5f;
        }

        private void Update()
        {
            if (IsGrounded)
                _secondJumpActive = true;

            var horizontalMove = Input.GetAxis(Constants.Axes[(int)Axis2D.X]);
            var verticalMove = Input.GetKeyDown(KeyCode.Space);

            if (horizontalMove != 0 || _rigidbody.velocity.x != 0)
            {
                Move(horizontalMove);
                Flip(horizontalMove == 0 ? _spriteRenderer.flipX : horizontalMove < 0);
            }
                
            if(verticalMove && (IsGrounded || _secondJumpActive))
                Jump();
        }

        protected override void Flip(bool direction)
        {
            _spriteRenderer.flipX = direction;
        }

        protected override void Move(float horizontalMove)
        {
            _rigidbody.velocity = new Vector2(horizontalMove * _speed, _rigidbody.velocity.y);
        }

        protected override void Jump()
        {
            if (!IsGrounded)
            {
                _secondJumpActive = false;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            }
                

            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
