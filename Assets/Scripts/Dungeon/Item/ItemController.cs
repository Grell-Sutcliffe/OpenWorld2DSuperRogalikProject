using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ItemController : InteractionController
{
    public ConsumableItemSO data;
    private Item item;

    private void Awake()
    {
        base.Awake();

        string item_name = data.item_name;
        item = mainController.GetItemByName(item_name);
    }

    protected override void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        mainController.PickUpItemByName(item.item_name);
        Destroy(gameObject);
    }
}
