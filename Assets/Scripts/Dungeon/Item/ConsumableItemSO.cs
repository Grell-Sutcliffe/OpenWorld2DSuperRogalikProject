using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item/ConsumableItem")]
public class ConsumableItemSO : ScriptableObject
{
    public Sprite sprite;
    public string item_name;
    public string description;
    public ItemType item_type;
    public int amount = 0;
}
