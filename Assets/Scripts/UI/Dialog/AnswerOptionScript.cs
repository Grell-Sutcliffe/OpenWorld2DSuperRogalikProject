using TMPro;
using UnityEngine;
using static DialogPanelScript;

public class AnswerOptionScript : MonoBehaviour
{
    public TextMeshProUGUI answerOptionText;
    public SpeachNode next_node;

    DialogPanelScript dialogPanelScript;

    private void Start()
    {
        dialogPanelScript = GameObject.Find("DialogPanel").GetComponent<DialogPanelScript>();
    }

    public void MakeAnswerOption(SpeachNode new_node)
    {
        next_node = new_node;

        if (next_node != null)
        {
            answerOptionText.text = next_node.answer_text;
        }
    }

    public void SelectAnswerOption()
    {
        dialogPanelScript.SelectAnswer(next_node);
    }
}
