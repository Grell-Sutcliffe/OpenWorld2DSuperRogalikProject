using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected PlayerMovementData movementData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;

        movementData = stateMachine.Player.Data.MovementData;
    }

    #region IState Methods
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void Update()
    {
        HandleFlip();
    }

    #endregion

    #region Main Methods

    // ����ߧ�ӧߧѧ� �ݧ�ԧڧܧ� �էӧڧا֧ߧڧ�
    private void Move()
    {
        if (stateMachine.ReusableMovementData.MovementInput == Vector2.zero || stateMachine.ReusableMovementData.MovementSpeedModifier == 0f)
        {
            return;
        }

        Vector2 movementDirection = GetMovementDirection();
        float movementSpeed = movementData.BaseSpeed * stateMachine.ReusableMovementData.MovementSpeedModifier;
        Vector2 movementVelocity = movementDirection * movementSpeed;
        stateMachine.Player.Rigidbody2D.linearVelocity = movementVelocity;

        SetAnimationMovingDirection(movementDirection);
    }

    private void HandleFlip()
    {
        if (stateMachine.ReusableMovementData.MovementInput.x < 0 && !stateMachine.Player.SpriteRenderer.flipX)
        {
            stateMachine.Player.SpriteRenderer.flipX = true;
        }
        else if (stateMachine.ReusableMovementData.MovementInput.x > 0 && stateMachine.Player.SpriteRenderer.flipX)
        {
            stateMachine.Player.SpriteRenderer.flipX = false;
        }
    }

    private Vector2 GetMovementDirection()
    {
        return new Vector2(stateMachine.ReusableMovementData.MovementInput.x, stateMachine.ReusableMovementData.MovementInput.y);
    }

    #endregion

    #region Input Methods
    private void ReadMovementInput()
    {
        stateMachine.ReusableMovementData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        // ����� ���ڧާ֧� �է�ҧѧӧݧ֧ߧڧ� �ܧ�ݧҧ�ܧ� �ߧ� ���ާ֧ߧ� �էӧڧا֧ߧڧ�
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMoveCanceled;
    }


    protected virtual void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMoveCanceled;
    }

    // ����ݧҧ�� �ߧ� ���ާ֧ߧ� �էӧڧا֧ߧڧ�
    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    #endregion

    #region Reusable Methods

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.PlayerAnimator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.PlayerAnimator.SetBool(animationHash, false);
    }

    private void SetAnimationMovingDirection(Vector2 movementInput)
    {
        stateMachine.Player.PlayerAnimator.SetFloat(stateMachine.Player.AnimationData.MoveXParameterHash, Math.Abs(movementInput.x));
        stateMachine.Player.PlayerAnimator.SetFloat(stateMachine.Player.AnimationData.MoveYParameterHash, movementInput.y);
    }

    #endregion


}
