using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InteractionController : MonoBehaviour
{
    protected MainController mainController;

    public GameObject interactIcon;

    public SpriteRenderer interactIconSR;

    Color active = new Color(0.1f, 1.0f, 0.1f, 0.5f);
    Color deactive = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    public KeyCode interactKey = KeyCode.F;

    protected bool is_player_in_range = false;

    private InputAction interact;

    protected void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        OffInteraction();

        interactIconSR = interactIcon.GetComponent<SpriteRenderer>();
    }

    protected void OnEnable()
    {
        interact = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/f");
        interact.performed += OnInteract;
        interact.Enable();
    }

    protected void OnDisable()
    {
        interact.performed -= OnInteract;
        interact.Disable();
    }

    protected void OnInteract(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Нажата F");
        if (mainController.is_keyboard_active && is_player_in_range)
        {
            // mainController.PressF();
            Interact();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Player"))
        {
            is_player_in_range = true;
            OnInteraction();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            is_player_in_range = false;
            OffInteraction();
        }
    }

    protected abstract void Interact();

    protected void OnInteraction()
    {
        if (!mainController.list_of_interactable_objects_names.Contains(gameObject.name))
        {
            interactIcon.SetActive(true);

            mainController.list_of_interactable_SR.Add(interactIconSR);
            mainController.list_of_interactable_objects_names.Add(gameObject.name);

            mainController.ShowInteraction();
        }
    }

    protected void OffInteraction()
    {
        interactIcon.SetActive(false);

        mainController.list_of_interactable_SR.Remove(interactIconSR);
        mainController.list_of_interactable_objects_names.Remove(gameObject.name);

        mainController.ShowInteraction();
    }

    protected void InteractIconActivate()
    {
        interactIconSR.color = active;
    }

    protected void InteractIconDeactivate()
    {
        interactIconSR.color = deactive;
    }
}
