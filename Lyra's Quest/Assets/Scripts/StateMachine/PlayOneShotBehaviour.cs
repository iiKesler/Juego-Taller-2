using UnityEngine;

namespace StateMachine
{
    public class PlayOneShotBehaviour : StateMachineBehaviour
    {
        public AudioClip soundToPlay;
        public float volume = 1f;
        public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;
        
        // Delay sound timer
        public float playDelay = 0.5f;
        private float timeSinceEntered = 0;
        private bool hasDelayedSoundPlayed = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (playOnEnter)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
            }
            
            timeSinceEntered = 0f;
            hasDelayedSoundPlayed = false;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!playAfterDelay || hasDelayedSoundPlayed) return;
            timeSinceEntered += Time.deltaTime;

            if (!(timeSinceEntered >= playDelay)) return;
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
            hasDelayedSoundPlayed = true;
        }
    }
}
