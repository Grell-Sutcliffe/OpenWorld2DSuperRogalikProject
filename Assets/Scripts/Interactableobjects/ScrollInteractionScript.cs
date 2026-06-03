using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();
        if (mainController.list_of_interactable_objects_names == null || mainController.list_of_interactable_SR == null) return;

        //Debug.Log($"current_index = {current_index},    list_of_interactable_SR.Count = {mainController.list_of_interactable_SR.Count},    list_of_interactable_objects_names.Count = {mainController.list_of_interactable_objects_names.Count}\n" +
        //    $"{string.Join(", ", mainController.list_of_interactable_objects_names)}");

        int check_amount_of_iterations = 100;
        bool is_everything_OK = false;
        bool need_to_delete = false;

        while (!is_everything_OK)
        {
            for (int i = 0; i < mainController.list_of_interactable_SR.Count; i++)
            {
                if (mainController.list_of_interactable_objects_names[i] == string.Empty)
                {
                    need_to_delete = true;

                    mainController.list_of_interactable_objects_names.RemoveAt(i);
                    mainController.list_of_interactable_SR.RemoveAt(i);

                    break;
                }
            }

            if (!need_to_delete)
            {
                is_everything_OK = true;
            }

            check_amount_of_iterations--;
            if (check_amount_of_iterations < 0)
            {
                Debug.LogError("ERROR : Too much iterations of trying to remove unused name!!!");
                break;
            }
        }

        bool active_was_found = false;

        for (int i = 0; i < mainController.list_of_interactable_SR.Count; i++)
        {
            SpriteRenderer r = mainController.list_of_interactable_SR[i];
            if (!r) Debug.LogError("ERROR : SpriteRenderer was not found!!!");

            if (i == current_index)
            {
                SetRendererColor(r, active);
                active_was_found = true;
            }
            else
            {
                SetRendererColor(r, deactive);
            }

            //SetRendererColor(r, i == current_index ? active : deactive);
        }

        if (mainController.list_of_interactable_SR.Count > 0 && !active_was_found) Debug.LogError("ERROR : Active interaction was not found!!!");
    }

    void SetRendererColor(SpriteRenderer r, Color c)
    {
        r.color = c;
    }
}
