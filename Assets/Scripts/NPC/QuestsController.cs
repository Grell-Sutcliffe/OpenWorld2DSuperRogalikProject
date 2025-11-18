using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;

public class QuestsController : MonoBehaviour
{
    MainController mainController;

    public GameObject taskShower;
    //public GameObject questPanel;
    public GameObject questPanelContent;

    RectTransform quest_panel_rect_transform;

    public GameObject questInfoPrefab;

    TaskShowerScript taskShowerScript;

    DedusQuestScript dedusQuestScript;
    DedusDialogScript dedusDialogScript;

    public int accepted_quests_amout = 0;

    public int item_height = 250;
    public int space_between_items = 25;

    public int none_quest_index = -1;
    public string none_quest_name = "no_name";

    public string dedus = "Dedus";
    public string grandsonEugene = "GrandsonEugene";

    public string quest_TheLostGrandson = "Потерянный внук";

    public string[] tasks_TheLostGrandson = {
        "Найди внука",
        "Спаси внука",
        "Отведи внука к дедушке",
    };

    public class Reward
    {
        public string title;
        public int amount;
    }

    public class Task
    {
        public string NPC;
        public string title;
        public string description;
        public bool is_task_completed;
        public List<Reward> rewards;
        public List<SpeachTree> speach_trees;
        public int current_speach_index;

        public Task()
        {
            title = string.Empty;
            description = string.Empty;
            is_task_completed = false;
            rewards = new List<Reward>();
            speach_trees = new List<SpeachTree>();
            current_speach_index = 0;
        }

        public Task(string title_, string description_)
        {
            title = title_;
            description = description_;
            is_task_completed = false;
            rewards = new List<Reward>();
            speach_trees = new List<SpeachTree>();
            current_speach_index = 0;
        }
    }

    public class Quest
    {
        public string title;
        public string description = string.Empty;
        public List<Task> tasks;
        public int current_task_index;
        public List<Reward> rewards;
        public bool is_quest_accepted;
        public bool is_quest_completed;

        public Quest()
        {
            title = string.Empty;
            description = string.Empty;
            is_quest_completed = false;
            is_quest_accepted = false;
            tasks = new List<Task>();
            rewards = new List<Reward>();
            current_task_index = 0;
        }

        public Quest(string title_, string description_)
        {
            title = title_;
            description = description_;
            is_quest_completed = false;
            is_quest_accepted = false;
            tasks = new List<Task>();
            rewards = new List<Reward>();
            current_task_index = 0;
        }
    }

    public Dictionary<string, Quest> dict_quest_name_to_quest = new Dictionary<string, Quest>();
    public Dictionary<string, List<string>> dict_npc_to_list_of_quests_names = new Dictionary<string, List<string>>();
    public Dictionary<string, GameObject> dict_npc_name_to_npc_GO = new Dictionary<string, GameObject>();

    public List<string> accepted_quests = new List<string>();

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        taskShowerScript = taskShower.GetComponent<TaskShowerScript>();

        quest_panel_rect_transform = questPanelContent.GetComponent<RectTransform>();

        MakeDictOfNPC();
        FindNPC();
        MakeQuests();
    }

    void FindNPC()
    {
        dedusQuestScript = dict_npc_name_to_npc_GO[dedus].GetComponent<DedusQuestScript>();
        dedusDialogScript = dict_npc_name_to_npc_GO[dedus].GetComponent<DedusDialogScript>();
    }

    public void AcceptQuest(string new_quest)
    {
        accepted_quests.Add(new_quest);
        accepted_quests_amout++;
    }

    public void CompleteQuest(string new_quest)
    {
        accepted_quests.Remove(new_quest);
        accepted_quests_amout--;
    }

    public void UpdateQestPanel()
    {
        foreach (Transform child in questPanelContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (string quest in accepted_quests)
        {
            GameObject new_prefab = Instantiate(questInfoPrefab, questPanelContent.transform);
            QuestInfoScript questInfoScript = new_prefab.GetComponent<QuestInfoScript>();
            questInfoScript.SetNewQuestTitle(quest);
        }

        int new_height = accepted_quests_amout * item_height + (accepted_quests_amout + 1) * space_between_items;
        quest_panel_rect_transform.sizeDelta = new Vector2(quest_panel_rect_transform.sizeDelta.x, new_height);
    }

    public void ShowNewTask(string new_task)
    {
        taskShowerScript.ShowNewTask(new_task);
    }

    public string GetIncompletedQuestOfNPC(string npc_name)
    {
        foreach (string quest_name in dict_npc_to_list_of_quests_names[npc_name])
        {
            if (!dict_quest_name_to_quest[quest_name].is_quest_completed)
            {
                return quest_name;
            }
        }
        return null;
    }

    void MakeQuests()
    {
        dict_npc_to_list_of_quests_names[dedus] = new List<string>();  // Dedus

        Quest new_quest = new Quest();  // Dedus - find grandson

        new_quest.title = quest_TheLostGrandson;
        new_quest.description = "Помоги Дедусу отыскать внука.";

        Task new_task_1 = new Task();

        new_task_1.title = tasks_TheLostGrandson[0];
        new_task_1.NPC = dedus;
        new_task_1.description = "Попробуй поискать его около деревни.";
        if (dedusDialogScript == null) dedusDialogScript = GameObject.Find("Dedus").GetComponent<DedusDialogScript>();
        new_task_1.speach_trees.Add(dedusDialogScript.TheLostGrandson_ask_for_search_grandson_1);
        new_task_1.speach_trees.Add(dedusDialogScript.TheLostGrandson_ask_for_search_grandson_2);
        new_quest.tasks.Add(new_task_1);

        /*
        new_task.title = tasks_TheLostGrandson[1];
        new_task.description = "Уничтож противников, не дай мальчишку в обиду.";
        new_quest.tasks.Add(new_task);
        */

        Task new_task_3 = new Task();

        new_task_3.title = tasks_TheLostGrandson[2];
        new_task_3.NPC = grandsonEugene;
        new_task_3.description = "Покажи мальчику дорогу до его дедушки.";
        new_quest.tasks.Add(new_task_3);

        dict_quest_name_to_quest[new_quest.title] = new_quest;
        dict_npc_to_list_of_quests_names[dedus].Add(new_quest.title);
    }

    void MakeDictOfNPC()
    {
        dict_npc_name_to_npc_GO[dedus] = mainController.Dedus;
        dict_npc_name_to_npc_GO[grandsonEugene] = mainController.GrandsonEugene;
    }

    public void OpenQuestPanel()
    {
        UpdateQestPanel();
    }
}
