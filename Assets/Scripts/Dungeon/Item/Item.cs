using UnityEngine;
using System;
using System.Threading;
using UnityEngine.U2D;

[Serializable]
public abstract class Item
{
    public Sprite sprite;
    public string item_name;
    public string description;
    //public Guid uniqueID;
    //public float radius = 0.5f;

    public int amount;

    public ItemType item_type = ItemType.Everything;

    public int id;
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
