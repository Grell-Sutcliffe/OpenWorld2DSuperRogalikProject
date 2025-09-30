using System;
using UnityEngine;

public class PlayerIdlingState : PlayerMovementState
{
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        Debug.Log("Enter Idling State");
        base.Enter();
        stateMachine.ReusableMovementData.MovementSpeedModifier = 0f;
        ResetVelocity();
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
