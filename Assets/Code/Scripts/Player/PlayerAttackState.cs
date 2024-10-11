using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        // Handle jump input during the attack
        if (Input.GetButtonDown("Jump")) // Make sure this matches your Input System
        {
            HandleJump();
        }

        // Add any logic you want while the attack is happening, like cooldown or hit detection
    }

    public override void FixedUpdate()
    {
      
            _player.ChangeState(PlayerStates.Idle);
        
    }
}
