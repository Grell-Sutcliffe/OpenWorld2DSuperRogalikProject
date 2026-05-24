using UnityEngine;

public class PoolController : InteractionController
{
    public ConsumableItemSO consumableItemSO;

    protected override void Interact()
    {
        backpackController.IncreaceItemByName(consumableItemSO.item_name);
    }
}
