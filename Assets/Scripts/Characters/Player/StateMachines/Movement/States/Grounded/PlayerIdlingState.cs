using System;
using UnityEngine;

public class PlayerIdlingState : PlayerGroundedState
{
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        //Debug.Log("Enter Idling State");
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

        stateMachine.ReusableMovementData.MovementSpeedModifier = 0f;
        ResetVelocity();
    }

    override public void Exit()
    {
        base.Exit();
        
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.ReusableMovementData.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.RunningState);
        }
    }

    #endregion
    private void ResetVelocity()
    {
        stateMachine.Player.Rigidbody2D.linearVelocity = Vector2.zero;
    }
}
