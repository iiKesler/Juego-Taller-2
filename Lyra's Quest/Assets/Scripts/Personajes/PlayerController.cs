using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Personajes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 5f;
        Vector2 moveInput;

        private Rigidbody2D rb;
        public bool IsMoving { get; set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();

            IsMoving = moveInput != Vector2.zero;
        }

    }
}
