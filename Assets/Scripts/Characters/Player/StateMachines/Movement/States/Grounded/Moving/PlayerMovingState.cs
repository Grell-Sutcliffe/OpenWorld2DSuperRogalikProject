using UnityEngine;

public class PlayerMovingState : PlayerMovementState
{
    public PlayerMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    override public void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
    }

    override public void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
    }
}
