using System.Xml.Linq;
using UnityEngine;

public class ConsumableItem : Item
{
    ConsumableItemSO data;

    public int time_to_grow;

    /*
    public ConsumableItem(ConsumableItemSO data) : base(data)
    {
        //this.data = data;
    }
    */

    public ConsumableItem(ConsumableItemSO data, int id) : base(data)
    {
        this.amount = data.amount;

        this.time_to_grow = data.time_to_grow;
        
        /*
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.item_name;
        this.description = data.description;

        this.item_type = data.item_type;

        this.id = id;
        */
    }

    public ConsumableItem(ConsumableItemSO data, int id, int amount) : base(data)
    {
        this.amount = amount;

        this.time_to_grow = data.time_to_grow;

        /*
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.item_name;
        this.description = data.description;

        this.item_type = data.item_type;

        this.id = id;
        */
    }

    /*
    public virtual void OnPickup(GameObject player)
    {
        Debug.Log($"Подобран расходник {name} x{amount}");
        Destroy(gameObject);
    }
    */
}