using TMPro;
using UnityEngine;

public class QuestInfoScript : MonoBehaviour
{
    public TextMeshProUGUI quest_title_TMP;

    public string quest_title;

    void Start()
    {
        
    }

    public void SetNewQuestTitle(string new_title)
    {
        quest_title = new_title;

        quest_title_TMP.text = quest_title;
    }
}
