using TMPro;
using UnityEngine;

public class QuestInfoScript : MonoBehaviour
{
    public TextMeshProUGUI quest_title_TMP;
    public TextMeshProUGUI quest_description_TMP;

    public string quest_title;
    public string quest_description;

    void Start()
    {
        
    }

    public void SetNewQuestTitle(string new_title)
    {
        quest_title = new_title;
        quest_description = "";

        quest_title_TMP.text = quest_title;
        quest_description_TMP.text = quest_description;
    }

    public void SetNewQuestTitle(string new_title, string new_description)
    {
        quest_title = new_title;
        quest_description = new_description;

        quest_title_TMP.text = quest_title;
        quest_description_TMP.text = quest_description;
    }
}
