using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Personajes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        private Vector2 _moveInput;

        private bool _isMoving = false;

        private Rigidbody2D _rb;

        private Animator _animator;

        public bool IsMoving
        {
            get
            {
                return _isMoving;
            } private set
            {
                _isMoving = value;
                _animator.SetBool("isMoving", value);
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_moveInput.x * walkSpeed, _rb.velocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();

            IsMoving = _moveInput != Vector2.zero;
        }

    }
}
