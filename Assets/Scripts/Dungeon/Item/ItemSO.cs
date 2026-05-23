using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite sprite;
    public string item_name;
    public string description;

    public bool is_craftable;
    public List<ItemSO> itemSOs_for_craft;
    public List<int> item_amounts_for_craft;

    public ItemType item_type;
}
