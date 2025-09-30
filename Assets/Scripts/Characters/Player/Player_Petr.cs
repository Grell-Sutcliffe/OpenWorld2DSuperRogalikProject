using UnityEngine;

public class Player : MonoBehaviour
{
    // ���ѧߧߧ�� �ڧԧ��ܧ�
    [field: SerializeField] public PlayerSO Data { get; private set; }

    // ����ާ��ߧ֧ߧ� �էݧ� ���ݧ��֧ߧڧ� �ӧӧ�է� �ڧԧ��ܧ�
    public PlayerInput Input { get; private set; }

    // ����ާ��ߧ֧ߧ� �ѧߧڧާѧ�ڧ� �ڧԧ��ܧ�
    public Animator PlayerAnimator;

    public Rigidbody2D Rigidbody2D { get; private set; }

    public SpriteRenderer SpriteRenderer { get; private set; }

    // ���ӧ��ާѧ� �������ߧڧ� �էݧ� ����ѧӧݧ֧ߧڧ� �էӧڧا֧ߧڧ� �ڧԧ��ܧ�
    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        // ���ߧڧ�ڧѧݧڧ٧ѧ�ڧ� �ܧ�ާ��ߧ֧ߧ���
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
