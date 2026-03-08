using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DialogController : MonoBehaviour
{
    protected MainController mainController;
    protected QuestsController questsController;   
    protected BackPackController backpackController;

    public Dictionary<string, Dialog> dict_dialog_title_to_dialog;

    private void Awake()
    {
        dict_dialog_title_to_dialog = new Dictionary<string, Dialog>();
    }

    public void AddNewDialog(DialogSO dialogSO)
    {
        dict_dialog_title_to_dialog[dialogSO.title] = new Dialog(dialogSO);
    }

    public class Answer
    {
        public string answer_text;
        public SpeachNode next_speachNode;
        public int delta_disposition_to_player = 0;

        AnswerSO data;

        public Answer(AnswerSO data)
        {
            this.data = data;

            this.answer_text = data.answer_text;
            //this.next_speachNode = new SpeachNode(data.next_speachNode);  // так нельзя, тк класс SpeachNode - абстрактный

            this.next_speachNode = new SpeachNode().NewSpeachNode(data.next_speachNode);
            this.delta_disposition_to_player = data.delta_disposition_to_player;
        }

        public Answer(string answer_text, SpeachNode next_speachNode, int delta_disposition_to_player)
        {
            this.answer_text = answer_text;
            this.next_speachNode = next_speachNode;
            this.delta_disposition_to_player = delta_disposition_to_player;
        }
    }

    public class SpeachNode
    {
        public NPCController npcController;
        public string speach;
        public SpeachType speach_type;

        SpeachNodeSO data;

        public SpeachNode()
        {

        }

        public SpeachNode(SpeachNodeSO data)
        {
            this.data = data;

            this.npcController = data.npcController;
            this.speach = data.speach;
            this.speach_type = data.speach_type;
        }

        public SpeachNode(NPCController npcController, string speach, SpeachType speach_type = SpeachType.Speach)
        {
            this.npcController = npcController;
            this.speach = speach;
            this.speach_type = speach_type;
        }

        public SpeachNode NewSpeachNode(SpeachNodeSO speachNodeSO)
        {
            if (speachNodeSO is AnswerableSpeachNodeSO answerableSpeachNodeSO)
            {
                return new AnswerableSpeachNode(answerableSpeachNodeSO);
            }
            if (speachNodeSO is FunctionSpeachNodeSO functionSpeachNodeSO)
            {
                return new FunctionSpeachNode(functionSpeachNodeSO);
            }
            if (speachNodeSO is ItemDeliverySpeachNodeSO itemDeliverySpeachNodeSO)
            {
                return new ItemDeliverySpeachNode(itemDeliverySpeachNodeSO);
            }
            if (speachNodeSO is DefaultSpeachNodeSO defaultSpeachNodeSO)
            {
                return new DefaultSpeachNode(defaultSpeachNodeSO);
            }

            return null;
        }
    }

    public class AnswerableSpeachNode : SpeachNode
    {
        public List<Answer> answers;

        AnswerableSpeachNodeSO data;

        public AnswerableSpeachNode(AnswerableSpeachNodeSO data) : base(data.npcController, data.speach, data.speach_type)
        {
            this.data = data;

            this.answers = new List<Answer>();
            foreach (AnswerSO answerSO in data.answerSOs)
            {
                this.answers.Add(new Answer(answerSO));
            }
        }

        public AnswerableSpeachNode(List<Answer> answers, NPCController npcController, string speach, SpeachType speach_type = SpeachType.Speach) : base(npcController, speach, speach_type)
        {
            this.answers = answers;
        }
    }

    public class ItemDeliverySpeachNode : DefaultSpeachNode
    {
        //public List<int> list_of_item_id;
        public List<CollectableItem> list_of_CollectableItems;

        ItemDeliverySpeachNodeSO data;

        public ItemDeliverySpeachNode(ItemDeliverySpeachNodeSO data) : base(data.next_speachNode, data.npcController, data.speach, data.speach_type)
        {
            this.data = data;

            //this.list_of_item_id = new List<int>();
            this.list_of_CollectableItems = new List<CollectableItem>();
            for (int i = 0; i < data.list_of_item_amounts.Count; i++)
            {
                //this.list_of_item_id.Add(new Item(itemSO));
                this.list_of_CollectableItems.Add(new CollectableItem(new Item(data.list_of_itemSOs[i]).item_name, data.list_of_item_amounts[i]));
            }
        }

        public ItemDeliverySpeachNode(List<CollectableItem> list_of_CollectableItems, ItemDeliverySpeachNodeSO data) : base(data.next_speachNode, data.npcController, data.speach, data.speach_type)
        {
            this.list_of_CollectableItems = list_of_CollectableItems;
        }
    }

    public class DefaultSpeachNode : SpeachNode
    {
        public SpeachNode next_speachNode;

        DefaultSpeachNodeSO data;

        public DefaultSpeachNode(DefaultSpeachNodeSO data) : base(data.npcController, data.speach, data.speach_type)
        {
            this.data = data;

            this.next_speachNode = NewSpeachNode(data.next_speachNode);
        }

        public DefaultSpeachNode(SpeachNode next_speachNode, NPCController npcController, string speach, SpeachType speach_type = SpeachType.Speach) : base(npcController, speach, speach_type)
        {
            this.next_speachNode = next_speachNode;
        }

        public DefaultSpeachNode(SpeachNodeSO next_speachNodeSO, NPCController npcController, string speach, SpeachType speach_type = SpeachType.Speach) : base(npcController, speach, speach_type)
        {
            this.next_speachNode = NewSpeachNode(next_speachNodeSO);
        }
    }

    public class FunctionSpeachNode : DefaultSpeachNode
    {
        public string function_name;

        FunctionSpeachNodeSO data;

        public FunctionSpeachNode(FunctionSpeachNodeSO data) : base(data.next_speachNode, data.npcController, data.speach, data.speach_type)
        {
            this.data = data;

            this.function_name = data.function_name;
        }

        public FunctionSpeachNode(string function_name, SpeachNode next_speachNode, NPCController npcController, string speach, SpeachType speach_type = SpeachType.Speach) : base(next_speachNode, npcController, speach, speach_type)
        {
            this.function_name = function_name;
        }
    }

    public class Dialog
    {
        public string title;
        public SpeachNode current_speachNode;
        public bool is_finished = false;

        DialogSO data;

        public Dialog(DialogSO data)
        {
            if (data == null) return;

            this.data = data;

            this.title = data.title;
            this.current_speachNode = new SpeachNode().NewSpeachNode(data.first_speachNode);
        }

        /*
        public Dialog(SpeachNodeSO current_speachNodeSO)
        {
            this.current_speachNode = new SpeachNode().NewSpeachNode(current_speachNodeSO);
        }

        public Dialog(SpeachNode current_speachNode)
        {
            this.current_speachNode = current_speachNode;
        }
        */
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
