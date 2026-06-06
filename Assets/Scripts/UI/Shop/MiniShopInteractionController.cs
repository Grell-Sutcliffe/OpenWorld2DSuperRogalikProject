using UnityEngine;

public class MiniShopInteractionController : InteractionController
{
    protected override void Start()
    {
        base.Start();
        is_interactable = true;
    }

    protected override void Interact()
    {
        mainController.OpenMiniShopPanel();
    }
}
