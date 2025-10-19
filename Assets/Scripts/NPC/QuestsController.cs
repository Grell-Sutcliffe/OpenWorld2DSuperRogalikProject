using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;

public class QuestsController : MonoBehaviour
{
    MainController mainController;

    public GameObject taskShower;
    //public GameObject questPanel;
    public GameObject questPanelContent;

    public GameObject questInfoPrefab;

    TaskShowerScript taskShowerScript;

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
        public string title;
        public string description;
        public bool is_task_completed;
        public List<Reward> rewards;

        public Task()
        {
            title = string.Empty;
            description = string.Empty;
            is_task_completed = false;
            rewards = new List<Reward>();
        }

        public Task(string title_, string description_)
        {
            title = title_;
            description = description_;
            is_task_completed = false;
            rewards = new List<Reward>();
        }
    }

    public class Quest
    {
        public GameObject NPC = null;
        public string title;
        public string description = string.Empty;
        public bool is_quest_completed;
        public List<Task> tasks;
        public List<Reward> rewards;
        public SpeachTree speach_tree;
        public bool is_quest_accepted;

        public Quest()
        {
            NPC = null;
            title = string.Empty;
            description = string.Empty;
            is_quest_completed = false;
            is_quest_accepted = false;
            tasks = new List<Task>();
            rewards = new List<Reward>();
        }

        public Quest(string title_, string description_)
        {
            NPC = null;
            title = title_;
            description = description_;
            is_quest_completed = false;
            is_quest_accepted = false;
            tasks = new List<Task>();
            rewards = new List<Reward>();
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

        MakeDictOfNPC();
        MakeQuests();
    }

    public void AcceptQuest(string new_quest)
    {
        accepted_quests.Add(new_quest);

        GameObject new_prefab = Instantiate(questInfoPrefab, questPanelContent.transform);
        QuestInfoScript questInfoScript = new_prefab.GetComponent<QuestInfoScript>();
        questInfoScript.SetNewQuestTitle(new_quest);
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

        new_quest.NPC = dict_npc_name_to_npc_GO[dedus];
        new_quest.title = quest_TheLostGrandson;
        new_quest.description = "Помоги Дедусу отыскать внука.";

        Task new_task = new Task();

        new_task.title = tasks_TheLostGrandson[0];
        new_task.description = "Попробуй поискать его около деревни.";
        new_quest.tasks.Add(new_task);

        new_task.title = tasks_TheLostGrandson[1];
        new_task.description = "Уничтож противников, не дай мальчишку в обиду.";
        new_quest.tasks.Add(new_task);

        new_task.title = tasks_TheLostGrandson[2];
        new_task.description = "Покажи мальчику дорогу до его дедушки.";
        new_quest.tasks.Add(new_task);

        dict_quest_name_to_quest[new_quest.title] = new_quest;
        dict_npc_to_list_of_quests_names[dedus].Add(new_quest.title);
    }

    void MakeDictOfNPC()
    {
        dict_npc_name_to_npc_GO[dedus] = mainController.Dedus;
        dict_npc_name_to_npc_GO[grandsonEugene] = mainController.GrandsonEugene;
    }
}
