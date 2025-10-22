using System;
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

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;
    
    public DedusDialogScript dedusDialogScript;
    public GrandsonEugeneDialogScript grandsonEugeneDialogScript;

    public bool is_keyboard_active = true;

    bool dedus_F;
    bool grandsonEugene_F;

    void Start()
    {
        StuffSetActiveFalse();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();

        SetDedusDialogScript();
        SetGrandsonEugeneDialogScript();

        is_keyboard_active = true;
    }

    public void PressF()
    {
        if (dedus_F)
        {
            if (dedusDialogScript == null) SetDedusDialogScript();
            dedusDialogScript.StartDialog();
        }
        else if (grandsonEugene_F)
        {
            if (grandsonEugeneDialogScript == null) SetGrandsonEugeneDialogScript();
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
    }

    public void DedusOff()
    {
        dedus_F = false;
    }

    public void GrandsonEugeneOn()
    {
        grandsonEugene_F = true;
    }

    public void GrandsonEugeneOff()
    {
        grandsonEugene_F = false;
    }

    void SetDedusDialogScript()
    {
        if (Dedus == null) Dedus = GameObject.Find("Dedus");
        if (Dedus != null) dedusDialogScript = Dedus.GetComponent<DedusDialogScript>();
    }

    void SetGrandsonEugeneDialogScript()
    {
        if (GrandsonEugene == null) GrandsonEugene = GameObject.Find("GrandsonEugine");
        if (GrandsonEugene != null) grandsonEugeneDialogScript = GrandsonEugene.GetComponent<GrandsonEugeneDialogScript>();
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
