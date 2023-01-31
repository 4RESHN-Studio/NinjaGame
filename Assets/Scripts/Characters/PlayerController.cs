using UnityEngine;

namespace Assets.Materials.Resources.Scripts.Characters
{
    public class PlayerController : BaseController
    {
        private void Update()
        {
            print($"{Position.x};{Position.y}");
            print(Physics2D.OverlapCircleAll(Position, _groundCheckRadius).Length);

            var horizontalMove = Input.GetAxisRaw(Constants.Axes[(int)Axis2D.X]);
            var verticalMove = Input.GetAxisRaw(Constants.Axes[(int)Axis2D.Y]);

            Move(horizontalMove);
            
            if (horizontalMove != 0)
                Flip(horizontalMove < 0);

            if(verticalMove != 0 && IsGrounded)
                Jump(verticalMove);
        }

        protected override void Flip(bool direction)
        {
            _spriteRenderer.flipX = direction;
        }

        protected override void Move(float horizontalMove)
        {
            _rigidbody.velocity = new Vector2(horizontalMove * _speed, _rigidbody.velocity.y);
        }

        protected override void Jump(float verticalMove)
        {
            _rigidbody.AddForce(transform.up * _jumpForce * verticalMove, ForceMode2D.Impulse);
        }
    }
}
