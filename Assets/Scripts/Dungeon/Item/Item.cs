using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public Sprite sprite;
    public string item_name;
    public string description;
    //public Guid uniqueID;
    //public float radius = 0.5f;

    public int amount;

    public ItemType item_type = ItemType.Everything;

    public int id;

    public bool is_craftable;
    public List<string> item_names_for_craft;
    public List<int> item_amounts_for_craft;

    /*
    public bool is_breakable;
    public Item broken_version;
    public float time_to_break;
    */

    ItemSO data;

    public Item()
    {

    }

    public Item(ItemSO data)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.item_name;
        this.description = data.description;

        this.item_type = data.item_type;

        this.is_craftable = data.is_craftable;

        this.item_names_for_craft = new List<string>();
        foreach (ItemSO itemSO in data.itemSOs_for_craft)
        {
            item_names_for_craft.Add(itemSO.item_name);
        }

        this.item_amounts_for_craft = new List<int>();
        foreach (int amount in data.item_amounts_for_craft)
        {
            item_amounts_for_craft.Add(amount);
        }
    }
}

public enum ItemType
{
    Everything = 0,
    Weapon = 1,
    Food = 2,
    Drink = 3,
    Materials = 4,
    Quest = 5,
}
