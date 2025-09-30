using UnityEngine;

public class Player : MonoBehaviour
{
    // §¥§Ñ§ß§ß§í§Ö §Ú§Ô§â§à§Ü§Ñ
    [field: SerializeField] public PlayerSO Data { get; private set; }

    // §¬§à§Þ§á§à§ß§Ö§ß§ä §Õ§Ý§ñ §á§à§Ý§å§é§Ö§ß§Ú§ñ §Ó§Ó§à§Õ§Ñ §Ú§Ô§â§à§Ü§Ñ
    public PlayerInput Input { get; private set; }

    // §¬§à§Þ§á§à§ß§Ö§ß§ä §Ñ§ß§Ú§Þ§Ñ§è§Ú§Ú §Ú§Ô§â§à§Ü§Ñ
    public Animator PlayerAnimator;

    public Rigidbody2D Rigidbody2D { get; private set; }

    public SpriteRenderer SpriteRenderer { get; private set; }

    // §¡§Ó§ä§à§Þ§Ñ§ä §ã§à§ã§ä§à§ñ§ß§Ú§Û §Õ§Ý§ñ §å§á§â§Ñ§Ó§Ý§Ö§ß§Ú§ñ §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ §Ú§Ô§â§à§Ü§Ñ
    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        // §ª§ß§Ú§è§Ú§Ñ§Ý§Ú§Ù§Ñ§è§Ú§ñ §Ü§à§Þ§á§à§ß§Ö§ß§ä§à§Ó
        Input = GetComponent<PlayerInput>();
        PlayerAnimator = GetComponent<Animator>();
        movementStateMachine = new PlayerMovementStateMachine(this);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();

        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }

}
