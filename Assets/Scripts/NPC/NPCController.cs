using System.Collections.Generic;
using UnityEngine;
using static DialogController;

public class NPCController : InteractionController
{
    public string npc_name;

    public SpeachNodeSO speachNodeSO; 

    public Dialog default_dialog;

    public List<Dialog> dialog_list;

    protected void Start()
    {
        base.Start();

        default_dialog = new Dialog(speachNodeSO);
    }

    protected override void Interact()
    {
        StartDialog();
    }

    protected void StartDialog()
    {
        mainController.StartDialog(default_dialog);
    }
}
