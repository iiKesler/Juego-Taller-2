using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Personajes
{
    [RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 8f;
        public float airWalkSpeed = 4.3f;
        public float jumpImpulse = 10f;
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
        
        private Rigidbody2D _rb;
        private Animator _animator;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _touchingDirection = GetComponent<TouchingDirection>();
        }
        
        private void FixedUpdate()
        {
            var velocity = _rb.velocity;
            velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, velocity.y);
            _rb.velocity = velocity;

            _animator.SetFloat(AnimationStrings.YVelocity, velocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();

            IsMoving = _moveInput != Vector2.zero;

            SetFacingDirecion(_moveInput);
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
            if (context.started && _touchingDirection.IsGrounded && CanMove)
            {
                _animator.SetTrigger(AnimationStrings.JumpTrigger);
                _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _animator.SetTrigger(AnimationStrings.AttackTrigger);
            }
        }
 
        public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);
    }
}
