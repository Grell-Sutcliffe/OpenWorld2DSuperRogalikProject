using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogPanelScript : MonoBehaviour
{
    MainController mainController;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speachText;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void StartDialog(string speaker_text, string speach_text)
    {
        OpenDialogPanel();
        ChangeDialogPanel(speaker_text, speach_text);
    }

    public void ChangeDialogPanel(string speaker_text, string speach_text)
    {
        speakerText.text = speaker_text;
        speachText.text = speach_text;
    }

    public void OpenDialogPanel()
    {
        gameObject.SetActive(true);
    }

    public void CloseDialogPanel()
    {
        gameObject.SetActive(false);
    }
}
