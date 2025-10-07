using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogPanelScript : MonoBehaviour
{
    MainController mainController;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speachText;

    public GameObject iconNextLine;

    public float text_speed = 0.05f;

    bool is_line_finished;
    private int index = 0;
    string[] lines;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void StartDialog(string speaker_text, string speach_text)
    {
        string[] new_speach_text = { speach_text };

        StartDialog(speaker_text, new_speach_text);
    }

    public void StartDialog(string speaker_text, string[] speach_text)
    {
        index = 0;
        lines = speach_text;

        OpenDialogPanel();
        ChangeDialogPanel(speaker_text, string.Empty);

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        iconNextLine.SetActive(false);
        is_line_finished = false;

        foreach (char c in lines[index].ToCharArray())
        {
            speachText.text += c;
            yield return new WaitForSeconds(text_speed);
        }

        is_line_finished = true;
        iconNextLine.SetActive(true);
    }

    public void NextLine()
    {
        if (!is_line_finished) return;

        if (index < lines.Length - 1)
        {
            index++;
            ChangeDialogPanel(string.Empty);
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogPanel();
        }
    }

    public void ChangeDialogPanel(string speaker_text, string speach_text)
    {
        speakerText.text = speaker_text;
        speachText.text = speach_text;
    }

    public void ChangeDialogPanel(string speach_text)
    {
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
