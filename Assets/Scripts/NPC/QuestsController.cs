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

    public DedusQuestScript dedusQuestScript;
    public DedusDialogScript dedusDialogScript;

    public DoggyQuestScript doggyQuestScript;
    public DoggyDialogScript doggyDialogScript;

    public GrandsonEugineQuestScript grandsonEugineQuestScript;
    public GrandsonEugeneDialogScript grandsonEugeneDialogScript;
    public GrandsonEugineMoveScript grandsonEugineMoveScript;

    public int accepted_quests_amout = 0;

    public int item_height = 250;
    public int space_between_items = 25;

    public int none_quest_index = -1;
    public string none_quest_name = "no_name";

    public string dedus = "Dedus";
    public string grandsonEugene = "GrandsonEugene";
    public string doggy = "Doggy";

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
        public string function_name;
        public bool is_task_completed;
        public List<Reward> rewards;
        public List<SpeachTree> speach_trees;
        public int current_speach_tree_index;

        public Task()
        {
            title = string.Empty;
            description = string.Empty;
            function_name = string.Empty;
            is_task_completed = false;
            rewards = new List<Reward>();
            speach_trees = new List<SpeachTree>();
            current_speach_tree_index = 0;
        }

        public Task(string title_, string description_)
        {
            title = title_;
            description = description_;
            function_name= string.Empty;
            is_task_completed = false;
            rewards = new List<Reward>();
            speach_trees = new List<SpeachTree>();
            current_speach_tree_index = 0;
        }
    }

    public class Quest
    {
        public string title;
        public string description = string.Empty;
        public string function_name;
        public List<Task> tasks;
        public int current_task_index;
        public List<Reward> rewards;
        public bool is_quest_accepted;
        public bool is_quest_completed;

        public Quest()
        {
            title = string.Empty;
            description = string.Empty;
            function_name = string.Empty;
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
            function_name = string.Empty;
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
        UpdateNPC();
    }

    void FindNPC()
    {
        dedusQuestScript = dict_npc_name_to_npc_GO[dedus].GetComponent<DedusQuestScript>();
        dedusDialogScript = dict_npc_name_to_npc_GO[dedus].GetComponent<DedusDialogScript>();

        doggyQuestScript = dict_npc_name_to_npc_GO[doggy].GetComponent<DoggyQuestScript>();
        doggyDialogScript = dict_npc_name_to_npc_GO[doggy].GetComponent<DoggyDialogScript>();

        grandsonEugineQuestScript = dict_npc_name_to_npc_GO[grandsonEugene].GetComponent<GrandsonEugineQuestScript>();
        grandsonEugeneDialogScript = dict_npc_name_to_npc_GO[grandsonEugene].GetComponent<GrandsonEugeneDialogScript>();
        grandsonEugineMoveScript = dict_npc_name_to_npc_GO[grandsonEugene].GetComponent<GrandsonEugineMoveScript>();
    }

    public void UpdateNPC()
    {
        dedusQuestScript.quests = dict_npc_to_list_of_quests_names[dedus];
        dedusQuestScript.UpdateInfo();

        doggyQuestScript.quests = dict_npc_to_list_of_quests_names[doggy];
        doggyQuestScript.UpdateInfo();

        grandsonEugineQuestScript.quests = dict_npc_to_list_of_quests_names[grandsonEugene];
        grandsonEugineQuestScript.UpdateInfo();
    }

    public void FinishTask(string quest_title)
    {
        string temp_function = dict_quest_name_to_quest[quest_title].tasks[dict_quest_name_to_quest[quest_title].current_task_index].function_name;

        if (temp_function != string.Empty)
        {
            Invoke(temp_function, 0.1f);
        }

        dict_quest_name_to_quest[quest_title].current_task_index++;

        if (dict_quest_name_to_quest[quest_title].current_task_index >= dict_quest_name_to_quest[quest_title].tasks.Count)
        {
            CompleteQuest(quest_title);
        }
    }

    public void AcceptQuest(string new_quest)
    {
        dict_quest_name_to_quest[new_quest].is_quest_accepted = true;
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

            Quest temp_quest = dict_quest_name_to_quest[quest];
            string current_quest_description = temp_quest.tasks[temp_quest.current_task_index].title;

            questInfoScript.SetNewQuestTitle(quest, current_quest_description);
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
        dict_npc_to_list_of_quests_names[doggy] = new List<string>();  // Doggy
        dict_npc_to_list_of_quests_names[grandsonEugene] = new List<string>();  // grandson Eugene

        MakeGrandsonQuest();
    }

    void MakeGrandsonQuest()
    {
        Quest new_quest = new Quest();

        new_quest.title = quest_TheLostGrandson;
        new_quest.description = "Помоги Дедусу отыскать внука.";
        new_quest.function_name = "TheLostGrandson_StopMoveToPlayer";

        Task new_task_1 = new Task();  // DEDUS

        new_task_1.title = "Узнай у Дедуса подробности.";
        new_task_1.NPC = dedus;
        new_task_1.description = "Попробуй поискать его около деревни.";
        if (dedusDialogScript == null) dedusDialogScript = GameObject.Find("Dedus").GetComponent<DedusDialogScript>();
        new_task_1.speach_trees.Add(dedusDialogScript.TheLostGrandson_ask_for_search_grandson_1);
        new_task_1.speach_trees.Add(dedusDialogScript.TheLostGrandson_ask_for_search_grandson_2);
        new_quest.tasks.Add(new_task_1);

        Task new_task_2 = new Task();  // DOGGY

        new_task_2.title = "Отыщи Джека, возвомжно, он что-то знает.";
        new_task_2.NPC = doggy;
        new_task_2.description = "Отыщи Джека.";
        if (doggyDialogScript == null) doggyDialogScript = GameObject.Find("Doggy").GetComponent<DoggyDialogScript>();
        new_task_2.speach_trees.Add(doggyDialogScript.TheLostGrandson_ask_for_help_1);
        new_quest.tasks.Add(new_task_2);

        /*
        new_task.title = tasks_TheLostGrandson[1];
        new_task.description = "Уничтож противников, не дай мальчишку в обиду.";
        new_quest.tasks.Add(new_task);
        */

        Task new_task_3 = new Task();  // BOY

        new_task_3.title = "Найди мальчика.";
        new_task_3.NPC = grandsonEugene;
        if (grandsonEugeneDialogScript == null) grandsonEugeneDialogScript = GameObject.Find("GrandsonEugene").GetComponent<GrandsonEugeneDialogScript>();
        new_task_3.speach_trees.Add(grandsonEugeneDialogScript.TheLostGrandson_ask_for_help_1);
        new_task_3.function_name = "TheLostGrandson_StartMoveToPlayer";
        new_quest.tasks.Add(new_task_3);

        Task new_task_4 = new Task();

        new_task_4.title = "Помоги мальчику найти дорогу до его дедушки.";
        new_task_4.NPC = dedus;
        new_task_4.description = "Покажи мальчику дорогу до его дедушки.";
        new_task_4.speach_trees.Add(dedusDialogScript.TheLostGrandson_ask_for_search_grandson_3);
        new_task_4.function_name = "TheLostGrandson_StopMoveToPlayer";
        new_quest.tasks.Add(new_task_4);

        dict_quest_name_to_quest[new_quest.title] = new_quest;

        dict_npc_to_list_of_quests_names[dedus].Add(new_quest.title);
        dict_npc_to_list_of_quests_names[doggy].Add(new_quest.title);
        dict_npc_to_list_of_quests_names[grandsonEugene].Add(new_quest.title);
    }

    void MakeDictOfNPC()
    {
        dict_npc_name_to_npc_GO[dedus] = mainController.Dedus;
        dict_npc_name_to_npc_GO[grandsonEugene] = mainController.GrandsonEugene;
        dict_npc_name_to_npc_GO[doggy] = mainController.Doggy;
    }

    public void OpenQuestPanel()
    {
        UpdateQestPanel();
    }

    void TheLostGrandson_StartMoveToPlayer()
    {
        grandsonEugineMoveScript.StartMoveToPlayer();
    }

    void TheLostGrandson_StopMoveToPlayer()
    {
        grandsonEugineMoveScript.StopMoveToPlayer();
    }
}
