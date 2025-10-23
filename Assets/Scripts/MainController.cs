using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DialogPanelScript;

public class MainController : MonoBehaviour
{
    public GameObject playerPanel;
    public GameObject dialogPanel;
    public GameObject questPanel;
    public GameObject wishPanel;

    public GameObject taskShower;

    public GameObject Dedus;
    public GameObject GrandsonEugene;

    ScrollInteractionScript scrollInteractionScript;

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;
    
    public DedusDialogScript dedusDialogScript;
    public GrandsonEugeneDialogScript grandsonEugeneDialogScript;

    DedusController dedusController;
    GrandsonEugeneController grandsonEugeneController;

    public bool is_keyboard_active = true;

    SpriteRenderer current_interaction_SR;

    public List<SpriteRenderer> list_of_interactable_GO = new List<SpriteRenderer>();
    public List<string> list_of_interactable_objects_names = new List<string>();

    bool dedus_F;
    bool grandsonEugene_F;

    void Start()
    {
        StuffSetActiveFalse();

        scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();

        SetDedusScripts();
        SetGrandsonEugeneScripts();

        is_keyboard_active = true;
    }

    public void ShowInteraction()
    {
        current_interaction_SR = null;

        list_of_interactable_GO.Clear();
        list_of_interactable_objects_names.Clear();

        if (dedus_F)
        {
            list_of_interactable_GO.Add(dedusController.interactIconSR);
            list_of_interactable_objects_names.Add(Dedus.name);
        }
        if (grandsonEugene_F)
        {
            list_of_interactable_GO.Add(grandsonEugeneController.interactIconSR);
            list_of_interactable_objects_names.Add(GrandsonEugene.name);
        }

        if (scrollInteractionScript == null) scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();

        scrollInteractionScript.ApplyAllColors();
    }

    public void PressF()
    {
        if (dedus_F && Dedus.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (dedusDialogScript == null) SetDedusScripts();
            dedusDialogScript.StartDialog();
        }
        else if (grandsonEugene_F && GrandsonEugene.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (grandsonEugeneDialogScript == null) SetGrandsonEugeneScripts();
            grandsonEugeneDialogScript.StartDialog();
        }
    }

    public void StartDialog(string speaker_text, SpeachNode speach_node)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_node);
    }

    public void StartDialog(string speaker_text, SpeachTree speach_tree)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_tree);
    }

    void StuffSetActiveFalse()
    {
        dialogPanel.SetActive(false);
        questPanel.SetActive(false);

        dedus_F = false;
        grandsonEugene_F = false;
    }

    public void ShowPlayerPanel()
    {
        playerPanel.SetActive(true);
    }

    public void HidePlayerPanel()
    {
        playerPanel.SetActive(false);
    }

    public void DedusOn()
    {
        dedus_F = true;
        ShowInteraction();
    }

    public void DedusOff()
    {
        dedus_F = false;
        ShowInteraction();
    }

    public void GrandsonEugeneOn()
    {
        grandsonEugene_F = true;
        ShowInteraction();
    }

    public void GrandsonEugeneOff()
    {
        grandsonEugene_F = false;
        ShowInteraction();
    }

    void SetDedusScripts()
    {
        if (Dedus == null) Dedus = GameObject.Find("Dedus");
        if (Dedus != null)
        {
            dedusController = Dedus.GetComponent<DedusController>();
            dedusDialogScript = Dedus.GetComponent<DedusDialogScript>();
        }
    }

    void SetGrandsonEugeneScripts()
    {
        if (GrandsonEugene == null) GrandsonEugene = GameObject.Find("GrandsonEugine");
        if (GrandsonEugene != null)
        {
            grandsonEugeneController = GrandsonEugene.GetComponent<GrandsonEugeneController>();
            grandsonEugeneDialogScript = GrandsonEugene.GetComponent<GrandsonEugeneDialogScript>();
        }
    }

    public void OpenQuestPanel()
    {
        questPanel.SetActive(true);
        TurnOffKeyboard();
    }

    public void CloseQuestPanel()
    {
        questPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void OpenWishPanel()
    {
        wishPanel.SetActive(true);
        TurnOffKeyboard();
    }

    public void CloseWishPanel()
    {
        wishPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void TurnOnKeyboard()
    {
        is_keyboard_active = true;
    }

    public void TurnOffKeyboard()
    {
        is_keyboard_active = false;
    }
}
