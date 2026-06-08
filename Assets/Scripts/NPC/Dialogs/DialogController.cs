using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

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
    public NPCSO npcSO;
    public string speach;
    public SpeachType speach_type;

    public bool is_finishing;

    SpeachNodeSO data;

    public SpeachNode()
    {

    }

    public SpeachNode(SpeachNodeSO data)
    {
        this.data = data;

        this.npcSO = data.npcSO;
        this.speach = data.speach;
        this.speach_type = data.speach_type;

        this.is_finishing = data.is_finishing;
    }

    public SpeachNode(NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false)
    {
        this.npcSO = npcSO;
        this.speach = speach;
        this.speach_type = speach_type;
        this.is_finishing = is_finishing;
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
        if (speachNodeSO is ItemAcceptSpeachNodeSO itemAcceptSpeachNodeSO)
        {
            return new ItemAcceptSpeachNode(itemAcceptSpeachNodeSO);
        }
        if (speachNodeSO is ItemDeliverySpeachNodeSO itemDeliverySpeachNodeSO)
        {
            return new ItemDeliverySpeachNode(itemDeliverySpeachNodeSO);
        }
        if (speachNodeSO is QuestAcceptingSpeachNodeSO questAcceptingSpeachNodeSO)
        {
            return new QuestAcceptingSpeachNode(questAcceptingSpeachNodeSO);
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

    public AnswerableSpeachNode(AnswerableSpeachNodeSO data) : base(data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        this.answers = new List<Answer>();
        foreach (AnswerSO answerSO in data.answerSOs)
        {
            this.answers.Add(new Answer(answerSO));
        }
    }

    public AnswerableSpeachNode(List<Answer> answers, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(npcSO, speach, speach_type, is_finishing)
    {
        this.answers = answers;
    }
}

public class ItemDeliverySpeachNode : DefaultSpeachNode
{
    //public List<int> list_of_item_id;
    public List<CollectableItem> list_of_CollectableItems;

    ItemDeliverySpeachNodeSO data;

    public ItemDeliverySpeachNode(ItemDeliverySpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
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

    public ItemDeliverySpeachNode(List<CollectableItem> list_of_CollectableItems, ItemDeliverySpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.list_of_CollectableItems = list_of_CollectableItems;
    }
}

public class ItemAcceptSpeachNode : DefaultSpeachNode
{
    //public List<int> list_of_item_id;
    public List<CollectableItem> list_of_CollectableItems;

    ItemAcceptSpeachNodeSO data;

    public ItemAcceptSpeachNode(ItemAcceptSpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
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

    public ItemAcceptSpeachNode(List<CollectableItem> list_of_CollectableItems, ItemAcceptSpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.list_of_CollectableItems = list_of_CollectableItems;
    }
}

public class DefaultSpeachNode : SpeachNode
{
    //public SpeachNode next_speachNode;
    public SpeachNodeSO next_speachNodeSO;

    DefaultSpeachNodeSO data;

    public DefaultSpeachNode(DefaultSpeachNodeSO data) : base(data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        //this.next_speachNode = NewSpeachNode(data.next_speachNodeSO);
        this.next_speachNodeSO = data.next_speachNodeSO;
    }

    //public DefaultSpeachNode(SpeachNode next_speachNode, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(npcSO, speach, speach_type, is_finishing)
    public DefaultSpeachNode(SpeachNodeSO next_speachNodeSO, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(npcSO, speach, speach_type, is_finishing)
    {
        //this.next_speachNode = next_speachNode;
        this.next_speachNodeSO = next_speachNodeSO;
    }

    /*
    public DefaultSpeachNode(SpeachNodeSO next_speachNodeSO, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(npcSO, speach, speach_type, is_finishing)
    {
        //this.next_speachNode = NewSpeachNode(next_speachNodeSO);
        this.next_speachNode = NewSpeachNode(next_speachNodeSO);
    }
    */
}

public class QuestAcceptingSpeachNode : DefaultSpeachNode
{
    public string quest_title;

    QuestAcceptingSpeachNodeSO data;

    public QuestAcceptingSpeachNode(QuestAcceptingSpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        this.quest_title = data.questSO.title;
    }

}

public class FunctionSpeachNode : DefaultSpeachNode
{
    public string function_name;

    FunctionSpeachNodeSO data;

    public FunctionSpeachNode(FunctionSpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        this.function_name = data.function_name;
    }

    //public FunctionSpeachNode(string function_name, SpeachNode next_speachNode, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(next_speachNode, npcSO, speach, speach_type, is_finishing)
    public FunctionSpeachNode(string function_name, SpeachNodeSO next_speachNodeSO, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(next_speachNodeSO, npcSO, speach, speach_type, is_finishing)
    {
        this.function_name = function_name;
    }
}

public class Dialog
{
    public string title;
    //public SpeachNode current_speachNode;
    public SpeachNodeSO current_speachNodeSO;
    public bool is_finished = false;

    public string dialog_starting_npc;

    DialogSO data;

    public Dialog(DialogSO data)
    {
        if (data == null) return;

        this.data = data;

        this.title = data.title;
        //this.current_speachNode = new SpeachNode().NewSpeachNode(data.first_speachNode);
        this.current_speachNodeSO = data.first_speachNode;

        this.dialog_starting_npc = data.dialog_starting_npcSO.npc_name;
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

public class DialogController : MonoBehaviour
{
    protected MainController mainController;
    protected QuestsController questsController;   
    protected BackPackController backpackController;

    public Dictionary<string, Dialog> dict_dialog_title_to_dialog;

    public List<DialogSO> dialogs = new List<DialogSO>();
    public static DialogController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        dict_dialog_title_to_dialog = new Dictionary<string, Dialog>();

        foreach (DialogSO dialogSO in dialogs)
        {
            AddNewDialog(dialogSO);
        }
    }

    public void AddNewDialog(DialogSO dialogSO)
    {
        dict_dialog_title_to_dialog[dialogSO.title] = new Dialog(dialogSO);
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
