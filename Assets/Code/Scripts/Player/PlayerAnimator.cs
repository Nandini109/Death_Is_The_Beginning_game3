using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _animator.Play(PlayerAnimationConstants.IDLE);
        }

        public void PlayAnimation(string animationName)
        {
            _animator?.Play(animationName);
        }
    }

    public static class PlayerAnimationConstants
    {
        public const string IDLE = "Idle";
        public const string WALK = "Walk";
        public const string RUN = "Run";
        public const string AIR = "JumpMid";
        public const string JUMP = "Jump";
    }
}