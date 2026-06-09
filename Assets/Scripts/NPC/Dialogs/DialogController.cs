using System.Collections.Generic;
using UnityEngine;

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
    public List<CollectableItem> list_of_CollectableItems;

    ItemDeliverySpeachNodeSO data;

    public ItemDeliverySpeachNode(ItemDeliverySpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        this.list_of_CollectableItems = new List<CollectableItem>();
        for (int i = 0; i < data.list_of_item_amounts.Count; i++)
        {
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
    public List<CollectableItem> list_of_CollectableItems;

    ItemAcceptSpeachNodeSO data;

    public ItemAcceptSpeachNode(ItemAcceptSpeachNodeSO data) : base(data.next_speachNodeSO, data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        this.list_of_CollectableItems = new List<CollectableItem>();
        for (int i = 0; i < data.list_of_item_amounts.Count; i++)
        {
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
    public SpeachNodeSO next_speachNodeSO;

    DefaultSpeachNodeSO data;

    public DefaultSpeachNode(DefaultSpeachNodeSO data) : base(data.npcSO, data.speach, data.speach_type, data.is_finishing)
    {
        this.data = data;

        this.next_speachNodeSO = data.next_speachNodeSO;
    }

    public DefaultSpeachNode(SpeachNodeSO next_speachNodeSO, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(npcSO, speach, speach_type, is_finishing)
    {
        this.next_speachNodeSO = next_speachNodeSO;
    }
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

    public FunctionSpeachNode(string function_name, SpeachNodeSO next_speachNodeSO, NPCSO npcSO, string speach, SpeachType speach_type = SpeachType.Speach, bool is_finishing = false) : base(next_speachNodeSO, npcSO, speach, speach_type, is_finishing)
    {
        this.function_name = function_name;
    }
}

public class Dialog
{
    public string title;
    public SpeachNodeSO current_speachNodeSO;
    public bool is_finished = false;

    public string dialog_starting_npc;

    DialogSO data;

    public Dialog(DialogSO data)
    {
        if (data == null) return;

        this.data = data;

        this.title = data.title;
        this.current_speachNodeSO = data.first_speachNode;

        this.dialog_starting_npc = data.dialog_starting_npcSO.npc_name;
    }
}

public class DialogController : MonoBehaviour
{
    protected MainController mainController;
    protected QuestsController questsController;   
    protected BackPackController backpackController;

    public Dictionary<string, Dialog> dict_dialog_title_to_dialog;

    public List<DialogSO> dialogs = new List<DialogSO>();
    
    public static DialogController Instance { get; private set; }

    private const string DialogsSaveKey = "dialogs_save";

    private Dictionary<string, bool> dict_dialog_title_to_is_finished = new Dictionary<string, bool>();

    [System.Serializable]
    public class DialogsSaveData
    {
        public List<DialogFinishedSaveData> dialogs = new List<DialogFinishedSaveData>();
    }

    [System.Serializable]
    public class DialogFinishedSaveData
    {
        public string dialogTitle;
        public bool isFinished;

        public DialogFinishedSaveData(string dialogTitle, bool isFinished)
        {
            this.dialogTitle = dialogTitle;
            this.isFinished = isFinished;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        dict_dialog_title_to_dialog = new Dictionary<string, Dialog>();
        dict_dialog_title_to_is_finished = new Dictionary<string, bool>();

        LoadDialogs();

        foreach (DialogSO dialogSO in dialogs)
        {
            AddNewDialog(dialogSO);
        }
    }

    public void AddNewDialog(DialogSO dialogSO)
    {
        if (dialogSO == null) return;

        Dialog dialog = new Dialog(dialogSO);

        if (dict_dialog_title_to_is_finished.ContainsKey(dialog.title))
        {
            dialog.is_finished = dict_dialog_title_to_is_finished[dialog.title];
        }

        dict_dialog_title_to_dialog[dialog.title] = dialog;

        if (!dict_dialog_title_to_is_finished.ContainsKey(dialog.title))
        {
            dict_dialog_title_to_is_finished[dialog.title] = dialog.is_finished;
        }
    }

    public void SetDialogFinished(string dialogTitle, bool isFinished = true)
    {
        if (string.IsNullOrEmpty(dialogTitle)) return;

        dict_dialog_title_to_is_finished[dialogTitle] = isFinished;

        if (dict_dialog_title_to_dialog.ContainsKey(dialogTitle))
        {
            dict_dialog_title_to_dialog[dialogTitle].is_finished = isFinished;
        }
    }

    public bool IsDialogFinished(string dialogTitle)
    {
        if (string.IsNullOrEmpty(dialogTitle)) return false;

        if (dict_dialog_title_to_is_finished.ContainsKey(dialogTitle))
        {
            return dict_dialog_title_to_is_finished[dialogTitle];
        }

        if (dict_dialog_title_to_dialog.ContainsKey(dialogTitle))
        {
            return dict_dialog_title_to_dialog[dialogTitle].is_finished;
        }

        return false;
    }

    public void SaveDialogs()
    {
        DialogsSaveData saveData = new DialogsSaveData();

        foreach (var pair in dict_dialog_title_to_dialog)
        {
            string dialogTitle = pair.Key;
            bool isFinished = pair.Value.is_finished;

            dict_dialog_title_to_is_finished[dialogTitle] = isFinished;

            saveData.dialogs.Add(new DialogFinishedSaveData(dialogTitle, isFinished));
        }

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(DialogsSaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Dialogs saved: " + json);
    }

    public void LoadDialogs()
    {
        dict_dialog_title_to_is_finished.Clear();

        if (!PlayerPrefs.HasKey(DialogsSaveKey))
        {
            Debug.Log("No dialogs save found");
            return;
        }

        string json = PlayerPrefs.GetString(DialogsSaveKey);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("Dialogs save is empty");
            return;
        }

        DialogsSaveData saveData = JsonUtility.FromJson<DialogsSaveData>(json);

        if (saveData == null || saveData.dialogs == null)
        {
            Debug.LogWarning("Dialogs save is broken");
            return;
        }

        foreach (DialogFinishedSaveData dialogData in saveData.dialogs)
        {
            dict_dialog_title_to_is_finished[dialogData.dialogTitle] = dialogData.isFinished;
        }

        Debug.Log("Dialogs loaded: " + json);
    }

    public void DeleteDialogs()
    {
        PlayerPrefs.DeleteKey(DialogsSaveKey);
        PlayerPrefs.Save();

        dict_dialog_title_to_is_finished.Clear();

        foreach (var pair in dict_dialog_title_to_dialog)
        {
            pair.Value.is_finished = false;
            dict_dialog_title_to_is_finished[pair.Key] = false;
        }

        Debug.Log("Dialogs reset to default");
    }
}

public enum SpeachType
{
    Speach = 0,
    Action = 1,
}
