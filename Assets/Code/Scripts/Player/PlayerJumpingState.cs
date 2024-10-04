using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerJumpingState : PlayerBaseState
    {
        public PlayerJumpingState(PlayerController player) : base(player)
        {
            
        }
        public override void Enter()
        {
            
            _player.Data.GravityMultiplier = _player.Data.DefaultGravityScale;
            var jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _player.RB.gravityScale * _player.Data.JumpHeight);
            _player.Data.IsJumping = true;
            _player.DelayGroundCheck();
            _player.RB.velocity = new Vector2(_player.RB.velocity.x, jumpSpeed);

            _player.PlayerAnim.PlayAnimation(PlayerAnimationConstants.JUMP);
            _player.Data.TimeEnteredAir = Time.time;
            _player.EventData.HandlePlayerJumps(_player);
            
        }

        public override void Exit()
        {
            
        }

        public override void HandleJump()
        {

        }
        
        public override void HandleJumpExit()
        {
            if(_player.Data.VariableJumpHeight) _player.ChangeState(PlayerStates.InAir);
        }

        protected override void CalculateGravity()
        {
            _player.Data.GravityMultiplier = _player.Data.UpMovementMultiplier;

            if (_player.RB.velocity.y < -0.01f)
            {
                _player.ChangeState(PlayerStates.InAir);
            }
        }
        
    
        public override void SetXVelocity()
        {
            SetXVelocity(_player.Data.AirMaxTurnSpeed, _player.Data.AirMaxAcceleration, _player.Data.AirMaxDeceleration);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            _player.RB.velocity = new Vector2(_player.RB.velocity.x,
                _player.RB.velocity.y);
            if (_player.IsGrounded)
            {
                _player.ChangeState(Mathf.Abs(_player.Data.MovementDirection) > 0.01f
                    ? PlayerStates.Run
                    : PlayerStates.Idle);
            }
        }
    }
}