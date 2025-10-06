using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;

    // §®§Ö§ä§à§Õ §Õ§Ý§ñ §Ú§Ù§Þ§Ö§ß§Ö§ß§Ú§ñ §ã§à§ã§ä§à§ñ§ß§Ú§ñ
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
