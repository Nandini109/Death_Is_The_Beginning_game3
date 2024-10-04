using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerInAirState : PlayerBaseState
    {

        public PlayerInAirState(PlayerController player):base(player)
        {
            
        }

        public override void Enter()
        {
            _player.PlayerAnim.PlayAnimation(PlayerAnimationConstants.AIR);
            _player.EventData.HandlePlayerInAir(_player);
        }

        public override void Exit()
        {
        }

        public override void HandleJump()
        {
            if ((Time.time - _player.Data.TimeEnteredAir) < _player.Data.CoyoteTime && !_player.Data.IsJumping)
            {
                Debug.Log("Coyote jump kicked in");
                base.HandleJump();
            }
        }

        public override void HandleJumpExit()
        {
            
        }

        protected override void CalculateGravity()
        {
            _player.Data.GravityMultiplier = _player.RB.velocity.y < -0.01f
                ? _player.Data.DownMovementMultiplier
                : _player.Data.GravityJumpCutOff;
        }
        public override void SetXVelocity()
        {
            SetXVelocity(_player.Data.AirMaxTurnSpeed, _player.Data.AirMaxAcceleration, _player.Data.AirMaxDeceleration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate(); 
            _player.RB.velocity = new Vector2(_player.RB.velocity.x, Mathf.Clamp(_player.RB.velocity.y, -_player.Data.TerminalVelocity, 100f));
            
            if(_player.IsGrounded)
                _player.ChangeState(Mathf.Abs(_player.Data.MovementDirection) > 0.01f ? PlayerStates.Run : PlayerStates.Idle);
        }
    }
}