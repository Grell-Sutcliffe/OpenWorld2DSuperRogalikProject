using UnityEngine;

public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        //Debug.Log("Enter Running State");
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);

        stateMachine.ReusableMovementData.MovementSpeedModifier = movementData.RunSpeedModifier;
    }


    override public void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    #endregion

}
