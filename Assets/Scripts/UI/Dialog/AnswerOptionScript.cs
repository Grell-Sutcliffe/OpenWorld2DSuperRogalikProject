using TMPro;
using UnityEngine;
using static DialogController;

public class AnswerOptionScript : MonoBehaviour
{
    public TextMeshProUGUI answerOptionText;
    public SpeachNode next_node;

    DialogPanelScript dialogPanelScript;

    private void Start()
    {
        dialogPanelScript = GameObject.Find("DialogPanel").GetComponent<DialogPanelScript>();
    }

    public void MakeAnswerOption(string answer_text, SpeachNode new_node)
    {
        answerOptionText.text = answer_text;
        next_node = new_node;
    }

    public void SelectAnswerOption()
    {
        dialogPanelScript.SelectAnswer(next_node);
    }
}
