using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject Dedus;

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;
    DedusDialogScript dedusDialogScript;

    bool dedus_F;

    void Start()
    {
        StuffSetActiveFalse();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();
        dedusDialogScript = Dedus.GetComponent<DedusDialogScript>();
    }

    public void PressF()
    {
        if (dedus_F)
        {
            dedusDialogScript.StartDialog();
        }
    }

    public void StartDialog(string speaker_text, string speach_text)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_text);
    }

    void StuffSetActiveFalse()
    {
        dialogPanel.SetActive(false);

        dedus_F = false;
    }

    public void DedusOn()
    {
        dedus_F = true;
    }

    public void DedusOff()
    {
        dedus_F = false;
    }
}
