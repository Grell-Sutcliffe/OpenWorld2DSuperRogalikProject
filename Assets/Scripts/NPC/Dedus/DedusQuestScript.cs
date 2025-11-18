using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;


public class DedusQuestScript : MonoBehaviour
{
    QuestsController questsController;

    DedusDialogScript dedusDialogScript;

    public List<string> quests = new List<string>();

    int current_quest_index = 0;

    void Start()
    {
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        dedusDialogScript = gameObject.GetComponent<DedusDialogScript>();
    }

    public SpeachTree GetCurrentSpeachTree()
    {
        quests = questsController.dict_npc_to_list_of_quests_names[questsController.dedus];

        if (dedusDialogScript == null) dedusDialogScript = gameObject.GetComponent<DedusDialogScript>();
        SpeachTree temp_speach_tree = dedusDialogScript.text_hello;

        int temp_task_index = questsController.dict_quest_name_to_quest[quests[current_quest_index]].current_task_index;
        Quest temp_quest = questsController.dict_quest_name_to_quest[quests[current_quest_index]];
        Task temp_task = temp_quest.tasks[temp_task_index];
        if (temp_task != null)
        {
            Debug.Log($"{temp_task.NPC} == {questsController.dedus}, {temp_task.title}, {temp_task_index}");
        }
        if (temp_task.NPC == questsController.dedus)
        {
            temp_speach_tree = temp_task.speach_trees[temp_task.current_speach_index];
        }

        if (!temp_speach_tree.repeatable) temp_task.current_speach_index++;

        return temp_speach_tree;
    }
}
