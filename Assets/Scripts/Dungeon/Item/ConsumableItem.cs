using System.Xml.Linq;
using UnityEngine;

public class ConsumableItem : Item
{
    ConsumableItemSO data;

    public ConsumableItem(ConsumableItemSO data, int id)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.item_name;
        this.description = data.description;

        this.item_type = data.item_type;

        this.amount = data.amount;

        this.id = id;
    }

    public ConsumableItem(ConsumableItemSO data, int id, int amount)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.item_name;
        this.description = data.description;

        this.item_type = data.item_type;

        this.amount = amount;

        this.id = id;
    }

    /*
    public virtual void OnPickup(GameObject player)
    {
        Debug.Log($"Подобран расходник {name} x{amount}");
        Destroy(gameObject);
    }
    */
}