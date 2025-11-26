using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class GrandsonEugineQuestScript : MonoBehaviour
{
    QuestsController questsController;

    GrandsonEugeneController grandsonEugeneController;
    GrandsonEugeneDialogScript grandsonEugeneDialogScript;

    public List<string> quests = new List<string>();

    int current_quest_index = 0;

    public bool is_waiting_for_help = false;
    public bool is_quest_ongoing = false;

    void Start()
    {
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        grandsonEugeneDialogScript = gameObject.GetComponent<GrandsonEugeneDialogScript>();
        grandsonEugeneController = gameObject.GetComponent<GrandsonEugeneController>();

        UpdateInfo();
    }

    public void CompleteCurrentQuest()
    {
        questsController.CompleteQuest(quests[current_quest_index]);
    }

    public void UpdateInfo()
    {
        foreach (string quest in quests)
        {
            Debug.Log(quest);
            if (!questsController.dict_quest_name_to_quest[quest].is_quest_completed)
            {
                is_waiting_for_help = true;
            }
            if (questsController.dict_quest_name_to_quest[quest].is_quest_accepted)
            {
                is_quest_ongoing = true;
            }
        }
        /*
        if (is_quest_ongoing) grandsonEugeneController.ShowExclamationPointIcon();
        else if (is_waiting_for_help) grandsonEugeneController.ShowQuestionIcon();
        else grandsonEugeneController.ShowDialogIcon();
        */
    }

    public SpeachTree GetCurrentSpeachTree()
    {
        quests = questsController.dict_npc_to_list_of_quests_names[questsController.grandsonEugene];

        if (grandsonEugeneDialogScript == null) grandsonEugeneDialogScript = gameObject.GetComponent<GrandsonEugeneDialogScript>();
        SpeachTree result_speach_tree = grandsonEugeneDialogScript.text_hello;

        int temp_task_index = questsController.dict_quest_name_to_quest[quests[current_quest_index]].current_task_index;
        Quest temp_quest = questsController.dict_quest_name_to_quest[quests[current_quest_index]];
        Task temp_task = temp_quest.tasks[temp_task_index];

        if (temp_task.NPC == questsController.grandsonEugene && temp_task.current_speach_tree_index < temp_task.speach_trees.Count)
        {
            result_speach_tree = temp_task.speach_trees[temp_task.current_speach_tree_index];
        }

        return result_speach_tree;
    }
}
