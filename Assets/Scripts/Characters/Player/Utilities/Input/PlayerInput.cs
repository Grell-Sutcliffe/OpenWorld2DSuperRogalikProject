using UnityEngine;
using UnityEngine.InputSystem;

// §¬§Ý§Ñ§ã§ã §Õ§Ý§ñ §å§Õ§à§Ò§ã§ä§Ó§Ñ §â§Ñ§Ò§à§ä§í §ã InputActions
public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }

    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        // §ª§ß§Ú§è§Ú§Ñ§Ý§Ú§Ù§Ñ§è§Ú§ñ InputActions
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player; // §¥§Ñ§Ý§î§ê§Ö §Þ§à§Ø§ß§à §à§Ò§â§Ñ§ë§Ñ§ä§î§ã§ñ §Ü §Õ§Ö§Û§ã§ä§Ó§Ú§ñ§Þ §é§Ö§â§Ö§Ù PlayerActions
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
