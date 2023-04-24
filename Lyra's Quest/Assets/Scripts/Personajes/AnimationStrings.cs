using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace Personajes
{
    public class AnimationStrings : MonoBehaviour
    {
        internal const string JumpTrigger = "jump";
        internal const string IsGrounded = "isGrounded";
        internal const string IsRunning = "isRunning";
        internal const string IsMoving = "isMoving";
        internal const string YVelocity = "yVelocity";
        internal const string IsOnWall = "isOnWall";
        internal const string IsOnCeiling = "isOnCeiling";
        internal const string AttackTrigger = "attack";
        internal const string CanMove = "canMove";
    }
}
