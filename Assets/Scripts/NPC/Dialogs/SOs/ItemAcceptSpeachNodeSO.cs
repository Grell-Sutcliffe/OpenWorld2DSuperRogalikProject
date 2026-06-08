using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemAcceptSpeachNode", menuName = "Dialog/ItemAcceptSpeachNode")]
public class ItemAcceptSpeachNodeSO : DefaultSpeachNodeSO
{
    public List<ItemSO> list_of_itemSOs;
    public List<int> list_of_item_amounts;
}
