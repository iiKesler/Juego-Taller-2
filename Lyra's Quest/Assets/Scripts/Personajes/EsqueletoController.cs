using UnityEngine;

namespace Personajes
{
    public class EsqueletoController : MonoBehaviour
    {
        public float walkSpeed = 5f;

        private Rigidbody2D _rb;
        private TouchingDirection _touchingDirection;
        public DetectionZone cliffDetectionZone;
        private Damageable _damageable;
        private Animator _animator;

        private enum WalkableDirection { Right, Left }

        private WalkableDirection _walkDirection;
        private Vector2 _walkDirecionVector = Vector2.left;
        
        private bool LockVelocity
        {
            get => _animator.GetBool(AnimationStrings.LockVelocity);
            set => _animator.SetBool(AnimationStrings.LockVelocity, value);
        }
        
        private WalkableDirection WalkDirection
        {
            get => _walkDirection;
            set
            {
                if (_walkDirection != value)
                {
                    // Cambiar la direccion
                    var o = gameObject;
                    var localScale = o.transform.localScale;
                    localScale = new Vector2(localScale.x * -1, localScale.y);
                    o.transform.localScale = localScale;

                    if (value == WalkableDirection.Right)
                    {
                        _walkDirecionVector = Vector2.right;
                    }
                    else if (value == WalkableDirection.Left)
                    {
                        _walkDirecionVector = Vector2.left;
                    }
                }

                _walkDirection = value;
            }
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _touchingDirection = GetComponent<TouchingDirection>();
            _damageable = GetComponent<Damageable>();
            _animator = GetComponent<Animator>();
        }
        
        private void FixedUpdate()
        {
            if (_touchingDirection.IsGrounded && _touchingDirection.IsOnWall)
            {
                FlipDirecion();
            }
            _rb.velocity = new Vector2(walkSpeed * _walkDirecionVector.x, _rb.velocity.y);
        }
        
        private void FlipDirecion()
        {
            if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else if (WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
        }

        private void OnCliffDetected()
        {
            if(_touchingDirection.IsGrounded)
                FlipDirecion();
        }
    }
}
