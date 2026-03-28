using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementIconScript : MonoBehaviour
{
    AchievementController achievementController;

    public TextMeshProUGUI achievementTMP;
    public GameObject claim_button;

    public GameObject rewards_content;
    RectTransform content_rect_transform;

    //List<Task>

    string achievement_title;

    public void ClaimRewardsButton()
    {
        achievementController.ClaimRewardsOnAchievement(achievement_title);

        DeactivateClaimButton();
    }

    public void SetAcievement(string achievement_title)
    {
        achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();
        content_rect_transform = rewards_content.GetComponent<RectTransform>();

        this.achievement_title = achievement_title;
        achievementTMP.text = achievementController.dict_achievement_title_to_achievement[achievement_title].achievement_text;

        UpdateContent();
        DeactivateClaimButton();
        if (achievementController.dict_achievement_title_to_achievement[achievement_title].is_completed)
        {
            ActivateClaimButton();
        }
    }

    public void UpdateContent()
    {
        ClearContent();
        SpawnRewards();
    }

    void ClearContent()
    {
        foreach (Transform child in rewards_content.transform)
        {
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void SpawnRewards()
    {
        foreach (Reward reward in achievementController.dict_achievement_title_to_achievement[achievement_title].rewards)
        {
            SpawnPrefab(reward);
        }
    }

    void SpawnPrefab(Reward reward)
    {
        GameObject new_prefab = Instantiate(achievementController.rewardPrefab, rewards_content.transform);
        RewardIconScript new_prefab_script = new_prefab.GetComponent<RewardIconScript>();

        new_prefab_script.SetReward(reward);
    }

    void ActivateClaimButton()
    {
        claim_button.SetActive(true);
    }

    void DeactivateClaimButton()
    {
        claim_button.SetActive(false);
    }
}
