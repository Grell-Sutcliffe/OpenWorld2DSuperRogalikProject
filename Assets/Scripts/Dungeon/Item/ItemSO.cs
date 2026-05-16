using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite sprite;
    public string item_name;
    public string description;

    public ItemType item_type;
}
