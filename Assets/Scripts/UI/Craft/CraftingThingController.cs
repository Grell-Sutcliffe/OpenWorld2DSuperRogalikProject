using UnityEngine;

public class CraftingThingController : InteractionController
{
    protected override void Start()
    {
        base.Start();
        is_interactable = true;
    }

    protected override void Interact()
    {
        mainController.OpenCraftPanel();
    }
}
