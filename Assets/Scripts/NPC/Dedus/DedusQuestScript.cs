using UnityEngine;

public class DedusQuestScript : MonoBehaviour
{
    QuestsController questsController;

    public int quest_index;

    void Start()
    {
        questsController = GameObject.Find("QuestController").GetComponent<QuestsController>();
    }


}
