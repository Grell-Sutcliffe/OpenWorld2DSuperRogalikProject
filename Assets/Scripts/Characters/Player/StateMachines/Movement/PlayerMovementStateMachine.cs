using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableMovementData { get; }
    public PlayerIdlingState IdlingState { get; }
    public PlayerRunningState RunningState { get; }
    //public PlayerDashingState DashingState { get; }

    public PlayerMovementStateMachine(Player player)
    {
        Player = player;
        ReusableMovementData = new PlayerStateReusableData();
        IdlingState = new PlayerIdlingState(this);
        RunningState = new PlayerRunningState(this);
        //DashingState = new PlayerDashingState(this);
    }

}
