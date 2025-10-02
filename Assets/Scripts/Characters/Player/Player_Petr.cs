using UnityEngine;

public class Player : MonoBehaviour
{
    // Player's Data
    [Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    // Component for getting player input
    public PlayerInput Input { get; private set; }

    [Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }


    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    // §¡§Ó§ä§à§Þ§Ñ§ä §ã§à§ã§ä§à§ñ§ß§Ú§Û §Õ§Ý§ñ §å§á§â§Ñ§Ó§Ý§Ö§ß§Ú§ñ §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ §Ú§Ô§â§à§Ü§Ñ
    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        // §ª§ß§Ú§è§Ú§Ñ§Ý§Ú§Ù§Ñ§è§Ú§ñ §Ü§à§Þ§á§à§ß§Ö§ß§ä§à§Ó
        Input = GetComponent<PlayerInput>();
        movementStateMachine = new PlayerMovementStateMachine(this);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        AnimationData.Initialize();
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
