using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DialogController : MonoBehaviour
{
    protected MainController mainController;
    protected QuestsController questsController;    

    public class Answer
    {
        public string answer_text;
        public SpeachNode next_speachNode;

        AnswerSO data;

        public Answer(AnswerSO data)
        {
            this.data = data;

            this.answer_text = data.answer_text;
            //this.next_speachNode = new SpeachNode(data.next_speachNode);  // так нельзя, тк класс SpeachNode - абстрактный

            this.next_speachNode = new SpeachNode().NewSpeachNode(data.next_speachNode);
        }

        public Answer(string answer_text, SpeachNode next_speachNode)
        {
            this.answer_text = answer_text;
            this.next_speachNode = next_speachNode;
        }
    }

    public class SpeachNode
    {
        public string speaker_name;
        public string speach;
        public SpeachType speach_type;

        SpeachNodeSO data;

        public SpeachNode()
        {
            this.speaker_name = "";
            this.speach = "";
        }

        public SpeachNode(SpeachNodeSO data)
        {
            this.data = data;

            this.speaker_name = data.speaker_name;
            this.speach = data.speach;
            this.speach_type = data.speach_type;
        }

        public SpeachNode(string speaker_name, string speach, SpeachType speach_type = SpeachType.Speach)
        {
            this.speaker_name = speaker_name;
            this.speach = speach;
            this.speach_type = speach_type;
        }

        public SpeachNode NewSpeachNode(SpeachNodeSO speachNodeSO)
        {
            if (speachNodeSO is AnswerableSpeachNodeSO answerableSpeachNodeSO)
            {
                return new AnswerableSpeachNode(answerableSpeachNodeSO);
            }
            if (speachNodeSO is DefaultSpeachNodeSO defaultSpeachNodeSO)
            {
                return new DefaultSpeachNode(defaultSpeachNodeSO);
            }
            if (speachNodeSO is FunctionSpeachNodeSO functionSpeachNodeSO)
            {
                return new FunctionSpeachNode(functionSpeachNodeSO);
            }

            return null;
        }
    }

    public class AnswerableSpeachNode : SpeachNode
    {
        public List<Answer> answers;

        AnswerableSpeachNodeSO data;

        public AnswerableSpeachNode(AnswerableSpeachNodeSO data) : base(data.speaker_name, data.speach, data.speach_type)
        {
            this.data = data;

            this.answers = new List<Answer>();
            foreach (AnswerSO answerSO in data.answerSOs)
            {
                this.answers.Add(new Answer(answerSO));
            }
        }

        public AnswerableSpeachNode(List<Answer> answers, string speaker_name, string speach, SpeachType speach_type = SpeachType.Speach) : base(speaker_name, speach, speach_type)
        {
            this.answers = answers;
        }
    }

    public class DefaultSpeachNode : SpeachNode
    {
        public SpeachNode next_speachNode;

        DefaultSpeachNodeSO data;

        public DefaultSpeachNode(DefaultSpeachNodeSO data) : base(data.speaker_name, data.speach, data.speach_type)
        {
            this.data = data;

            this.next_speachNode = NewSpeachNode(data.next_speachNode);
        }

        public DefaultSpeachNode(SpeachNode next_speachNode, string speaker_name, string speach, SpeachType speach_type = SpeachType.Speach) : base(speaker_name, speach, speach_type)
        {
            this.next_speachNode = next_speachNode;
        }

        public DefaultSpeachNode(SpeachNodeSO next_speachNodeSO, string speaker_name, string speach, SpeachType speach_type = SpeachType.Speach) : base(speaker_name, speach, speach_type)
        {
            this.next_speachNode = NewSpeachNode(next_speachNodeSO);
        }
    }

    public class FunctionSpeachNode : DefaultSpeachNode
    {
        public string function_name;

        FunctionSpeachNodeSO data;

        public FunctionSpeachNode(FunctionSpeachNodeSO data) : base(data.next_speachNode, data.speaker_name, data.speach, data.speach_type)
        {
            this.data = data;

            this.function_name = data.function_name;
        }

        public FunctionSpeachNode(string function_name, SpeachNode next_speachNode, string speaker_name, string speach, SpeachType speach_type = SpeachType.Speach) : base(next_speachNode, speaker_name, speach, speach_type)
        {
            this.function_name = function_name;
        }
    }

    public class Dialog
    {
        public SpeachNode current_speachNode;

        public Dialog(SpeachNodeSO current_speachNodeSO)
        {
            this.current_speachNode = new SpeachNode().NewSpeachNode(current_speachNodeSO);
        }

        public Dialog(SpeachNode current_speachNode)
        {
            this.current_speachNode = current_speachNode;
        }
    }

    /*
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
    */
}

public enum SpeachType
{
    Speach = 0,
    Action = 1,
}
