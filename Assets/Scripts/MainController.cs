using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject Dedus;
    public GameObject GrandsonEugene;

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;
    DedusDialogScript dedusDialogScript;
    GrandsonEugeneDialogScript grandsonEugeneDialogScript;

    bool dedus_F;
    bool grandsonEugene_F;

    void Start()
    {
        StuffSetActiveFalse();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();
        dedusDialogScript = Dedus.GetComponent<DedusDialogScript>();
        grandsonEugeneDialogScript = GrandsonEugene.GetComponent<GrandsonEugeneDialogScript>();
    }

    public void PressF()
    {
        if (dedus_F)
        {
            dedusDialogScript.StartDialog();
        }
        else if (grandsonEugene_F)
        {
            grandsonEugeneDialogScript.StartDialog();
        }
    }

    public void StartDialog(string speaker_text, string speach_text)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_text);
    }

    public void StartDialog(string speaker_text, string[] speach_text)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_text);
    }

    void StuffSetActiveFalse()
    {
        dialogPanel.SetActive(false);

        dedus_F = false;
        grandsonEugene_F = false;
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
}
