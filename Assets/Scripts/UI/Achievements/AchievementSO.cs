using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement/Achievement")]
public class AchievementSO : ScriptableObject
{
    public string achievement_text;
    public List<RewardSO> rewardSOs;
    public AchievementType achievementType;

    public AchievementTaskSO achievementTaskSO;
}
