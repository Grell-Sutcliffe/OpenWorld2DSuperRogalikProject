using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using static QuestsController;

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

    public int no_answer_key_zero = 0;

    Coroutine coroutine;

    public class SpeachNode
    {
        public string current_npc_name;
        public string current_text;
        public SpeachNode prev_node;
        public bool is_answering;
        public bool is_accepting_quest;
        public bool is_finishing_task;
        public Dictionary<int, SpeachNode> next_node;
        public string answer_text;
        public bool is_ending;
        public bool is_text_action;

        public SpeachNode()
        {
            current_npc_name = string.Empty;
            current_text = string.Empty;
            prev_node = null;
            next_node = null;
            is_answering = false;
            is_accepting_quest = false;
            is_finishing_task = false;
            is_ending = false;
            is_text_action = false;
        }

        public SpeachNode(string npc_name_, string text_)
        {
            current_npc_name = npc_name_;
            current_text = text_;
            prev_node = null;
            next_node = null;
            is_answering = false;
            is_accepting_quest = false;
            is_finishing_task = false;
            is_ending = false;
            is_text_action = false;
        }

        public void AddNextNode(SpeachNode new_node)
        {
            if (next_node == null) next_node = new Dictionary<int, SpeachNode>();

            if (is_answering)
            {
                for (int i = 1; i < 100; i++)
                {
                    if (!next_node.ContainsKey(i))
                    {
                        next_node[i] = new_node;
                        break;
                    }
                }
            }
            else
            {
                next_node[0] = new_node;
            }
        }
    }

    public class SpeachTree
    {
        public SpeachNode root;
        public string npc_name;
        public string quest_title;
        public bool is_finished;

        public SpeachTree()
        {
            root = new SpeachNode();
            quest_title = string.Empty;
            is_finished = false;
        }

        public SpeachTree(SpeachNode root_)
        {
            root = root_;
            quest_title = string.Empty;
            is_finished = false;
        }

        public SpeachTree(string npc_name_, string text_)
        {
            root = new SpeachNode(npc_name_, text_);
            quest_title = string.Empty;
            is_finished = false;
        }
    }

    public float text_speed = 0.05f;

    public int answer_height = 125;
    public int space_between_answers = 25;

    bool is_line_finished;
    bool is_quest_accepted;

    SpeachTree speach_tree;
    SpeachNode current_node;
    SpeachNode next_node;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        if (answerPanel == null) answerPanel = GameObject.Find("AnswerPanel");
        answer_panel_rect_transform = answerPanel.GetComponent<RectTransform>();
    }

    public void StartDialog(string speaker_text, SpeachTree new_speach_tree)
    {
        speach_tree = new_speach_tree;
        if (speach_tree == null) return;
        current_node = speach_tree.root;

        OpenDialogPanel();
        ChangeDialogPanel(speaker_text, string.Empty);

        coroutine = StartCoroutine(TypeLine());
    }

    public void StartDialog(string speaker_text, SpeachNode speach_node)
    {
        is_quest_accepted = false;
        current_node = speach_node;

        OpenDialogPanel();
        ChangeDialogPanel(speaker_text, string.Empty);

        coroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        ChangeDialogPanel(current_node.current_npc_name, string.Empty);

        speachText.alignment = TextAlignmentOptions.TopLeft;
        if (current_node.is_text_action)
        {
            speachText.alignment = TextAlignmentOptions.Top;
        }

        if (current_node.is_ending)
        {
            speach_tree.is_finished = true;

            int temp_task_index = questsController.dict_quest_name_to_quest[speach_tree.quest_title].current_task_index;
            questsController.dict_quest_name_to_quest[speach_tree.quest_title].tasks[temp_task_index].current_speach_tree_index++;
        }

        if (current_node.is_finishing_task)
        {
            questsController.FinishTask(speach_tree.quest_title);
        }

        if (current_node.is_accepting_quest)
        {
            AcceptQuest();
        }

        ClearAnswerPanel();
        answerPanel.SetActive(false);
        iconNextLine.SetActive(false);
        is_line_finished = false;

        foreach (char c in current_node.current_text.ToCharArray())
        {
            speachText.text += c;
            yield return new WaitForSeconds(text_speed);
        }

        iconNextLine.SetActive(true);

        if (current_node.is_answering)
        {
            answerPanel.SetActive(true);
            ChangeAnswerPanelHeight();
        }
        else
        {
            if (current_node.next_node != null)
            {
                next_node = current_node.next_node[no_answer_key_zero];
            }
            else
            {
                next_node = null;
            }
            is_line_finished = true;
        }
    }

    void PrintAllLine()
    {
        ClearAnswerPanel();
        answerPanel.SetActive(false);
        iconNextLine.SetActive(false);
        is_line_finished = false;

        speachText.text = current_node.current_text;

        iconNextLine.SetActive(true);

        if (current_node.is_answering)
        {
            answerPanel.SetActive(true);
            ChangeAnswerPanelHeight();
        }
        else
        {
            if (current_node.next_node != null)
            {
                next_node = current_node.next_node[no_answer_key_zero];
            }
            else
            {
                next_node = null;
            }
            is_line_finished = true;
        }
    }

    void AcceptQuest()
    {
        if (is_quest_accepted) return;
        if (speach_tree == null) return;

        if (questsController == null) questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();
        //string temp_npc = speach_tree.npc_name;

        string temp_quest = speach_tree.quest_title;
        is_quest_accepted = true;

        questsController.AcceptQuest(temp_quest);
    }

    public void SelectAnswer(SpeachNode new_next_node)
    {
        is_line_finished = true;
        next_node = new_next_node;
        NextLine();
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

        if (current_node.next_node != null)
        {
            current_node = next_node;
            next_node = null;
            ChangeDialogPanel(string.Empty);
            coroutine = StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogPanel();

            if (is_quest_accepted)
            {
                questsController.ShowNewTask(speach_tree.quest_title);
            }

            current_node = null;
            next_node = null;
            speach_tree = null;
        }
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
        int amount_of_answers = current_node.next_node.Keys.Count;
        int new_height = amount_of_answers * answer_height + (amount_of_answers + 1) * space_between_answers;
        answer_panel_rect_transform.sizeDelta = new Vector2(answer_panel_rect_transform.sizeDelta.x, new_height);

        for (int i = 1; i <= amount_of_answers; i++)
        {
            SpawnAnswerOptionPrefab(i);
        }
    }

    void SpawnAnswerOptionPrefab(int index)
    {
        GameObject new_prefab = Instantiate(answerOptionPrefab, answerPanel.transform);
        AnswerOptionScript new_prefab_script = new_prefab.GetComponent<AnswerOptionScript>();
        //new_prefab_script.next_node = current_node.next_node[index];
        new_prefab_script.MakeAnswerOption(current_node.next_node[index]);
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

        is_quest_accepted = false;

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

        questsController.UpdateNPC();
    }
}
