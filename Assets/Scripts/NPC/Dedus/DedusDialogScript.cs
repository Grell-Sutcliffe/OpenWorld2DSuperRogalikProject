using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DedusDialogScript : MonoBehaviour
{
    public string npc_name = "�����";

    Quest current_quest;

    SpeachTree text_hello;
    SpeachTree ask_for_search_grandson;

    MainController mainController;
    QuestsController questsController;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        CreateSpeach();
    }

    public void StartDialog()
    {
        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();
        //mainController.StartDialog(npc_name, text_hello.root);
        mainController.StartDialog(npc_name, ask_for_search_grandson);
    }

    void CreateSpeach()
    {
        CreateSpeach_Hello();
        CreateSpeach_AskForSearchGrandson();
    }

    void CreateSpeach_Hello()
    {
        
    }

    void CreateSpeach_AskForSearchGrandson()
    {
        ask_for_search_grandson = new SpeachTree();
        ask_for_search_grandson.npc_name = questsController.dedus;
        ask_for_search_grandson.quest_title = questsController.quest_TheLostGrandson;

        SpeachNode root = new SpeachNode("����������, ��������������!");
        root.is_answering = true;

        SpeachNode bye_node_1 = new SpeachNode("��� ����. �� �������.");
        bye_node_1.answer_text = "��� ���� ����.";
        root.AddNextNode(bye_node_1);

        SpeachNode help_node_1 = new SpeachNode("� ���� ��������� �������� ����. �� �������� ��������?");
        help_node_1.is_answering = true;
        help_node_1.answer_text = "������, ������!";
        root.AddNextNode(help_node_1);

        SpeachNode help_node_2 = new SpeachNode("��, ������� ����, ������ ������� �������!");
        help_node_2.answer_text = "�������, ������.";
        help_node_1.AddNextNode(help_node_2);

        SpeachNode bye_node_2 = new SpeachNode("�� �������.");
        bye_node_2.answer_text = "��� �������. ����� � ������ ���?";
        help_node_1.AddNextNode(bye_node_2);

        SpeachNode help_node_3 = new SpeachNode("��� ���� ���������, ����� �� ���� ����� ��� ��� 2 ���. ����������, ����� ���.");
        help_node_3.is_answering = true;
        help_node_2.AddNextNode(help_node_3);

        SpeachNode bye_node_3 = new SpeachNode("�� � �� ���� �����, ������� ���������.");
        bye_node_3.answer_text = "��, ���. ��� ���.";
        help_node_3.AddNextNode(bye_node_3);

        SpeachNode help_node_4 = new SpeachNode("�������! �� �������.");
        help_node_4.answer_text = "����������� �����.";
        help_node_4.is_accepting_quest = true;
        help_node_3.AddNextNode(help_node_4);

        ask_for_search_grandson.root = root;
    }
}
