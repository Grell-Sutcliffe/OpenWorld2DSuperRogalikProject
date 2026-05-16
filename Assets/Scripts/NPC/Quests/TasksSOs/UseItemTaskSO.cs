using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseItemTask", menuName = "Quest/UseItemTask")]
public class UseItemTaskSO : TaskSO
{
    public List<CollectableItemSO> usableItemSOs = new List<CollectableItemSO>();
}
