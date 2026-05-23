using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item/ConsumableItem")]
public class ConsumableItemSO : ItemSO
{
    public int amount = 0;

    public int time_to_grow;
}
