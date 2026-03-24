using System.Collections.Generic;
using UnityEngine;
using static DialogController;

public class Reward
{
    public string item_name;
    public int amount;

    RewardSO data;

    public Reward(RewardSO data)
    {
        this.data = data;

        this.item_name = data.itemSO.item_name;
        this.amount = data.amount;
    }
}

public class CollectableItem
{
    public string item_name;
    public int amount;

    CollectableItemSO data;

    public CollectableItem(CollectableItemSO data)
    {
        this.data = data;

        this.item_name= data.itemSO.item_name;
        this.amount = data.amount;
    }

    public CollectableItem(string item_name, int amount)
    {
        this.item_name = item_name;
        this.amount = amount;
    }
}

public class Task
{
    public string subtitle;
    public string description;

    public string finish_function_name;

    //public List<Reward> rewards;

    public Task next_task;

    public Task()
    {

    }

    public Task(string subtitle, string description = "", string finish_function_name = "", Task next_task = null)
    {
        this.subtitle = subtitle;
        this.description = description;
        this.finish_function_name = finish_function_name;
        this.next_task = next_task;

        //rewards = new List<Reward>();
    }

    public Task(string subtitle, string description = "", string finish_function_name = "", TaskSO next_taskSO = null)
    {
        this.subtitle = subtitle;
        this.description = description;
        this.finish_function_name = finish_function_name;
        this.next_task = new Task().NewTask(next_taskSO);

        //rewards = new List<Reward>();
    }

    public Task NewTask(TaskSO taskSO)
    {
        if (taskSO is DialogTaskSO dialogTaskSO)
        {
            return new DialogTask(dialogTaskSO);
        }
        if (taskSO is CollectItemTaskSO collectItemTask)
        {
            return new CollectItemTask(collectItemTask);
        }

        return null;
    }

    /*
    public void CreateListOfRewards(List<RewardSO> rewardSOs)
    {
        this.rewards = new List<Reward>();

        foreach (RewardSO rewardSO in rewardSOs)
        {
            rewards.Add(new Reward(rewardSO));
        }
    }*/

    public virtual Task FinishTaskAndGetNextTask()
    {
        //if (CheckIfTaskIsCompleted())
        {
            return next_task;
        }
        return this;
    }
}

public class DialogTask : Task
{
    //public Dialog dialog;
    public string dialog_title;

    DialogTaskSO data;

    public DialogTask(DialogTaskSO data) : base(data.subtitle, data.description, data.finish_function_name, data.next_taskSO)
    {
        this.dialog_title = data.dialogSO.title;

        //this.CreateListOfRewards(data.rewardSOs);

        this.data = data;
    }
}

public class CollectItemTask : Task
{
    public List<CollectableItem> collectable_items;

    CollectItemTaskSO data;

    public CollectItemTask(CollectItemTaskSO data) : base(data.subtitle, data.description, data.finish_function_name, data.next_taskSO)
    {
        this.collectable_items = new List<CollectableItem>();

        foreach (CollectableItemSO collectableItemSO in data.collectacleItemSOs)
        {
            collectable_items.Add(new CollectableItem(collectableItemSO));
        }

        this.data = data;
    }
}

public class EnemyKillTask : Task
{
    // public List<GameObject> enemy_GOs;
}

public class Quest
{
    public string title;
    public string description;

    public Task current_task;

    public List<Reward> rewards;

    public string quest_accepting_NPC_name;

    QuestSO data;

    public Quest(QuestSO data)
    {
        this.data = data;

        this.title = data.title;
        this.description = data.description;

        this.current_task = new Task().NewTask(data.start_taskSO);

        this.rewards = new List<Reward>();

        //foreach ()

        this.quest_accepting_NPC_name = data.quest_accepting_NPCSO.npc_name;

        this.rewards = new List<Reward>();

        foreach (RewardSO rewardSO in data.rewardSOs)
        {
            this.rewards.Add(new Reward(rewardSO));
        }
    }
}

