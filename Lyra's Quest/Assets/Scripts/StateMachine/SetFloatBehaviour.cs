using UnityEngine;
using UnityEngine.Animations;

namespace StateMachine
{
    public class SetFloatBehaviour : StateMachineBehaviour
    {
        public string floatName;
        public bool updateOnStateEnter, updateOnStateExit;
        public bool updateOnStateMachineEnter, updateOnStateMachineExit;
        public float valueOnEnter, valueOnExit;
    
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnStateEnter)
            {
                animator.SetFloat(floatName, valueOnEnter);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnStateExit)
            {
                animator.SetFloat(floatName, valueOnExit);
            }
        }
        
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
        {
            if (updateOnStateMachineEnter)
                animator.SetFloat(floatName, valueOnEnter);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
        {
            if (updateOnStateMachineExit)
                animator.SetFloat(floatName, valueOnExit);
        }
    }
}
