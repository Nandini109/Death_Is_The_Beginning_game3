using System;
using UnityEngine;

namespace Code.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "SOs/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
     
        [field: SerializeField, Range(0f, 20f),Header("Movement")] public float MovementSpeed { get; private set; } = 5f;
        
        [field: SerializeField, Range(1f, 20f)] public float MaxAcceleration { get; private set; } = 50f;
        [field: SerializeField, Range(1f, 20f)] public float MaxDeceleration { get; private set; } = 50f;
        [field: SerializeField, Range(0f, 20f)] public float MaxTurnSpeed { get; private set; } = 80f;
        
        [field: SerializeField, Range(0f, 20f), Header("Jump Settings")] public float JumpHeight { get; private set; } = 5f;
        [field: SerializeField, Range(0.2f, 5f)] public float TimeToApex { get; private set; }

        [field: SerializeField, Range(0.2f, 5f)]
        public float UpMovementMultiplier { get; private set; } = 1f;
        [field: SerializeField, Range(0.2f, 10f)] public float DownMovementMultiplier { get; private set; } = 1f;
        
        [field: SerializeField, Range(1.0f, 10f)] public float GravityJumpCutOff { get; private set; } = 1f;

        [field: SerializeField, Range(.5f, 2f)]
        public float DefaultGravityScale { get; private set; } = 1f;
        [field: SerializeField] public bool VariableJumpHeight { get; private set; }
        
        [field: SerializeField, Range(0.0f, 0.3f), Header("Other Settings")] public float JumpBufferTime { get; private set; } = .15f;

        [field: SerializeField, Range(0.0f, 0.3f)] public float CoyoteTime { get; private set; }

        [field: SerializeField, Range(0.0f, 2.0f)]
        public float PlayerDeathDelay { get; private set; } = .1f;

        [field: SerializeField, Range(5.0f, 50f)] public float TerminalVelocity { get; private set; } = 20f;
        [field: SerializeField, Range(0f, 10f), Header("In Air Movement")] public float AirMaxAcceleration { get; private set; } = 50f;
        [field: SerializeField, Range(0f, 100)] public float AirMaxDeceleration { get; private set; } = 50f;
        [field: SerializeField, Range(0f, 100f)] public float AirMaxTurnSpeed { get; private set; } = 80f;
       
        [HideInInspector]public bool IsGrounded { get; set; }
        [HideInInspector]public bool IsJumpHeld { get; set; }
        [HideInInspector] public bool IsJumping { get; set; } = false;
        [HideInInspector]public float MovementDirection { get; set; }
        [HideInInspector]public float Acceleration { get; set; }
        [HideInInspector]public Vector2 DesiredVelocity { get; set; }


        private void OnEnable()
        {
            _jumpBuffered = false;
            TimeJumpBuffered = -100000;
        }

        private bool _jumpBuffered = false;
        [HideInInspector] public float TimeJumpBuffered { get; set; } = -100000;
        [HideInInspector]public float TimeEnteredAir { get; set; }
        [HideInInspector]public float GravityMultiplier { get; set; }

        [HideInInspector]
        public bool JumpBuffered
        {
            get
            {
                _jumpBuffered = _jumpBuffered ?  Time.time - TimeJumpBuffered < JumpBufferTime : false;
                return _jumpBuffered;
            }
            set
            {
                _jumpBuffered = value;
                TimeJumpBuffered = _jumpBuffered? Time.time: -1000f;
            }
        }

    }
}