public class QuestsController : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;
    DialogController dialogController;

    public GameObject taskShower;
    public GameObject questPanelContent;

    RectTransform quest_panel_rect_transform;

    public GameObject questInfoPrefab;

    TaskShowerScript taskShowerScript;

    public int item_height = 500;
    public int space_between_items = 25;

    public int none_quest_index = -1;
    public string none_quest_name = "no_name";

    private string temp_task = "";

    public Dictionary<string, Quest> dict_quest_name_to_quest = new Dictionary<string, Quest>();
    //public Dictionary<string, List<string>> dict_npc_to_list_of_quests_names = new Dictionary<string, List<string>>();
    //public Dictionary<string, GameObject> dict_npc_name_to_npc_GO = new Dictionary<string, GameObject>();

    public List<QuestSO> quests = new List<QuestSO>();
    public List<string> accepted_quests = new List<string>();
    public List<string> finished_quests = new List<string>();

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
    }

    void Start()
    {
        taskShowerScript = taskShower.GetComponent<TaskShowerScript>();

        quest_panel_rect_transform = questPanelContent.GetComponent<RectTransform>();

        MakeQuests();
        UpdateNPCsQuestsIcons();
    }

    public void UpdateNPCsQuestsIcons()
    {
        foreach (QuestSO questSO in quests)
        {
            if (accepted_quests.Contains(questSO.title))
            {
                mainController.dict_npc_name_to_npcController[dict_quest_name_to_quest[questSO.title].quest_accepting_NPC_name].IconHasAcceptedQuestSetActiveTrue();
                continue;
            }
            if (dict_quest_name_to_quest[questSO.title].current_task != null)
            {
                mainController.dict_npc_name_to_npcController[dict_quest_name_to_quest[questSO.title].quest_accepting_NPC_name].IconHasUnacceptedQuestSetActiveTrue();
                continue;
            }
            mainController.dict_npc_name_to_npcController[dict_quest_name_to_quest[questSO.title].quest_accepting_NPC_name].IconThinkingSetActiveTrue();
        }
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
        List<string> list_of_quests_to_be_completed = new List<string>();

        if (e is DialogFinishedEvent dialogFinishedEvent)
        {
            Debug.Log($"EVENT  :  Завершили диалог {dialogFinishedEvent.dialog_title}");
            ShowNewTask();

            foreach (string quest_title in accepted_quests)
            {
                if (dict_quest_name_to_quest[quest_title].current_task is DialogTask dialogTask)
                {
                    if (dialogFinishedEvent.dialog_title == dialogTask.dialog_title)
                    {
                        NextTask(quest_title);

                        if (dict_quest_name_to_quest[quest_title].current_task == null)
                        {
                            list_of_quests_to_be_completed.Add(quest_title);
                        }
                    }
                }
            }
        }

        if (e is ItemCollectedEvent itemCollectedEvent)
        {
            foreach (string quest_title in accepted_quests)
            {
                if (dict_quest_name_to_quest[quest_title].current_task is CollectItemTask collectItemTask)
                {
                    bool is_task_finished = true;

                    foreach (CollectableItem collectableItem in collectItemTask.collectable_items)
                    {
                        if (backpackController.GetItemCounterByName(collectableItem.item_name) < collectableItem.amount)
                        {
                            is_task_finished = false;
                            break;
                        }
                    }

                    if (is_task_finished)
                    {
                        NextTask(quest_title);

                        if (dict_quest_name_to_quest[quest_title].current_task == null)
                        {
                            list_of_quests_to_be_completed.Add(quest_title);
                        }
                    }
                }
            }
        }

        if (e is QuestAcceptedEvent questAcceptedEvent)
        {
            Debug.Log($"EVENT  :  Приняли квест {questAcceptedEvent.quest_title}");
            AcceptQuest(questAcceptedEvent.quest_title);
        }

        foreach (string quest_title in list_of_quests_to_be_completed)
        {
            CompleteQuest(quest_title);
        }
    }

    public void AcceptQuest(string new_quest)
    {
        if (accepted_quests.Contains(new_quest))
        {
            return;
        }

        accepted_quests.Add(new_quest);

        temp_task = dict_quest_name_to_quest[new_quest].current_task.subtitle;

        UpdateNPCsQuestsIcons();
    }
    
    public void NextTask(string quest_title)
    {
        dict_quest_name_to_quest[quest_title].current_task = dict_quest_name_to_quest[quest_title].current_task.next_task;

        if (dict_quest_name_to_quest[quest_title].current_task != null)
        {
            ShowNewTask(dict_quest_name_to_quest[quest_title].current_task.subtitle);
        }

        if (dict_quest_name_to_quest[quest_title].current_task is CollectItemTask collectItemTask)
        {
            bool is_task_finished = true;

            foreach (CollectableItem collectableItem in collectItemTask.collectable_items)
            {
                if (backpackController.GetItemCounterByName(collectableItem.item_name) < collectableItem.amount)
                {
                    is_task_finished = false;
                    break;
                }
            }

            if (is_task_finished)
            {
                NextTask(quest_title);
            }
        }
    }

    public void CompleteQuest(string new_quest)
    {
        accepted_quests.Remove(new_quest);
        finished_quests.Add(new_quest);

        UpdateNPCsQuestsIcons();
    }

    public void ClaimRewardsOnQuest(string quest_title)
    {
        foreach (Reward reward in dict_quest_name_to_quest[quest_title].rewards)
        {
            backpackController.IncreaceItemByName(reward.item_name, reward.amount);
        }

        finished_quests.Remove(quest_title);
    }

    public void UpdateQestPanel()
    {
        foreach (Transform child in questPanelContent.transform)
        {
            Destroy(child.gameObject);
        }

        int new_height = 0;
        quest_panel_rect_transform.sizeDelta = new Vector2(quest_panel_rect_transform.sizeDelta.x, new_height);

        SpawnQuestsOfList(finished_quests);
        SpawnQuestsOfList(accepted_quests);
    }

    public void SpawnQuestsOfList(List<string> list_of_quests)
    {
        int quests_amout = list_of_quests.Count;

        foreach (string quest in list_of_quests)
        {
            GameObject new_prefab = Instantiate(questInfoPrefab, questPanelContent.transform);
            QuestInfoScript questInfoScript = new_prefab.GetComponent<QuestInfoScript>();

            Quest temp_quest = dict_quest_name_to_quest[quest];

            questInfoScript.SetQuest(quest);
        }

        int delta_height = quests_amout * item_height + (quests_amout + 1) * space_between_items;
        float new_height = quest_panel_rect_transform.sizeDelta.y + delta_height;
        quest_panel_rect_transform.sizeDelta = new Vector2(quest_panel_rect_transform.sizeDelta.x, new_height);
    }

    public void ShowNewTask()
    {
        if (temp_task != "")
        {
            taskShowerScript.ShowNewTask(temp_task);
            temp_task = "";
        }
    }

    public void ShowNewTask(string new_task)
    {
        taskShowerScript.ShowNewTask(new_task);
    }

    void MakeQuests()
    {
        foreach (QuestSO questSO in quests) {
            dict_quest_name_to_quest[questSO.title] = new Quest(questSO);
        }
    }

    public void OpenQuestPanel()
    {
        UpdateQestPanel();
    }

    void TheLostGrandson_StartMoveToPlayer()
    {
        //grandsonEugineMoveScript.StartMoveToPlayer();
    }

    void TheLostGrandson_StopMoveToPlayer()
    {
        //grandsonEugineMoveScript.StopMoveToPlayer();
    }
}
