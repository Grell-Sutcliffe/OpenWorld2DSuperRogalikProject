using UnityEngine;

public class AchievementTypeIconScript : MonoBehaviour
{
    AchievementController achievementController;
    AchievementPanelScript achievementPanelScript;

    public AchievementType achievementType;

    private void Start()
    {
        achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();
        achievementPanelScript = GameObject.Find("AchievementsPanel").GetComponent<AchievementPanelScript>();
    }

    public void OnClick()
    {
        if (achievementController == null) achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();
        if (achievementPanelScript == null) achievementPanelScript = GameObject.Find("AchievementsPanel").GetComponent<AchievementPanelScript>();

        achievementPanelScript.UpdateContent(achievementType);
    }
}
