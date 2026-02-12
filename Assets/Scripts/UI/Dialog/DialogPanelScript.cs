using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using static QuestsController;
using static DialogController;

public class DialogPanelScript : MonoBehaviour
{
    MainController mainController;
    QuestsController questsController;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speachText;

    public GameObject answerPanel;
    public GameObject iconNextLine;

    public GameObject answerOptionPrefab;

    RectTransform answer_panel_rect_transform;

    Coroutine coroutine;

    public float text_speed = 0.05f;

    public int answer_height = 125;
    public int space_between_answers = 25;

    bool is_line_finished;

    SpeachNode current_speachNode;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        if (answerPanel == null) answerPanel = GameObject.Find("AnswerPanel");
        answer_panel_rect_transform = answerPanel.GetComponent<RectTransform>();
    }

    public void StartDialog(Dialog new_speachNode)
    {
        current_speachNode = new_speachNode.current_speachNode;
        if (current_speachNode == null) return;

        OpenDialogPanel();
        //ChangeDialogPanel(current_speachNode.speaker_name);

        coroutine = StartCoroutine(TypeLine());
    }

    void ChangeSpeachTextAlignment()
    {
        if (current_speachNode.speach_type == SpeachType.Speach)
        {
            speachText.alignment = TextAlignmentOptions.TopLeft;
            speachText.fontStyle = FontStyles.Normal;
        }
        if (current_speachNode.speach_type == SpeachType.Action)
        {
            speachText.alignment = TextAlignmentOptions.Top;
            speachText.fontStyle = FontStyles.Italic;
        }
    }

    IEnumerator TypeLine()
    {
        ChangeDialogPanel(current_speachNode.speaker_name);
        ChangeSpeachTextAlignment();

        ClearAndCloseAnswerPanel();
        iconNextLine.SetActive(false);

        is_line_finished = false;

        foreach (char c in current_speachNode.speach.ToCharArray())
        {
            speachText.text += c;
            yield return new WaitForSeconds(text_speed);
        }

        FinishLine();
    }

    void PrintAllLine()
    {
        ClearAndCloseAnswerPanel();
        iconNextLine.SetActive(false);
        is_line_finished = false;

        speachText.text = current_speachNode.speach;

        FinishLine();
    }

    void FinishLine()
    {
        iconNextLine.SetActive(true);

        if (current_speachNode is AnswerableSpeachNode _)
        {
            answerPanel.SetActive(true);
            ChangeAnswerPanelHeight();
        }
        else
        {
            is_line_finished = true;

            if (current_speachNode is DefaultSpeachNode current_default_speachNode)
            {
                current_speachNode = current_default_speachNode.next_speachNode;
            }
        }
    }

    public void SelectAnswer(SpeachNode new_next_node)
    {
        is_line_finished = true;
        current_speachNode = new_next_node;
        coroutine = StartCoroutine(TypeLine());
    }

    public void NextLine()
    {
        if (!is_line_finished)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            PrintAllLine();
            return;
        }

        if (current_speachNode != null)
        {
            coroutine = StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogPanel();
        }
    }

    void ClearAndCloseAnswerPanel()
    {
        ClearAnswerPanel();
        answerPanel.SetActive(false);
    }

    void ClearAnswerPanel()
    {
        foreach (Transform child in answerPanel.transform)
        {
            Destroy(child.gameObject);
        }

        if (answerPanel == null) answerPanel = GameObject.Find("AnswerPanel");
        if (answer_panel_rect_transform == null) answer_panel_rect_transform = answerPanel.GetComponent<RectTransform>();

        answer_panel_rect_transform.sizeDelta = new Vector2(answer_panel_rect_transform.sizeDelta.x, 0);
    }

    void ChangeAnswerPanelHeight()
    {
        AnswerableSpeachNode answerableSpeachNode = null;
        if (current_speachNode is AnswerableSpeachNode temp_speachNode)
        {
            answerableSpeachNode = temp_speachNode;
        }
        if (answerableSpeachNode == null) return;

        int amount_of_answers = answerableSpeachNode.answers.Count;
        int new_height = amount_of_answers * answer_height + (amount_of_answers + 1) * space_between_answers;
        answer_panel_rect_transform.sizeDelta = new Vector2(answer_panel_rect_transform.sizeDelta.x, new_height);

        foreach (Answer answer in answerableSpeachNode.answers)
        {
            SpawnAnswerOptionPrefab(answer);
        }
    }

    void SpawnAnswerOptionPrefab(Answer answer)
    {
        GameObject new_prefab = Instantiate(answerOptionPrefab, answerPanel.transform);
        AnswerOptionScript new_prefab_script = new_prefab.GetComponent<AnswerOptionScript>();

        new_prefab_script.MakeAnswerOption(answer.answer_text, answer.next_speachNode);
    }

    public void ChangeDialogPanel(string speaker_text, string speach_text = "")
    {
        speakerText.text = speaker_text;
        speachText.text = speach_text;
    }

    public void OpenDialogPanel()
    {
        gameObject.SetActive(true);

        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();

        mainController.TurnOffKeyboard();
        mainController.HidePlayerPanel();
    }

    public void CloseDialogPanel()
    {
        gameObject.SetActive(false);
        mainController.TurnOnKeyboard();

        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();

        mainController.ShowPlayerPanel();
    }
}
