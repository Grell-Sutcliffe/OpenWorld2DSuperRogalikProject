using UnityEngine;
using static ShopPanelScript;

public class ItemForSale : Item
{
    public Cost cost;

    ItemForSaleSO data;

    public ItemForSale(ItemForSaleSO data, int id)
    {
        this.data = data;

        this.item_name = data.item_name;
        this.description = data.description;
        this.sprite = data.sprite;

        this.item_type = data.item_type;

        //this.cost = data.cost;
        this.cost = new Cost(data.cost_amount, data.cost_type);

        this.amount = 0;

        this.id = id;
    }

    public ItemForSale(ItemForSaleSO data, int id, int amount)
    {
        this.data = data;

        this.item_name = data.item_name;
        this.description = data.description;
        this.sprite = data.sprite;

        this.item_type = data.item_type;

        //this.cost = data.cost;
        this.cost = new Cost(data.cost_amount, data.cost_type);

        this.amount = amount;

        this.id = id;
    }
}
