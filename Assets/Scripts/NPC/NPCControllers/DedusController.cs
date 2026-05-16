using UnityEngine;

public class DedusController : NPCController
{
    public GameObject iconTask_1;
    public GameObject iconTask_7;
    public GameObject iconDialog;

    void Start()
    {
        base.Start();

        StuffSetActiveFalse();

        interactIconSR = interactIcon.GetComponent<SpriteRenderer>();
    }
    
    void StuffSetActiveFalse()
    {
        iconTask_1.SetActive(false);
        iconTask_7.SetActive(false);
        iconDialog.SetActive(false);
    }

    public void ShowExclamationPointIcon()
    {
        HideAllIcons();
        iconTask_1.SetActive(true);
    }

    public void HideExclamationPointIcon()
    {
        iconTask_1.SetActive(false);
    }

    public void ShowQuestionIcon()
    {
        HideAllIcons();
        iconTask_7.SetActive(true);
    }

    public void HideQuestionIcon()
    {
        iconTask_7.SetActive(false);
    }

    public void ShowDialogIcon()
    {
        HideAllIcons();
        iconDialog.SetActive(true);
        Invoke("HideDialogIcon", 3f);
        Invoke("ShowDialogIcon", 15f);
    }

    public void HideDialogIcon()
    {
        iconDialog.SetActive(false);
    }

    void HideAllIcons()
    {
        CancelInvoke("ShowDialogIcon");
        HideDialogIcon();
        HideExclamationPointIcon();
        HideQuestionIcon();
    }
}
