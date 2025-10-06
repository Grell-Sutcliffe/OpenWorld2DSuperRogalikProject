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

    // ���ӧ��ާѧ� �������ߧڧ� �էݧ� ����ѧӧݧ֧ߧڧ� �էӧڧا֧ߧڧ� �ڧԧ��ܧ�
    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        // ���ߧڧ�ڧѧݧڧ٧ѧ�ڧ� �ܧ�ާ��ߧ֧ߧ���
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
