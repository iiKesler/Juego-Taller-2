using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

namespace Personajes
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 8f;
        public float airWalkSpeed = 5f;
        public float jumpImpulse = 9f;
        public bool isPaused;
        
        private Vector2 _moveInput;
        private TouchingDirection _touchingDirection;

        private float CurrentMoveSpeed
        {
            get
            {
                if (CanMove)
                {
                    if (IsMoving && !_touchingDirection.IsOnWall)
                    {
                        if (_touchingDirection.IsGrounded)
                        {
                            if (isRunning)
                            {
                                return runSpeed;
                            }

                            return walkSpeed;
                        }

                        // En el aire
                        return airWalkSpeed;
                    }

                    // Reposo
                    return 0;
                }

                // Movimiento desactivado
                return 0;
            }
        }
        
        [FormerlySerializedAs("_isMoving")] [SerializeField]
        private bool isMoving;

        private bool IsMoving
        {
            get => isMoving;
            set
            {
                isMoving = value;
                _animator.SetBool(AnimationStrings.IsMoving, value);
            }
        }
        
        [FormerlySerializedAs("_isRunning")] [SerializeField]
        private bool isRunning;

        private bool IsRunning
        {
            get => isRunning;
            set
            {
                isRunning = value;
                _animator.SetBool(AnimationStrings.IsRunning, value);
            }
        }

        private bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

        private bool IsAlive => _animator.GetBool(AnimationStrings.IsAlive);

        private bool LockVelocity
        {
            get => _animator.GetBool(AnimationStrings.LockVelocity);
            set => _animator.SetBool(AnimationStrings.LockVelocity, value);
        }

        private Rigidbody2D _rb;
        private Animator _animator;
        private PauseMenu _pauseMenu;
        private Damageable _damageable;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _touchingDirection = GetComponent<TouchingDirection>();
            _damageable = GetComponent<Damageable>();
        }
        
        private void FixedUpdate()
        {
            var velocity = _rb.velocity;
            
            if (!LockVelocity)
            {
                velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, velocity.y);
                _rb.velocity = velocity;
            }
            
            _animator.SetFloat(AnimationStrings.YVelocity, velocity.y);

            if (IsAlive == false)
            {
                OnDying();
            }
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();

            if (IsAlive)
            {
                IsMoving = _moveInput != Vector2.zero;

                SetFacingDirecion(_moveInput);
            }
            else
            {
                isMoving = false;
            }
        }
        

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsRunning = true;
            }
            else if (context.canceled)
            {
                IsRunning = false;
            }
        }
        
        
        [FormerlySerializedAs("_isFacingRight")] 
        public bool isFacingRight = true;
        
        private bool IsFacingRight
        {
            get => isFacingRight;
            set
            {
                if(isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                
                isFacingRight = value;
                
            } }

        private void SetFacingDirecion(Vector2 moveInput)
        {
            IsFacingRight = moveInput.x switch
            {
                > 0 when !IsFacingRight => true,
                < 0 when IsFacingRight => false,
                _ => IsFacingRight
            };
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            //Revisar si esta vivo tambien
            if (!context.started || !_touchingDirection.IsGrounded || !CanMove) return;
            _animator.SetTrigger(AnimationStrings.JumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (isPaused) return;
            if (context.started)
            {
                _animator.SetTrigger(AnimationStrings.AttackTrigger);
            }

        }

        public void OnHit(int damage, Vector2 knockback)
        {
            LockVelocity = true;
            _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
        }

        private static void OnDying() // Restart game if player dies
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
