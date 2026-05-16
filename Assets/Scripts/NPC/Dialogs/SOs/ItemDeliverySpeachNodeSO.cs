using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDeliverySpeachNode", menuName = "Dialog/ItemDeliverySpeachNode")]
public class ItemDeliverySpeachNodeSO : DefaultSpeachNodeSO
{
    public List<ItemSO> list_of_itemSOs;
    public List<int> list_of_item_amounts;
}
