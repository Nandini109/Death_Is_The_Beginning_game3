using System;
using Code.Scripts.StateMachine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerBaseState : State
    {
        protected PlayerController _player;
        public PlayerBaseState(PlayerController player)
        {
            _player = player;
        }
        

        public virtual void HandleJump()
        {
            if (!_player.Data.IsJumping)
            {
                _player.ChangeState(PlayerStates.Jumping);
            }
        }
        
        public virtual void CancelJump()
        {
            
        }

        public virtual void HandleJumpExit()
        {
            
        }
        protected virtual void SetPhysics()
        {
            var newGravity = -2f * _player.Data.JumpHeight / (_player.Data.TimeToApex * _player.Data.TimeToApex);
            _player.RB.gravityScale = (newGravity / Physics2D.gravity.y) * _player.Data.GravityMultiplier;
        }

        protected virtual void CalculateGravity()
        {
            _player.Data.GravityMultiplier = _player.Data.DefaultGravityScale;
        }

        public virtual void SetXVelocity()
        {
            SetXVelocity(_player.Data.MaxTurnSpeed, _player.Data.MaxAcceleration, _player.Data.MaxDeceleration);
        }
        public virtual void SetXVelocity(float turnSpeed, float maxAcceleration, float maxDeceleration)
        {
            var maxSpeedChange=1f;
            var desiredVelocity = _player.Data.MovementSpeed * _player.Data.MovementDirection;
            //check to see first if we are moving the controller at all
            if (Mathf.Abs(_player.Data.MovementDirection) > 0.05f)
            {
                //check to see if we are turning (we are turning when we are going a different direction on our controller; this is stored in the MovementDirection
                if(_player.Data.MovementDirection * _player.RB.velocity.x < 0)
                {
                    maxSpeedChange = turnSpeed; //we tend to accelerate faster when turning
                }
                else
                {
                    maxSpeedChange = maxAcceleration;
                }
            }
            else //if we are not using the controller we are decelerating
            {
                maxSpeedChange = maxDeceleration;
            }

            maxSpeedChange *= Time.fixedDeltaTime;
            _player.RB.velocity = new Vector2(Mathf.MoveTowards(_player.RB.velocity.x, desiredVelocity, maxSpeedChange),
                _player.RB.velocity.y);
        }
        public override void Update()
        {
            SetPhysics();
        }
        public override void FixedUpdate()
        {
            SetXVelocity();
            CalculateGravity();
            
        }
        
    }
}