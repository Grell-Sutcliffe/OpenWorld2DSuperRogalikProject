public class ItemController : InteractionController
{
    public ConsumableItemSO data;
    private Item item;

    protected override void Awake()
    {
        base.Awake();

        string item_name = data.item_name;
        item = mainController.GetItemByName(item_name);

        is_interactable = true;
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
