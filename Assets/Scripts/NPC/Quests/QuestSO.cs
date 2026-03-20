using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Quest")]
public class QuestSO : ScriptableObject
{
    public string title;
    public string description;

    public TaskSO start_taskSO;

    public NPCSO quest_accepting_NPCSO;

    // public List<RewardSO> rewardSOs;
}
