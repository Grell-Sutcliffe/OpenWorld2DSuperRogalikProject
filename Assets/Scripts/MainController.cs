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
    public GameObject enterDangeonPanel;

    public GameObject taskShower;

    public GameObject Dedus;
    public GameObject GrandsonEugene;
    public GameObject Dangeon;

    ScrollInteractionScript scrollInteractionScript;

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;
    
    public DedusDialogScript dedusDialogScript;
    public GrandsonEugeneDialogScript grandsonEugeneDialogScript;

    DedusController dedusController;
    GrandsonEugeneController grandsonEugeneController;

    DangeonInteractionScript dangeonInteractionScript;

    public bool is_keyboard_active = true;

    SpriteRenderer current_interaction_SR;

    public List<SpriteRenderer> list_of_interactable_SR = new List<SpriteRenderer>();
    public List<string> list_of_interactable_objects_names = new List<string>();

    bool dedus_F;
    bool grandsonEugene_F;
    bool dangeon_F;

    void Start()
    {
        StuffSetActiveFalse();

        scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();

        SetDedusScripts();
        SetGrandsonEugeneScripts();
        SetDangeonScripts();

        is_keyboard_active = true;
    }

    public void ShowInteraction()
    {
        Debug.Log("SHOW INTERACTION");

        current_interaction_SR = null;

        list_of_interactable_SR.Clear();
        list_of_interactable_objects_names.Clear();

        if (dedus_F)
        {
            list_of_interactable_SR.Add(dedusController.interactIconSR);
            list_of_interactable_objects_names.Add(Dedus.name);
        }
        if (grandsonEugene_F)
        {
            list_of_interactable_SR.Add(grandsonEugeneController.interactIconSR);
            list_of_interactable_objects_names.Add(GrandsonEugene.name);
        }
        if (dangeon_F)
        {
            list_of_interactable_SR.Add(dangeonInteractionScript.interactIconSR);
            list_of_interactable_objects_names.Add(Dangeon.name);
        }

        if (scrollInteractionScript == null) scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();

        if (scrollInteractionScript.current_index >= list_of_interactable_objects_names.Count)
        {
            scrollInteractionScript.current_index = 0;
        }
        scrollInteractionScript.ApplyAllColors();
    }

    public void PressF()
    {
        if (list_of_interactable_objects_names.Count == 0) return;

        //Debug.Log($"list.Count = {list_of_interactable_objects_names.Count}, dangeon_F = {dangeon_F}, dangeon.name = {Dangeon.name}, list_names[cur_ind] = {list_of_interactable_objects_names[scrollInteractionScript.current_index]}");

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
        else if (dangeon_F && Dangeon.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (dangeonInteractionScript == null) SetDangeonScripts();
            ShowEnterDangeonPanel();
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
        enterDangeonPanel.SetActive(false);

        dedus_F = false;
        grandsonEugene_F = false;
        dangeon_F = false;
    }

    public void EnterDangeon()
    {
        Debug.Log("ENTER DANGEON");
        dangeonInteractionScript.EnterDangeon();
    }

    public void ShowPlayerPanel()
    {
        playerPanel.SetActive(true);
    }

    public void HidePlayerPanel()
    {
        playerPanel.SetActive(false);
    }

    public void ShowEnterDangeonPanel()
    {
        enterDangeonPanel.SetActive(true);
        TurnOffKeyboard();
    }

    public void HideEnterDangeonPanel()
    {
        enterDangeonPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void DedusOn()
    {
        dedus_F = true;
        ShowInteraction();
        Debug.Log("Dedus on");
    }

    public void DedusOff()
    {
        dedus_F = false;
        ShowInteraction();
        Debug.Log("Dedus off");
    }

    public void GrandsonEugeneOn()
    {
        grandsonEugene_F = true;
        ShowInteraction();
        Debug.Log("Grandson on");
    }

    public void GrandsonEugeneOff()
    {
        grandsonEugene_F = false;
        ShowInteraction();
        Debug.Log("Grandson off");
    }

    public void DangeonOn()
    {
        dangeon_F = true;
        ShowInteraction();
        Debug.Log("Dangeon on");
    }

    public void DangeonOff()
    {
        dangeon_F = false;
        ShowInteraction();
        Debug.Log("Dangeon off");
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

    void SetDangeonScripts()
    {
        if (Dangeon == null) Dangeon = GameObject.Find("Dangeon");
        if (Dangeon != null)
        {
            dangeonInteractionScript = Dangeon.GetComponent<DangeonInteractionScript>();
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
