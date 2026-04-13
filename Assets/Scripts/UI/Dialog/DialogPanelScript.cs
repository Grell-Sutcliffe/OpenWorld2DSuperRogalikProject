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

    Dialog current_dialog;

    SpeachNode current_speachNode;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        if (answerPanel == null) answerPanel = GameObject.Find("AnswerPanel");
        answer_panel_rect_transform = answerPanel.GetComponent<RectTransform>();
    }

    public void StartDialog(Dialog new_dialog)
    {
        current_dialog = new_dialog;
        //current_speachNode = new_dialog.current_speachNode;
        current_speachNode = new SpeachNode().NewSpeachNode(new_dialog.current_speachNodeSO);   // THIS !!!

        if (current_speachNode == null) return;

        OpenDialogPanel();
        //ChangeDialogPanel(current_speachNode.speaker_name);

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
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
        //Debug.Log($"Dialog  :  TypeLine -- dialog = {current_dialog.title}, speach_node = {current_speachNode.speach}");

        ChangeDialogPanel(current_speachNode.npcSO.npc_name);
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
        // Debug.Log($"DialogPanel   :   line finished");

        if (current_speachNode == null)
        {
            CloseDialogPanel();
            return;
        }

        if (current_speachNode.is_finishing)
        {
            current_dialog.is_finished = true;
        }

        iconNextLine.SetActive(true);

        if (current_speachNode is AnswerableSpeachNode _)
        {
            answerPanel.SetActive(true);
            ChangeAnswerPanelHeight();
            return;
        }
        
        if (current_speachNode is QuestAcceptingSpeachNode questAcceptingSpeachNode)
        {
            // Debug.Log($"DialogPanel   :   questAcceptingSpeachNode  ---  quest.title = {questAcceptingSpeachNode.quest_title}");
            EventBus.Raise(new QuestAcceptedEvent(questAcceptingSpeachNode.quest_title));
        }

        is_line_finished = true;
    }

    private void OnEnable()
    {
        EventBus.OnEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.OnEvent -= HandleEvent;
    }

    private void HandleEvent(IEvent e)
    {
        if (e is ItemDeliveredEvent itemDeliveredEvent)
        {
            if (itemDeliveredEvent.is_success)
            {
                NextLine(false);
            }
            else
            {
                CloseDialogPanel();
            }
        }
    }

    public void SelectAnswer(SpeachNode new_next_node)
    {
        is_line_finished = true;
        current_speachNode = new_next_node;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(TypeLine());
    }

    public void NextLine()
    {
        NextLine(true);
    }

    public void NextLine(bool need_nextNode)
    {
        if (current_speachNode == null)
        {
            CloseDialogPanel();
            return;
        }
        // Debug.Log($"DialogPanel   :   NextLine   ---   {current_speachNode.speach}");

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

        if (current_speachNode is ItemDeliverySpeachNode itemDeliverySpeachNode)
        {
            mainController.OpenItemDeliveryPanel(itemDeliverySpeachNode.list_of_CollectableItems);
            //current_speachNode = itemDeliverySpeachNode.next_speachNode;
            current_speachNode = new SpeachNode().NewSpeachNode(itemDeliverySpeachNode.next_speachNodeSO);   // THIS !!!
            return;
        }

        if (need_nextNode && current_speachNode is DefaultSpeachNode current_default_speachNode)
        {
            //current_speachNode = current_default_speachNode.n(ext_speachNode;
            current_speachNode = new SpeachNode().NewSpeachNode(current_default_speachNode.next_speachNodeSO);
        }

        if (current_speachNode != null)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogPanel();
            // FinishDialog();
            return;
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

    /*
    public void FinishDialog()
    {
        if (current_dialog.is_finished)
        {
            current_dialog.is_finished = true;
            EventBus.Raise(new DialogueFinishedEvent(current_dialog.title));
        }

        CloseDialogPanel();
    }
    */

    public void CloseDialogPanel()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if (current_dialog.is_finished)
        {
            Debug.Log($"EventBus  ---  dialog finished = {current_dialog.title}");
            EventBus.Raise(new DialogFinishedEvent(current_dialog.title));
        }

        gameObject.SetActive(false);

        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();

        mainController.TurnOnKeyboard();
        mainController.ShowPlayerPanel();

        //questsController.ShowNewTask();
    }
}
