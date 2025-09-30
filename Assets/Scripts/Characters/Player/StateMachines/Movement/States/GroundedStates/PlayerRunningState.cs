using UnityEngine;

public class PlayerRunningState : PlayerMovementState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        Debug.Log("Enter Running State");
        base.Enter();
        stateMachine.ReusableMovementData.MovementSpeedModifier = movementData.RunSpeedModifier;
    }
    #endregion

}
