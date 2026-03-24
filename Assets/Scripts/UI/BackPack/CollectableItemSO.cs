using UnityEngine;

[CreateAssetMenu(fileName = "CollectacleItem", menuName = "Item/CollectacleItem")]
public class CollectableItemSO : ScriptableObject
{
    public ItemSO itemSO;
    public int amount;
}
