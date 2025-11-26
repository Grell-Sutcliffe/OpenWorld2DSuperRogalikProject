using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DoggyDialogScript : MonoBehaviour
{
    public string npc_name = "Джек";

    Quest current_quest;

    public SpeachTree text_hello;

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

        //mainController.StartDialog(npc_name, TheLostGrandson_ask_for_search_grandson_1);
        mainController.StartDialog(npc_name, text_hello);
    }

    void CreateSpeach()
    {
        CreateSpeach_Hello();
    }

    void CreateSpeach_Hello()
    {
        text_hello = new SpeachTree();
        text_hello.npc_name = questsController.doggy;
        text_hello.quest_title = questsController.none_quest_name;

        SpeachNode root = new SpeachNode("Гав-гав!");
        root.is_answering = true;

        SpeachNode bye_node_1 = new SpeachNode("*виляет хвостиком*");
        bye_node_1.is_text_action = true;
        bye_node_1.answer_text = "Хороший пёсик!";
        root.AddNextNode(bye_node_1);

        SpeachNode help_node_1 = new SpeachNode("Гр-р-р-р...");
        help_node_1.answer_text = "Фу!";
        root.AddNextNode(help_node_1);

        text_hello.root = root;
    }
}
