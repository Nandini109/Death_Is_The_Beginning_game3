using System;
using Code.Scripts.StateMachine;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using State = Code.Scripts.StateMachine.State;

namespace Code.Scripts.Player
{
    public enum PlayerStates{Idle, Run, Jumping, InAir}

    public class PlayerInfo
    {
        public bool IsGrounded { get; set; }
        public bool IsJumpHeld { get; set; }
        public float MovementDirection { get; set; }
    }

    public class PlayerController : BaseStateMachine
    {
        #region Serialize Fields
        [field: SerializeField] public PlayerData Data { get; private set; }
        [field: SerializeField] public PlayerEventData EventData { get; private set; }
        #endregion
        #region Public-ish Info

        public PlayerInfo Info { get; set; }
        public PlayerAnimator PlayerAnim { get; private set; }
        public Rigidbody2D RB { get; private set; }
        public bool IsRunning { get; private set; } = false;
       
        public bool IsGrounded => _groundCheck.IsGrounded;
        public void DelayGroundCheck ()=> _groundCheck.DelayGrounding();
        #endregion
        #region  Player State Info
        private PlayerBaseState _playerState;
        private PlayerIdleState _idleState;
        private PlayerRunState _runState;
        private PlayerJumpingState _jumpingState;
        private PlayerInAirState _inAirState;
        

        #endregion
        
        #region Private Variables
        private GroundCheck _groundCheck;

        #endregion

        public override void ChangeState(State newState)
        {
            base.ChangeState(newState);
        }

        public void ChangeState(PlayerStates newState)
        {
            switch (newState)
            {
                case PlayerStates.Idle:
                    ChangeState(_idleState);
                    break;
                case PlayerStates.Run:
                    ChangeState(_runState);
                    break;
                case PlayerStates.Jumping:
                    ChangeState(_jumpingState);
                    break;
                case PlayerStates.InAir:
                    ChangeState(_inAirState);
                    break;
            }
        }

        private void Start()
        {
            RB = GetComponent<Rigidbody2D>();
            PlayerAnim = GetComponent<PlayerAnimator>();
            _groundCheck = GetComponent<GroundCheck>();
            EventData.HandlePlayerSpawn(this);
            StateSetup();
            ChangeState(PlayerStates.InAir);
            _groundCheck.GroundChanged += (val) =>
            {
                if (!val) Data.TimeEnteredAir = Time.time;
            };
            

        }

        private void StateSetup()
        {
            _idleState = new PlayerIdleState(this);
            _runState = new PlayerRunState(this);
            _jumpingState = new PlayerJumpingState(this);
            _inAirState = new PlayerInAirState(this);
        }

        public void HandleMovement(Vector2 movement)
        {
            
            Data.MovementDirection = movement.x;
        }

        public void HandleJump()
        {
            ((PlayerBaseState)_currentState).HandleJump();
        }

        public void CancelJump()
        {
            
             ((PlayerBaseState)_currentState).HandleJumpExit();
        }

        public void HandleRun(bool isRunning)
        {
            IsRunning = isRunning;
        }
        protected override void Update()
        {
            base.Update();
            EventData.HandlePlayerUpdate(this);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.GetComponent<SpriteRenderer>().flipX = RB.velocity.x < -0.02f;

        }

        public void Death()
        {
            RB.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("ActualDeath", Data.PlayerDeathDelay);
        }

        private void ActualDeath()
        {
            EventData.HandlePlayerDeath(this);
            Destroy(this);
        }
    }
}