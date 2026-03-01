using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnswerableSpeachNode", menuName = "Dialog/AnswerableSpeachNode")]
public class ItemDeliverySpeachNodeSO : SpeachNodeSO
{
    public List<ItemSO> list_of_itemSOs;
    public List<int> list_of_item_amounts;
}
