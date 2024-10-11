using System;
using Code.Scripts.StateMachine;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static Cinemachine.DocumentationSortingAttribute;
using static UnityEditor.Experimental.GraphView.GraphView;
using State = Code.Scripts.StateMachine.State;
using UnityEngine.UI;

namespace Code.Scripts.Player
{
    public enum PlayerStates{Idle, Run, Jumping, InAir, Attacking, Hurt, Dead}

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
        private PlayerAttackState1 _attackState1;
        private PlayerHurtState _hurtState;
        private PlayerDeadState _deadState;


        #endregion

        #region Private Variables
        private GroundCheck _groundCheck;

        #endregion


        private float normalGravity = 1f;
        private float flippedGravity = -1f;

        private bool isFlipped = false;
        private bool isAttacking = false;
        private int lives = 3;       

        [SerializeField] private float flipJumpForce = 5f;
        [SerializeField] private Image[] lifeSprites;
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
                case PlayerStates.Attacking:
                    ChangeState(_attackState1);
                    break;
                case PlayerStates.Hurt:
                    ChangeState(_hurtState);
                    break;
                case PlayerStates.Dead:
                    ChangeState(_deadState);
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

            
            var newGravity = -2f * Data.JumpHeight / (Data.TimeToApex * Data.TimeToApex);
            RB.gravityScale = (newGravity / Physics2D.gravity.y) * Data.GravityMultiplier;

        }

        private void StateSetup()
        {
            _idleState = new PlayerIdleState(this);
            _runState = new PlayerRunState(this);
            _jumpingState = new PlayerJumpingState(this);
            _inAirState = new PlayerInAirState(this);
            _attackState1 = new PlayerAttackState1(this);
            _hurtState = new PlayerHurtState(this);
            _deadState = new PlayerDeadState(this);
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
            //Debug.Log(RB.gravityScale);
            EventData.HandlePlayerUpdate(this);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.GetComponent<SpriteRenderer>().flipX = RB.velocity.x < -0.02f;

        }

      
        public void Death()
        {
            //RB.constraints = RigidbodyConstraints2D.FreezeAll;
            //Invoke("ActualDeath", Data.PlayerDeathDelay);
            lives--; 

            if (lives > 0)
            {
                ((PlayerBaseState)_currentState).HurtState();

                Respawn();
                UpdateLivesUI();
                Debug.Log("Lives left:" + lives);
            }
            else
            {
                
                ActualDeath();
            }
        }

        private void ActualDeath()
        {
            ((PlayerBaseState)_currentState).DeadState();

            EventData.HandlePlayerDeath(this);
            Destroy(this);
        }

        private void Respawn()
        {
            Debug.Log("Player Respawn");
            Vector2 checkpointPosition = CheckPointManager.Instance.GetCurrentCheckpoint();
            transform.position = checkpointPosition;
        }

        public void OnFlipGravity(InputAction.CallbackContext context)
        {
            Debug.Log("I'm trying to flip the player");

            if (_groundCheck.IsGrounded == true)
            {
            
            if (isFlipped == false)
            {
                
                Debug.Log("Player Flipped");
                RB.gravityScale = flippedGravity;
                transform.localScale = new Vector3(5, -5, 1);
                RB.AddForce(new Vector2(0, flipJumpForce), ForceMode2D.Impulse);
                isFlipped = true;
                
            }
            else
            {
                Debug.Log("Player is back to normal");
                RB.gravityScale = normalGravity;
                transform.localScale = new Vector3(5, 5, 1);
                RB.AddForce(new Vector2(0, -flipJumpForce), ForceMode2D.Impulse);
                isFlipped = false;
            }
            }
        }

        public void OnSwordAttack()
        {
            
            if (_currentState is PlayerRunState || _currentState is PlayerIdleState)
            {
                ((PlayerBaseState)_currentState).SwordAttack();
                Debug.Log("Attack");
                isAttacking = true;
                
            }
        }
        public void OnSwordAttackCancelled()
        {
            Debug.Log("Cancled");
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isAttacking == true)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    //Debug.Log("Dieeeee");
                    isAttacking = false;
                }
            }
        }

        private void UpdateLivesUI()
        {
            if (lives >= 0 && lives < lifeSprites.Length)
            {
                lifeSprites[lives].enabled = false; 
            }
        }
    }
    
}