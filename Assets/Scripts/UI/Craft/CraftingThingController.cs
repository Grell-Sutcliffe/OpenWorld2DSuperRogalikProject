using UnityEngine;

public class CraftingThingController : InteractionController
{
    /*
    protected void Start()
    {
        base.Start();
    }
    */

    protected override void Interact()
    {
        mainController.OpenCraftPanel();
    }
}
