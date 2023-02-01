using Assets.Scripts.Characters.Enums;
using UnityEngine;

namespace Assets.Materials.Resources.Scripts.Characters
{
    public class PlayerController : BaseController
    {
        private void Update()
        {
            if (IsGrounded) State = AnimationsState.Idle;
            else State = AnimationsState.Jump;
            var horizontalMove = Input.GetAxisRaw(Constants.Axes[(int)Axis2D.X]);
            var verticalMove = Input.GetAxisRaw(Constants.Axes[(int)Axis2D.Y]);

           
            
            if (horizontalMove != 0 || _rigidbody.velocity.x != 0)
            {
                Move(horizontalMove); 
                Flip(horizontalMove < 0);
            }
                

            if(verticalMove != 0 && IsGrounded)
                Jump();
        }

        protected override void Flip(bool direction)
        {
            _spriteRenderer.flipX = direction;
        }

        protected override void Move(float horizontalMove)
        {
            base.Move(horizontalMove);
            _rigidbody.velocity = new Vector2(horizontalMove * _speed, _rigidbody.velocity.y);
        }

        protected override void Jump()
        {
            print(((Vector2)transform.up * _jumpForce).y);
            _rigidbody.AddForce((Vector2)transform.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
