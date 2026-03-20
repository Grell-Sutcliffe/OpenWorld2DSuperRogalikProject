using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogController;
using static UnityEngine.GraphicsBuffer;

public class NPCController : InteractionController
{
    public NPCSO data;

    protected QuestsController questsController;
    protected DialogController dialogController;

    public GameObject icon_thinking;
    public GameObject icon_hasUnacceptedQuest;  // !
    public GameObject icon_hasAcceptedQuest;    // ?

    public string npc_name;

    public DialogSO default_dialogSO;

    public List<DialogSO> list_of_dialogs_to_quests = new List<DialogSO>();

    //public Dialog default_dialog;
    public string default_dialog_title;

    //public List<Dialog> dialog_list;

    public int disposition_to_player;

    protected void Awake()
    {
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        npc_name = data.npc_name;
    }

    protected void Start()
    {
        base.Start();

        //default_dialog = new Dialog(default_dialogSO);
        if (default_dialogSO != null)
        {
            default_dialog_title = default_dialogSO.title;
            dialogController.AddNewDialog(default_dialogSO);
        }

        foreach (DialogSO dialogSO in list_of_dialogs_to_quests)
        {
            dialogController.AddNewDialog(dialogSO);
        }
    }

    protected override void Interact()
    {
        StartDialog();
    }

    protected void StartDialog()
    {
        foreach (string quest_title in questsController.accepted_quests)
        {
            if (questsController.dict_quest_name_to_quest[quest_title].current_task is DialogTask dialogTask)
            {
                if (dialogController.dict_dialog_title_to_dialog[dialogTask.dialog_title].dialog_starting_npc == this.npc_name)
                {
                    mainController.StartDialog(dialogController.dict_dialog_title_to_dialog[dialogTask.dialog_title]);
                    return;
                }
            }
        }

        foreach (DialogSO dialogSO in list_of_dialogs_to_quests)
        {
            if (dialogController.dict_dialog_title_to_dialog[dialogSO.title].is_finished == false)
            {
                mainController.StartDialog(dialogController.dict_dialog_title_to_dialog[dialogSO.title]);
                return;
            }
        }

        mainController.StartDialog(dialogController.dict_dialog_title_to_dialog[default_dialog_title]);
    }

    public void ChangeDispositionToPlayer(int delta_disposition_to_player)
    {
        disposition_to_player += delta_disposition_to_player;
    }

    protected IEnumerator IconThinkingActivate()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);

            icon_thinking.SetActive(true);

            yield return new WaitForSeconds(5);

            icon_thinking.SetActive(false);
        }
    }

    public void IconThinkingSetActiveTrue()
    {
        IconsSetActiveFalse();

        StartCoroutine(IconThinkingActivate());
    }

    public void IconHasAcceptedQuestSetActiveTrue()
    {
        IconsSetActiveFalse();

        icon_hasAcceptedQuest.SetActive(true);
    }

    public void IconHasUnacceptedQuestSetActiveTrue()
    {
        IconsSetActiveFalse();

        icon_hasUnacceptedQuest.SetActive(true);
    }

    protected void IconsSetActiveFalse()
    {
        StopCoroutine(IconThinkingActivate());

        icon_thinking.SetActive(false);
        icon_hasAcceptedQuest.SetActive(false);
        icon_hasUnacceptedQuest.SetActive(false);
    }
}
