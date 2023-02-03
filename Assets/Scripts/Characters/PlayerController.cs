using Assets.Scripts.Helpers;

using System.Collections.Generic;

using UnityEngine;

namespace Assets.Materials.Resources.Scripts.Characters
{
    public class PlayerController : BaseController
    {
        private readonly ContactFilter2D _wallOverlapFilter = new()
        { 
            minDepth = 0, 
            useDepth = true 
        };

        [SerializeField] private Collider2D _wallCollider;
        [SerializeField] private bool _secondJumpAvailable = false;
        [SerializeField] private float _hangTime = 0.11f; //seconds
        [SerializeField] private float _jumpBufferLength = 0.15f; //seconds

        private bool _secondJumpActive;
        private float _hangTimer;
        private float _jumpBufferCounter;
        private bool _isOnWall = false;
        private bool _isNearWall = false;
        private bool _isOnWallJump = false;

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
            CheckWalls();

            var horizontalMove = Input.GetAxis(Constants.Axes[(int)Axis2D.X]);

            if (!_isOnWall && (horizontalMove != 0 || _rigidbody.velocity.x != 0))
            {
                var move = !_isOnWallJump ? horizontalMove : Mathf.Abs(horizontalMove) * (int)Direction;

                Move(move);
                Flip(move);
            }

            CheckJumpBuffer();

            if (_isNearWall && !IsGrounded && Input.GetKeyDown(KeyCode.E))
            {
                if (!_isOnWall)
                    StickToWall();
                else
                    UnstickFromWall();
            }

            if (!_isOnWall && ((_jumpBufferCounter >= 0.0f && _hangTimer > 0.0f) || (Input.GetKeyDown(KeyCode.Space) && _secondJumpActive)))
                Jump();

            if (_isOnWall && Input.GetKeyDown(KeyCode.Space))
                WallJump();

            if (Input.GetKeyUp(KeyCode.Space) && _rigidbody.velocity.y > 0)
                StopJump();
        }

        private void Flip(float horizontalMove)
        {
            if (horizontalMove == 0)
                return;

            var direction = horizontalMove > 0 ? Direction.Right : Direction.Left;
            Flip(direction);
        }

        private void WallJump()
        {
            _isOnWall = false;
            _isOnWallJump = true;

            _rigidbody.gravityScale = Constants.NormalGravity;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + _speed * (int)Direction, _jumpSpeed);
        }

        private void CheckWalls()
        {
            _isNearWall = GetWallOverlaps().Count > 0;

            if (!_isNearWall)
            {
                _isOnWall = false;

                return;
            }

            if (!_isOnWall)
            {
                _rigidbody.gravityScale = Constants.NormalGravity;
            }       
        }

        private void StickToWall()
        {
            _isOnWall = true;
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = new Vector2(0, 0);

            var wallCollider = GetWallOverlaps()[0];
            var collisionVector = _wallCollider.Distance(wallCollider).normal;

            Flip(Helper.GetOppositeDirection((Direction)collisionVector.x));
        }

        private void UnstickFromWall()
        {
            _rigidbody.gravityScale = Constants.NormalGravity;
            _isOnWall = false;
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
            if (IsGrounded || _isOnWall)
            {
                _isOnWallJump = false;
                _hangTimer = _hangTime;

                if(IsGrounded)
                    _secondJumpActive = _secondJumpAvailable;

                return;
            }

            _hangTimer -= Time.deltaTime;
        }

        protected override void Jump()
        {
            if (_hangTimer <= 0 && _secondJumpActive)
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

        private List<Collider2D> GetWallOverlaps()
        {
            var colliders = new List<Collider2D>();
            _wallCollider.OverlapCollider(_wallOverlapFilter, colliders);

            return colliders;
        }
    }
}
