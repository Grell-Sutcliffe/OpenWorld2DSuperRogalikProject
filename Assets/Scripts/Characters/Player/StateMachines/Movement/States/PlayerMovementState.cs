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
        // §¶§å§ß§Ü§è§Ú§ñ §Õ§Ý§ñ §Õ§à§Ò§Ñ§Ó§Ý§Ö§ß§Ú§ñ §Ü§à§Ý§Ò§ï§Ü§à§Ó §ß§Ñ §Õ§Ö§Û§ã§ä§Ó§Ú§ñ §Ó§Ó§à§Õ§Ñ
        // §¯§Ñ§á§â§Ú§Þ§Ö§â §à§ä§Þ§Ö§ß§Ñ §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ, §é§ä§à§Ò§í §á§Ö§â§Ö§Û§ä§Ú §Ó §ã§à§ã§ä§à§ñ§ß§Ú§Ö Idling
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        // §¶§å§ß§Ü§è§Ú§ñ §Õ§Ý§ñ §å§Õ§Ñ§Ý§Ö§ß§Ú§ñ §Ü§à§Ý§Ò§ï§Ü§à§Ó §ß§Ñ §Õ§Ö§Û§ã§ä§Ó§Ú§ñ §Ó§Ó§à§Õ§Ñ
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

    // §°§ã§ß§à§Ó§ß§Ñ§ñ §Ý§à§Ô§Ú§Ü§Ñ §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ
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
    }

    private void HandleFlip()
    {
        if (stateMachine.ReusableMovementData.MovementInput.x < 0)
        {
            stateMachine.Player.SpriteRenderer.flipX = true;
        }
        else if (stateMachine.ReusableMovementData.MovementInput.x > 0)
        {
            stateMachine.Player.SpriteRenderer.flipX = false;
        }
    }

    private Vector2 GetMovementDirection()
    {
        return new Vector2(stateMachine.ReusableMovementData.MovementInput.x, stateMachine.ReusableMovementData.MovementInput.y);
    }

    private void Rotate(Vector2 movementDirection)
    {
        // §´§å§ä §á§à§Ü§Ñ §ß§Ö §à§é§Ö§ß§î §Ü§â§Ñ§ã§Ú§Ó§à
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg - 90;
        stateMachine.Player.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    #endregion

    #region Input Methods
    private void ReadMovementInput()
    {
        stateMachine.ReusableMovementData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        // §£§à§ä §á§â§Ú§Þ§Ö§â §Õ§à§Ò§Ñ§Ó§Ý§Ö§ß§Ú§ñ §Ü§à§Ý§Ò§ï§Ü§Ñ §ß§Ñ §à§ä§Þ§Ö§ß§å §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMoveCanceled;
    }


    protected virtual void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMoveCanceled;
    }

    // §¬§à§Ý§Ò§ï§Ü §ß§Ñ §à§ä§Þ§Ö§ß§å §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ
    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    #endregion


}
