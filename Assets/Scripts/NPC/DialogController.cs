using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DialogController : MonoBehaviour
{
    protected MainController mainController;
    protected QuestsController questsController;

    public string npc_name;

    public Dialog default_dialog;

    public List<Dialog> dialog_list;

    public class Answer
    {
        string answer_text;
        // AnswerType type_of_answer_text;
        SpeachNode next_speachNode;
    }

    public abstract class SpeachNode
    {
        string speaker_name;
        string speach;

        public SpeachNode(string speaker_name, string speach)
        {
            this.speaker_name = speaker_name;
            this.speach = speach;
        }
    }

    public class AnswerableSpeachNode : SpeachNode
    {
        List<Answer> answers;

        public AnswerableSpeachNode(List<Answer> answers, string speaker_name, string speach) : base(speaker_name, speach)
        {
            this.answers = answers;
        }
    }

    public class DefaultSpeachNode : SpeachNode
    {
        SpeachNode next_speachNode;

        public DefaultSpeachNode(SpeachNode next_speachNode, string speaker_name, string speach) : base(speaker_name, speach)
        {
            this.next_speachNode = next_speachNode;
        }
    }

    public class FunctionSpeachNode : SpeachNode
    {
        string function_name;

        public FunctionSpeachNode(string function_name, string speaker_name, string speach) : base(speaker_name, speach)
        {
            this.function_name = function_name;
        }
    }

    public class Dialog
    {

    }

    protected void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();
    }

    protected void Start()
    {
        CreateSpeach();
    }

    public void StartDialog()
    {
        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();
        //mainController.StartDialog(npc_name, text_hello.root);

        //mainController.StartDialog(npc_name, TheLostGrandson_ask_for_search_grandson_1);
        //mainController.StartDialog(npc_name, grandsonEugineQuestScript.GetCurrentSpeachTree());
    }

    protected void CreateSpeach()
    {

    }
}
