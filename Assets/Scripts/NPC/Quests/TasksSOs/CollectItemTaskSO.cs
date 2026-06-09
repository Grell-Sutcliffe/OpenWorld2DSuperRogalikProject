using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectItemTask", menuName = "Quest/CollectItemTask")]
public class CollectItemTaskSO : TaskSO
{
    public List<CollectableItemSO> collectableItemSOs = new List<CollectableItemSO>();
    public string collect_title;
}
