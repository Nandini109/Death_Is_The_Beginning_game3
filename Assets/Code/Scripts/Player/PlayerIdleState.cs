using System;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerController player) : base(player)
        {
            
        }
        public override void Enter()
        {
            _player.Data.IsJumping = false;
            _player.PlayerAnim.PlayAnimation(PlayerAnimationConstants.IDLE);
            if(_player.Data.JumpBuffered)
                base.HandleJump();
            _player.EventData.HandlePlayerIdles(_player);
        }
        public override void Update()
        {
            base.Update();
        }

        public override void HandleJump()
        {
            base.HandleJump();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!_player.IsGrounded)
            {
                _player.ChangeState(PlayerStates.InAir);
                return;
            }
            if (MathF.Abs(_player.Data.MovementDirection) > 0)
            {
                _player.ChangeState(PlayerStates.Run);
            }
            
        }
    }
}