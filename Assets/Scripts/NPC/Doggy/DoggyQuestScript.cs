using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DoggyQuestScript : MonoBehaviour
{
    QuestsController questsController;

    DoggyController doggyController;
    DoggyDialogScript doggyDialogScript;

    public List<string> quests = new List<string>();

    int current_quest_index = 0;

    public bool is_waiting_for_help = false;
    public bool is_quest_ongoing = false;

    void Start()
    {
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        doggyDialogScript = gameObject.GetComponent<DoggyDialogScript>();
        doggyController = gameObject.GetComponent<DoggyController>();

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

        Debug.Log($"HEEEY {is_quest_ongoing}, {is_waiting_for_help}");

        /*
        if (is_quest_ongoing) doggyController.ShowExclamationPointIcon();
        else if (is_waiting_for_help) doggyController.ShowQuestionIcon();
        else doggyController.ShowDialogIcon();
        */
    }

    public SpeachTree GetCurrentSpeachTree()
    {
        quests = questsController.dict_npc_to_list_of_quests_names[questsController.doggy];

        if (doggyDialogScript == null) doggyDialogScript = gameObject.GetComponent<DoggyDialogScript>();
        SpeachTree result_speach_tree = doggyDialogScript.text_hello;

        string temp_quest_name = quests[current_quest_index];  // название квеста
        Quest temp_quest = questsController.dict_quest_name_to_quest[temp_quest_name];  // квест
        int temp_task_index = temp_quest.current_task_index;  // индекс текущего задания в квесте
        Task temp_task = temp_quest.tasks[temp_task_index];  // задание

        /*
        Debug.Log($"{temp_task.title} : current_index = {temp_task.current_speach_tree_index}, speach_trees.Count = {temp_task.speach_trees.Count}");
        SpeachTree temp_speach_tree = temp_task.speach_trees[temp_task.current_speach_tree_index];  // текущий диалог в текущем задании
        */

        /*
        if (temp_speach_tree.is_finished)  // если текущий диалог завершён
        {
            Debug.Log($"{temp_task.NPC} == {questsController.doggy}, next speach");
            temp_task.current_speach_tree_index++;
        }

        int temp_tasks_amount = temp_quest.tasks.Count;
        int temp_task_speach_trees_amout = temp_task.speach_trees.Count;
        if (temp_task.current_speach_tree_index >= temp_task_speach_trees_amout) questsController.dict_quest_name_to_quest[quests[current_quest_index]].current_task_index++;

        temp_task_index = questsController.dict_quest_name_to_quest[quests[current_quest_index]].current_task_index;
        if (temp_task_index >= temp_tasks_amount) return null;
        temp_quest = questsController.dict_quest_name_to_quest[quests[current_quest_index]];
        temp_task = temp_quest.tasks[temp_task_index];

        temp_task_speach_trees_amout = temp_task.speach_trees.Count;
        if (temp_task.current_speach_tree_index >= temp_task_speach_trees_amout) return null;
        */

        if (temp_task.NPC == questsController.doggy && temp_task.current_speach_tree_index < temp_task.speach_trees.Count)
        {
            result_speach_tree = temp_task.speach_trees[temp_task.current_speach_tree_index];
        }

        return result_speach_tree;
    }
}
