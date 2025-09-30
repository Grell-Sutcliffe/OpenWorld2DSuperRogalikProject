using UnityEngine;
using UnityEngine.InputSystem;

// ���ݧѧ�� �էݧ� ��է�ҧ��ӧ� ��ѧҧ��� �� InputActions
public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }

    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        // ���ߧڧ�ڧѧݧڧ٧ѧ�ڧ� InputActions
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player; // ���ѧݧ��� �ާ�اߧ� ��ҧ�ѧ�ѧ���� �� �է֧ۧ��ӧڧ�� ��֧�֧� PlayerActions
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
