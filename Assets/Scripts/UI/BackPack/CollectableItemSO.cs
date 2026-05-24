using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItem", menuName = "Item/CollectableItem")]
public class CollectableItemSO : ScriptableObject
{
    public ItemSO itemSO;
    public int amount;
}
