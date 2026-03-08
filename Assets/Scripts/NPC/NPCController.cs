using System.Collections.Generic;
using UnityEngine;
using static DialogController;

public class NPCController : InteractionController
{
    DialogController dialogController;

    public string npc_name;

    public DialogSO default_dialogSO;

    //public Dialog default_dialog;
    public string default_dialog_title;

    //public List<Dialog> dialog_list;

    public int disposition_to_player;

    protected void Awake()
    {
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
    }

    protected void Start()
    {
        base.Start();

        //default_dialog = new Dialog(default_dialogSO);
        default_dialog_title = default_dialogSO.title;
        dialogController.AddNewDialog(default_dialogSO);
    }

    protected override void Interact()
    {
        StartDialog();
    }

    protected void StartDialog()
    {
        mainController.StartDialog(dialogController.dict_dialog_title_to_dialog[default_dialog_title]);
    }

    public void ChangeDispositionToPlayer(int delta_disposition_to_player)
    {
        disposition_to_player += delta_disposition_to_player;
    }
}
