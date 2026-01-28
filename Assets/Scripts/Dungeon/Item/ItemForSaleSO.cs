using UnityEngine;
using static ShopPanelScript;

[CreateAssetMenu(fileName = "ItemForSale", menuName = "Item/ItemForSale")]
public class ItemForSaleSO : ScriptableObject
{
    public Sprite sprite;
    public string item_name;
    public string description;

    public ItemType item_type;

    //public Cost cost;
    public int cost_amount;
    public CostType cost_type;
}
