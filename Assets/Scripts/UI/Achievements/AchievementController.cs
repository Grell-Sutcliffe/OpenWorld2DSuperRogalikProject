using System.Collections.Generic;
using UnityEngine;

int current_id = 0;

public enum AchievementType
{
    All = 0,
    Quests = 1,
    NPC = 2,
    Map = 3,
    Enemy = 4,
    Wish = 5,
    Upgrade = 6,
    Inventory = 7,
}

public class AchievementTask
{
    public bool is_completed;
}

public class QuestAchievementTask : AchievementTask
{
    public int completed_quests_amount;
}

public class UpgradeAchievementTask : AchievementTask  // ???
{
    public int level;
}

public class Achievement
{
    public int id;
    public string achievement_text;
    public List<Task> tasks;
    public List<Reward> rewards;
    public AchievementType achievementType;

    public bool is_completed;
    public bool is_claimed;

    AchievementSO data;

    public Achievement(int id, AchievementSO data)
    {
        this.data = data;

        this.id = id;

        this.achievement_text = data.achievement_text;
        this.achievementType = data.achievementType;

        this.is_completed = false;
        this.is_claimed = false;

        this.tasks = new List<Task>();
        foreach (TaskSO taskSO in data.taskSOs)
        {
            this.tasks.Add(new Task().NewTask(taskSO));
        }

        this.rewards = new List<Reward>();
        foreach (RewardSO rewardSO in data.rewardSOs)
        {
            this.rewards.Add(new Reward(rewardSO));
        }
    }

    //public Achievement(int id, string achievement_text)
}

public class AchievementController : MonoBehaviour
{
    BackPackController backpackController;

    public GameObject achievementPanel;

    public GameObject rewardPrefab;
    
    AchievementPanelScript achievementPanelScript;

    List<AchievementType> achievementTypes;
    public List<AchievementSO> list_of_achievementSOs = new List<AchievementSO>();

    public List<int> list_of_completed_quest_amount_for_quest_achievement = new List<int>();

    public Dictionary<string, Achievement> dict_achievement_title_to_achievement;
    public Dictionary<AchievementType, List<string>> dict_achievementType_to_list_of_achievement_list;

    private void Start()
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        achievementPanelScript = achievementPanel.GetComponent<AchievementPanelScript>();

        MakeListOfAchievementTypes();
        FillDictionary();
    }

    private void MakeListOfAchievementTypes()
    {
        achievementTypes = new List<AchievementType>();

        achievementTypes.Add(AchievementType.All);
        achievementTypes.Add(AchievementType.Quests);
        achievementTypes.Add(AchievementType.NPC);
        achievementTypes.Add(AchievementType.Map);
        achievementTypes.Add(AchievementType.Enemy);
        achievementTypes.Add(AchievementType.Wish);
        achievementTypes.Add(AchievementType.Upgrade);
        achievementTypes.Add(AchievementType.Inventory);
    }

    private void FillDictionary()
    {
        dict_achievement_title_to_achievement = new Dictionary<string, Achievement>();
        dict_achievementType_to_list_of_achievement_list = new Dictionary<AchievementType, List<string>>();

        foreach (AchievementType achievementType in achievementTypes)
        {
            dict_achievementType_to_list_of_achievement_list[achievementType] = new List<string>();
        }

        foreach (AchievementSO achievementSO in list_of_achievementSOs)
        {
            dict_achievement_title_to_achievement[achievementSO.achievement_title] = new Achievement(achievementSO);

            dict_achievementType_to_list_of_achievement_list[achievementSO.achievementType].Add(achievementSO.achievement_title);
            dict_achievementType_to_list_of_achievement_list[AchievementType.All].Add(achievementSO.achievement_title);
        }
    }

    public void ClaimRewardsOnAchievement(string achievement_title)
    {
        dict_achievement_title_to_achievement[achievement_title].is_claimed = true;

        foreach (Reward reward in dict_achievement_title_to_achievement[achievement_title].rewards)
        {
            backpackController.IncreaceItemByName(reward.item_name, reward.amount);
        }
    }

    public void OpenAchievementPanel()
    {
        achievementPanelScript.OpenPanel();
    }

    private void OnEnable()
    {
        EventBus.OnEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.OnEvent -= HandleEvent;
    }

    private void HandleEvent(IEvent e)
    {
        if (e is DialogFinishedEvent dialogFinishedEvent)
        {
            
        }

        if (e is ItemCollectedEvent itemCollectedEvent)
        {
            
        }

        if (e is QuestAcceptedEvent questAcceptedEvent)
        {
            
        }
    }
}
