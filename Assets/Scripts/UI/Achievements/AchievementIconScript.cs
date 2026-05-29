using TMPro;
using UnityEngine;

public class AchievementIconScript : MonoBehaviour
{
    AchievementController achievementController;

    public TextMeshProUGUI achievementTMP;
    public GameObject claim_button;
    public GameObject claimedTMP_GO;

    public GameObject rewards_content;
    RectTransform content_rect_transform;

    //List<Task>

    public string achievement_title;

    public void ClaimRewardsButton()
    {
        achievementController.ClaimRewardsOnAchievement(achievement_title);

        DeactivateClaimButton();

        ActivateClaimedTMP();
    }

    public void SetAcievement(string achievement_title)
    {
        achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();
        content_rect_transform = rewards_content.GetComponent<RectTransform>();

        this.achievement_title = achievement_title;
        achievementTMP.text = achievementController.dict_achievement_title_to_achievement[achievement_title].achievement_text;

        SetCurrentProgress();

        UpdateContent();
        DeactivateClaimButton();
        DeactivateClaimedTMP();

        if (achievementController.dict_achievement_title_to_achievement[achievement_title].is_completed && achievementController.dict_achievement_title_to_achievement[achievement_title].is_claimed == false)
        {
            ActivateClaimButton();
        }
        if (achievementController.dict_achievement_title_to_achievement[achievement_title].is_claimed)
        {
            ActivateClaimedTMP();
        }
    }

    void SetCurrentProgress()
    {
        if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_NPC_TalkToAmountNPC achievementTask_NPC_TalkToAmountNPC)
        {
            achievementTMP.text += " " + achievementController.set_of_npc_names_player_talked_to.Count.ToString() + "/" + achievementTask_NPC_TalkToAmountNPC.amount;
        }
        else if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_NPC_MakeAmountDialogs achievementTask_NPC_MakeAmountDialogs)
        {
            achievementTMP.text += " " + achievementController.set_of_completed_dialog_titles.Count.ToString() + "/" + achievementTask_NPC_MakeAmountDialogs.amount;
        }
        else if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_ENEMY_AmountOfEnemyKilled achievementTask_ENEMY_AmountOfEnemyKilled)
        {
            achievementTMP.text += " " + achievementController.amount_of_enemy_killed.ToString() + "/" + achievementTask_ENEMY_AmountOfEnemyKilled.amount;
        }
        else if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_INVENTORY_UseAmountItems achievementTask_INVENTORY_UseAmountItems)
        {
            achievementTMP.text += " " + achievementController.amount_of_items_used.ToString() + "/" + achievementTask_INVENTORY_UseAmountItems.amount;
        }
        else if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_QUEST_CompleteAmountQuest achievementTask_QUEST_CompleteAmountQuest)
        {
            achievementTMP.text += " " + achievementController.amount_of_quests_completed.ToString() + "/" + achievementTask_QUEST_CompleteAmountQuest.amount;
        }
        else if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_WISH_UseAmountWishes achievementTask_WISH_UseAmountWishes)
        {
            achievementTMP.text += " " + achievementController.amount_of_wishes_made.ToString() + "/" + achievementTask_WISH_UseAmountWishes.amount;
        }
        else if (achievementController.dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_MAP_AmountOfChestsOpened achievementTask_MAP_AmountOfChestsOpened)
        {
            achievementTMP.text += " " + achievementController.amount_of_chests_opened.ToString() + "/" + achievementTask_MAP_AmountOfChestsOpened.amount;
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

    void ActivateClaimedTMP()
    {
        claimedTMP_GO.SetActive(true);
    }

    void DeactivateClaimedTMP()
    {
        claimedTMP_GO.SetActive(false);
    }
}
