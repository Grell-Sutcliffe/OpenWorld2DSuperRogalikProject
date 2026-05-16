using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InteractKeyListener : MonoBehaviour
{
    /*
    MainController mainController;

    private InputAction interact;

    private void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    void OnEnable()
    {
        interact = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/f");
        interact.performed += OnInteract;
        interact.Enable();
    }

    void OnDisable()
    {
        interact.performed -= OnInteract;
        interact.Disable();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Нажата F");
        if (mainController.is_keyboard_active)
        {
            mainController.PressF();
        }
    }
    */
}
