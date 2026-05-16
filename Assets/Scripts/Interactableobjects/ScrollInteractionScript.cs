using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollInteractionScript : MonoBehaviour
{
    MainController mainController;

    Color active = new Color(0.1f, 1.0f, 0.1f, 0.5f);
    Color deactive = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    [SerializeField]
    private InputAction scrollY = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll/y");

    public int current_index = 0;

    private void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        if (scrollY == null || scrollY.bindings.Count == 0)
        {
            scrollY = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll/y");
        }

        ApplyAllColors();
    }

    void OnEnable()
    {
        scrollY.performed += OnScroll;
        scrollY.Enable();
    }

    void OnDisable()
    {
        scrollY.performed -= OnScroll;
        scrollY.Disable();
    }

    private void OnScroll(InputAction.CallbackContext ctx)
    {
        //mainController.ShowInteraction();

        float y = ctx.ReadValue<float>();
        if (y > 0.01f) SelectPrev();
        else if (y < -0.01f) SelectNext();
    }

    void SelectNext()
    {
        if (mainController.list_of_interactable_SR.Count == 0) return;
        current_index = (current_index + 1) % mainController.list_of_interactable_SR.Count;
        ApplyAllColors();
    }

    void SelectPrev()
    {
        if (mainController.list_of_interactable_SR.Count == 0) return;
        current_index = (current_index - 1 + mainController.list_of_interactable_SR.Count) % mainController.list_of_interactable_SR.Count;
        ApplyAllColors();
    }

    public void ApplyAllColors()
    {
        for (int i = 0; i < mainController.list_of_interactable_SR.Count; i++)
        {
            var r = mainController.list_of_interactable_SR[i];
            if (!r) continue;
            SetRendererColor(r, i == current_index ? active : deactive);
        }
    }

    void SetRendererColor(SpriteRenderer r, Color c)
    {
        r.color = c;
    }
}
