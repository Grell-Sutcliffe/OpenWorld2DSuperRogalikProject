using System.Collections.Generic;
using UnityEngine;

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

public class Achievement
{
    public string achievement_text;
    public List<Reward> rewards;
    public AchievementType achievementType;

    public bool is_completed;
    public bool is_claimed;

    public AchievementTaskSO achievementTaskSO;

    AchievementSO data;

    public Achievement(AchievementSO data)
    {
        this.data = data;

        this.achievement_text = data.achievement_text;
        this.achievementType = data.achievementType;

        this.is_completed = false;
        this.is_claimed = false;

        this.achievementTaskSO = data.achievementTaskSO;

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
    DialogController dialogController;
    BackPackController backpackController;

    public GameObject achievementPanel;

    public GameObject rewardPrefab;
    
    AchievementPanelScript achievementPanelScript;

    List<AchievementType> achievementTypes;
    public List<AchievementSO> list_of_achievementSOs = new List<AchievementSO>();

    public Dictionary<string, Achievement> dict_achievement_title_to_achievement;
    public Dictionary<AchievementType, List<string>> dict_achievementType_to_list_of_achievement_list;

    public HashSet<string> set_of_completed_dialog_titles = new HashSet<string>();
    public HashSet<string> set_of_npc_names_player_talked_to = new HashSet<string>();

    private void Start()
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();

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
            dict_achievement_title_to_achievement[achievementSO.achievement_text] = new Achievement(achievementSO);

            dict_achievementType_to_list_of_achievement_list[achievementSO.achievementType].Add(achievementSO.achievement_text);
            dict_achievementType_to_list_of_achievement_list[AchievementType.All].Add(achievementSO.achievement_text);
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
            set_of_completed_dialog_titles.Add(dialogFinishedEvent.dialog_title);
            set_of_npc_names_player_talked_to.Add(dialogController.dict_dialog_title_to_dialog[dialogFinishedEvent.dialog_title].dialog_starting_npc);

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.NPC])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_NPC_TalkToCertainNPC achievementTask_NPC_TalkToCertainNPC)
                {
                    if (set_of_npc_names_player_talked_to.Contains(achievementTask_NPC_TalkToCertainNPC.npcSO.npc_name))
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_NPC_TalkToAmountNPC achievementTask_NPC_TalkToAmountNPC)
                {
                    if (set_of_npc_names_player_talked_to.Count >= achievementTask_NPC_TalkToAmountNPC.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
                
                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_NPC_MakeAmountDialogs achievementTask_NPC_MakeAmountDialogs)
                {
                    if (set_of_completed_dialog_titles.Count >= achievementTask_NPC_MakeAmountDialogs.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
            }
        }

        if (e is ItemCollectedEvent itemCollectedEvent)
        {
            
        }

        if (e is QuestAcceptedEvent questAcceptedEvent)
        {
            
        }
    }
}
