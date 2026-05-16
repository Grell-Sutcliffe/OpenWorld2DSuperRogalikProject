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
    public HashSet<string> set_of_completed_quest_titles = new HashSet<string>();

    public int amount_of_items_used = 0;
    public int amount_of_wishes_made = 0;
    public int amount_of_enemy_killed = 0;
    public int amount_of_quests_completed = 0;
    public int amount_of_chests_opened = 0;
    public int amount_of_5_star = 0;
    public int amount_of_4_star = 0;

    public static AchievementController Instance { get; private set; }

    private const string AchievementsSaveKey = "achievements_save";

    [System.Serializable]
    public class AchievementsSaveData
    {
        public List<AchievementSaveData> achievements = new List<AchievementSaveData>();

        public List<string> completedDialogTitles = new List<string>();
        public List<string> npcNamesPlayerTalkedTo = new List<string>();
        public List<string> completedQuestTitles = new List<string>();

        public int amountOfItemsUsed;
        public int amountOfWishesMade;
        public int amountOfEnemyKilled;
        public int amountOfQuestsCompleted;
        public int amountOfChestsOpened;
        public int amountOf5Star;
        public int amountOf4Star;
    }

    [System.Serializable]
    public class AchievementSaveData
    {
        public string achievementTitle;
        public bool isCompleted;
        public bool isClaimed;

        public AchievementSaveData(string achievementTitle, bool isCompleted, bool isClaimed)
        {
            this.achievementTitle = achievementTitle;
            this.isCompleted = isCompleted;
            this.isClaimed = isClaimed;
        }
    }

    public void SaveAchievements()
    {
        AchievementsSaveData saveData = new AchievementsSaveData();

        foreach (var pair in dict_achievement_title_to_achievement)
        {
            Achievement achievement = pair.Value;

            saveData.achievements.Add(new AchievementSaveData(
                pair.Key,
                achievement.is_completed,
                achievement.is_claimed
            ));
        }

        saveData.completedDialogTitles = new List<string>(set_of_completed_dialog_titles);
        saveData.npcNamesPlayerTalkedTo = new List<string>(set_of_npc_names_player_talked_to);
        saveData.completedQuestTitles = new List<string>(set_of_completed_quest_titles);

        saveData.amountOfItemsUsed = amount_of_items_used;
        saveData.amountOfWishesMade = amount_of_wishes_made;
        saveData.amountOfEnemyKilled = amount_of_enemy_killed;
        saveData.amountOfQuestsCompleted = amount_of_quests_completed;
        saveData.amountOfChestsOpened = amount_of_chests_opened;
        saveData.amountOf5Star = amount_of_5_star;
        saveData.amountOf4Star = amount_of_4_star;

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(AchievementsSaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Achievements saved: " + json);
    }

    public void LoadAchievements()
    {
        if (!PlayerPrefs.HasKey(AchievementsSaveKey))
        {
            Debug.Log("No achievements save found");
            return;
        }

        string json = PlayerPrefs.GetString(AchievementsSaveKey);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("Achievements save is empty");
            return;
        }

        AchievementsSaveData saveData = JsonUtility.FromJson<AchievementsSaveData>(json);

        if (saveData == null)
        {
            Debug.LogWarning("Achievements save is broken");
            return;
        }

        set_of_completed_dialog_titles = new HashSet<string>(
            saveData.completedDialogTitles ?? new List<string>()
        );

        set_of_npc_names_player_talked_to = new HashSet<string>(
            saveData.npcNamesPlayerTalkedTo ?? new List<string>()
        );

        set_of_completed_quest_titles = new HashSet<string>(
            saveData.completedQuestTitles ?? new List<string>()
        );

        amount_of_items_used = saveData.amountOfItemsUsed;
        amount_of_wishes_made = saveData.amountOfWishesMade;
        amount_of_enemy_killed = saveData.amountOfEnemyKilled;
        amount_of_quests_completed = saveData.amountOfQuestsCompleted;
        amount_of_chests_opened = saveData.amountOfChestsOpened;
        amount_of_5_star = saveData.amountOf5Star;
        amount_of_4_star = saveData.amountOf4Star;

        if (saveData.achievements != null)
        {
            foreach (AchievementSaveData achievementSaveData in saveData.achievements)
            {
                if (!dict_achievement_title_to_achievement.ContainsKey(achievementSaveData.achievementTitle))
                {
                    Debug.LogWarning("Saved achievement not found: " + achievementSaveData.achievementTitle);
                    continue;
                }

                Achievement achievement = dict_achievement_title_to_achievement[achievementSaveData.achievementTitle];

                achievement.is_completed = achievementSaveData.isCompleted;
                achievement.is_claimed = achievementSaveData.isClaimed;
            }
        }

        Debug.Log("Achievements loaded: " + json);
    }

    public void DeleteAchievements()
    {
        PlayerPrefs.DeleteKey(AchievementsSaveKey);
        PlayerPrefs.Save();

        set_of_completed_dialog_titles.Clear();
        set_of_npc_names_player_talked_to.Clear();
        set_of_completed_quest_titles.Clear();

        amount_of_items_used = 0;
        amount_of_wishes_made = 0;
        amount_of_enemy_killed = 0;
        amount_of_quests_completed = 0;
        amount_of_chests_opened = 0;
        amount_of_5_star = 0;
        amount_of_4_star = 0;

        foreach (var pair in dict_achievement_title_to_achievement)
        {
            pair.Value.is_completed = false;
            pair.Value.is_claimed = false;
        }

        SaveAchievements();

        Debug.Log("Achievements reset to default");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();

        achievementPanelScript = achievementPanel.GetComponent<AchievementPanelScript>();

        MakeListOfAchievementTypes();
        FillDictionary();

        LoadAchievements();
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

        SaveAchievements();
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
            Debug.Log($"Dialog finished  ---  {dialogFinishedEvent.dialog_title}");

            set_of_completed_dialog_titles.Add(dialogFinishedEvent.dialog_title);
            set_of_npc_names_player_talked_to.Add(dialogController.dict_dialog_title_to_dialog[dialogFinishedEvent.dialog_title].dialog_starting_npc);

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.NPC])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

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

        if (e is QuestCompletedEvent questCompletedEvent)
        {
            amount_of_quests_completed++;

            set_of_completed_quest_titles.Add(questCompletedEvent.quest_title);

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.Quests])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_QUEST_CompleteCertainQuest achievementTask_QUEST_CompleteCertainQuest)
                {
                    if (set_of_completed_quest_titles.Contains(achievementTask_QUEST_CompleteCertainQuest.questSO.title))
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_QUEST_CompleteAmountQuest achievementTask_QUEST_CompleteAmountQuest)
                {
                    if (amount_of_quests_completed >= achievementTask_QUEST_CompleteAmountQuest.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
            }
        }

        if (e is ItemUsedEvent itemUsedEvent)
        {
            amount_of_items_used++;

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.Inventory])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_INVENTORY_UseCertainItem achievementTask_INVENTORY_UseCertainItem)
                {
                    if (achievementTask_INVENTORY_UseCertainItem.usableItemSO.item_name == itemUsedEvent.item_name)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_INVENTORY_UseAmountItems achievementTask_INVENTORY_UseAmountItems)
                {
                    if (amount_of_items_used >= achievementTask_INVENTORY_UseAmountItems.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
            }
        }

        if (e is WishMadeEvent wishMadeEvent)
        {
            amount_of_wishes_made += wishMadeEvent.wish_amount;

            foreach (WishReward wishReward in wishMadeEvent.rewards)
            {
                if (wishReward.star == 4)
                {
                    amount_of_4_star++;
                }
                else if (wishReward.star == 5)
                {
                    amount_of_5_star++;
                }
            }

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.Wish])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_WISH_UseAmountWishes achievementTask_WISH_UseAmountWishes)
                {
                    if (amount_of_wishes_made >= achievementTask_WISH_UseAmountWishes.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_WISH_GetAmountOfLegendaryWeapon achievementTask_WISH_GetAmountOfLegendaryWeapon)
                {
                    if (achievementTask_WISH_GetAmountOfLegendaryWeapon.stars == 4)
                    {
                        if (amount_of_4_star >= achievementTask_WISH_GetAmountOfLegendaryWeapon.amount)
                        {
                            dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                        }
                    }
                    else if (achievementTask_WISH_GetAmountOfLegendaryWeapon.stars == 5)
                    {
                        if (amount_of_5_star >= achievementTask_WISH_GetAmountOfLegendaryWeapon.amount)
                        {
                            dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                        }
                    }
                }
            }
        }

        if (e is EnemyKilledEvent enemyKilledEvent)
        {
            amount_of_enemy_killed++;

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.Enemy])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_ENEMY_AmountOfEnemyKilled achievementTask_ENEMY_AmountOfEnemyKilled)
                {
                    if (amount_of_enemy_killed >= achievementTask_ENEMY_AmountOfEnemyKilled.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
            }
        }

        if (e is ChestOpenedEvent chestOpenedEvent)
        {
            amount_of_chests_opened++;

            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.Map])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_MAP_AmountOfChestsOpened achievementTask_MAP_AmountOfChestsOpened)
                {
                    if (amount_of_chests_opened >= achievementTask_MAP_AmountOfChestsOpened.amount)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
            }
        }

        if (e is CharacterUpgradeEvent characterUpgradeEvent)
        {
            foreach (string achievement_title in dict_achievementType_to_list_of_achievement_list[AchievementType.Upgrade])
            {
                if (dict_achievement_title_to_achievement[achievement_title].is_completed) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO == null) continue;

                if (dict_achievement_title_to_achievement[achievement_title].achievementTaskSO is AchievementTask_UPGRADE_CurrentLevel achievementTask_UPGRADE_CurrentLevel)
                {
                    if (characterUpgradeEvent.level >= achievementTask_UPGRADE_CurrentLevel.level)
                    {
                        dict_achievement_title_to_achievement[achievement_title].is_completed = true;
                    }
                }
            }
        }
    }
}
