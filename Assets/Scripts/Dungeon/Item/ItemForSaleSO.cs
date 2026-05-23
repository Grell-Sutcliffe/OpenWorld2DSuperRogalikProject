using UnityEngine;
using static ShopPanelScript;

[CreateAssetMenu(fileName = "ItemForSale", menuName = "Item/ItemForSale")]
public class ItemForSaleSO : ConsumableItemSO
{
    //public Cost cost;
    public int cost_amount;
    public CostType cost_type;
}
