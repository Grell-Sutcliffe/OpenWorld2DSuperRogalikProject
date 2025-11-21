using System.Xml.Linq;
using UnityEngine;

public class ConsumableItem : Item
{
    public int amount = 1;

    public ConsumableItem(string name_, string description_, Sprite sprite_) 
    {
        name = name_;
        description = description_;
        count = 0;
        sprite = sprite_;
    }
    public ConsumableItem(string name_)
    {
        name = name_;
        description = "";
        count = 0;
    }
    public ConsumableItem()
    {
        name = "";
        description = "";
        count = 0;
    }

    public virtual void OnPickup(GameObject player)
    {
        Debug.Log($"Подобран расходник {name} x{amount}");
        Destroy(gameObject);
    }
}