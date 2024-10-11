using Code.Scripts.Player;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    float elapsedTime;
    float resetTime = 0.29f;
    public PlayerHurtState(PlayerController player) : base(player)
    {
    }
    public override void Enter()
    {
        _player.PlayerAnim.PlayAnimation(PlayerAnimationConstants.HURT);
        elapsedTime = 0f;
        _player.StartCoroutine(GoIdle());
    }
    public override void Update()
    {
        base.Update();

    }
    private IEnumerator GoIdle()
    {

        while (elapsedTime < resetTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _player.ChangeState(PlayerStates.Idle);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }
    public override void Exit()
    {
        base.Exit();
    }
}
