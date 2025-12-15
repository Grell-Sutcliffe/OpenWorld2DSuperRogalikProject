using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;

    // ���֧��� �էݧ� �ڧ٧ާ֧ߧ֧ߧڧ� �������ߧڧ�
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
