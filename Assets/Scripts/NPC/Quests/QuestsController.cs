using System.Collections.Generic;
using UnityEngine;
using static DialogController;

public class QuestsController : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;

    public GameObject taskShower;
    public GameObject questPanelContent;

    RectTransform quest_panel_rect_transform;

    public GameObject questInfoPrefab;

    TaskShowerScript taskShowerScript;

    public int accepted_quests_amout = 0;

    public int item_height = 250;
    public int space_between_items = 25;

    public int none_quest_index = -1;
    public string none_quest_name = "no_name";

    public class Reward
    {
        public Item item;
        public int amount;
    }

    public class CollectableItem
    {
        public Item item;
        public int amount;
    }

    public abstract class Task
    {
        public string subtitle;
        public string description;

        public string finish_function_name;

        public List<Reward> rewards;

        public Task next_task;

        public Task(string subtitle, string description = "", string finish_function_name = "", Task next_task = null)
        {
            this.subtitle = subtitle;
            this.description = description;
            this.finish_function_name = finish_function_name;
            this.next_task = next_task;

            rewards = new List<Reward>();
        }

        public virtual Task FinishTaskAndGetNextTask()
        {
            if (CheckIfTaskIsCompleted())
            {
                return next_task;
            }
            return this;
        }

        public abstract bool CheckIfTaskIsCompleted();
    }

    public class DialogTask : Task
    {
        public Dialog dialog;

        public DialogTask(Dialog dialog, string subtitle, string description = "", string finish_function_name = "", Task next_task = null) : base(subtitle, description, finish_function_name, next_task)
        {
            this.dialog = dialog;
        }

        public override bool CheckIfTaskIsCompleted()
        {
            throw new System.NotImplementedException();
            //return dialog.current_speeck == null;
        }
    }

    public class CollectItemTask : Task
    {
        public List<CollectableItem> collectable_items;

        public CollectItemTask(List<CollectableItem> collectable_items, string subtitle, string description = "", string finish_function_name = "", Task next_task = null) : base(subtitle, description, finish_function_name, next_task)
        {
            this.collectable_items = collectable_items;
        }

        public override bool CheckIfTaskIsCompleted()
        {
            
            throw new System.NotImplementedException();
            /*
            foreach (CollectableItem item in collectable_items)
            {
                if ()
            }
            return true;
            */
        }
    }
    public class EnemyKillTask : Task
    {
        public List<GameObject> enemy_GOs;

        public EnemyKillTask(List<GameObject> enemy_GOs, string subtitle, string description = "", string finish_function_name = "", Task next_task = null) : base(subtitle, description, finish_function_name, next_task)
        {
            this.enemy_GOs = enemy_GOs;
        }

        public override bool CheckIfTaskIsCompleted()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Quest
    {
        public string title;

        public Task current_task;

        public List<Reward> rewards;

        public Quest(string title, Task current_task)
        {
            this.title = title;
            this.current_task = current_task;
        }
    }

    public Dictionary<string, Quest> dict_quest_name_to_quest = new Dictionary<string, Quest>();
    public Dictionary<string, List<string>> dict_npc_to_list_of_quests_names = new Dictionary<string, List<string>>();
    public Dictionary<string, GameObject> dict_npc_name_to_npc_GO = new Dictionary<string, GameObject>();

    public List<string> accepted_quests = new List<string>();

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    void Start()
    {
        taskShowerScript = taskShower.GetComponent<TaskShowerScript>();

        quest_panel_rect_transform = questPanelContent.GetComponent<RectTransform>();

        MakeQuests();
    }

    public void FinishTask(string quest_title)
    {
        
    }

    public void AcceptQuest(string new_quest)
    {
        
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
            string current_quest_description = temp_quest.current_task.subtitle;

            questInfoScript.SetNewQuestTitle(quest, current_quest_description);
        }

        int new_height = accepted_quests_amout * item_height + (accepted_quests_amout + 1) * space_between_items;
        quest_panel_rect_transform.sizeDelta = new Vector2(quest_panel_rect_transform.sizeDelta.x, new_height);
    }

    public void ShowNewTask(string new_task)
    {
        taskShowerScript.ShowNewTask(new_task);
    }

    void MakeQuests()
    {
        
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
