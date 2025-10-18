using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;

public class QuestsController : MonoBehaviour
{
    MainController mainController;

    public GameObject taskShower;

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

    public Dictionary<string, List<Quest>> dict_npc_to_list_of_quests = new Dictionary<string, List<Quest>>();
    public Dictionary<string, GameObject> dict_npc_name_to_npc_GO = new Dictionary<string, GameObject>();

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        taskShowerScript = taskShower.GetComponent<TaskShowerScript>();

        MakeDictOfNPC();
        MakeQuests();
    }

    public void ShowNewTask(string new_task)
    {
        taskShowerScript.ShowNewTask(new_task);
    }

    public Quest GetIncompletedQuestOfNPC(string name)
    {
        foreach (Quest quest in dict_npc_to_list_of_quests[name]) 
        {
            if (!quest.is_quest_completed)
            {
                return quest;
            }
        }
        return null;
    }

    void MakeQuests()
    {
        dict_npc_to_list_of_quests[dedus] = new List<Quest>();  // Dedus

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

        dict_npc_to_list_of_quests[dedus].Add(new_quest);
    }

    void MakeDictOfNPC()
    {
        dict_npc_name_to_npc_GO[dedus] = mainController.Dedus;
        dict_npc_name_to_npc_GO[grandsonEugene] = mainController.GrandsonEugene;
    }
}
