using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DoggyDialogScript : MonoBehaviour
{
    public string npc_name = "Джек";

    Quest current_quest;

    public SpeachTree text_hello;
    public SpeachTree TheLostGrandson_ask_for_help_1;

    MainController mainController;
    QuestsController questsController;

    DoggyQuestScript doggyQuestScript;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        doggyQuestScript = gameObject.GetComponent<DoggyQuestScript>();

        CreateSpeach();
    }

    public void StartDialog()
    {
        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();
        if (doggyQuestScript == null) doggyQuestScript = gameObject.GetComponent<DoggyQuestScript>();

        //mainController.StartDialog(npc_name, text_hello);
        mainController.StartDialog(npc_name, doggyQuestScript.GetCurrentSpeachTree());
    }

    void CreateSpeach()
    {
        CreateSpeach_Hello();
        Create_Speach_TheLostGrandson_ask_for_help_1();
    }

    void CreateSpeach_Hello()
    {
        text_hello = new SpeachTree();
        //text_hello.npc_name = questsController.doggy;
        text_hello.npc_name = npc_name;
        text_hello.quest_title = questsController.none_quest_name;

        SpeachNode root = new SpeachNode(npc_name, "Гав-гав!");
        root.is_answering = true;

        SpeachNode bye_node_1 = new SpeachNode(npc_name, "*виляет хвостиком*");
        bye_node_1.is_text_action = true;
        bye_node_1.answer_text = "Хороший пёсик!";
        root.AddNextNode(bye_node_1);

        SpeachNode help_node_1 = new SpeachNode(npc_name, "Гр-р-р-р...");
        help_node_1.answer_text = "Фу!";
        root.AddNextNode(help_node_1);

        text_hello.root = root;
    }

    void Create_Speach_TheLostGrandson_ask_for_help_1()
    {
        TheLostGrandson_ask_for_help_1 = new SpeachTree();
        //TheLostGrandson_ask_for_help_1.npc_name = questsController.doggy;
        TheLostGrandson_ask_for_help_1.npc_name = npc_name;
        TheLostGrandson_ask_for_help_1.quest_title = questsController.quest_TheLostGrandson;

        SpeachNode root = new SpeachNode(npc_name, "Гав-гав!");
        root.is_answering = true;

        SpeachNode hi_node_1 = new SpeachNode(npc_name, "*радостно виляет хвостиком*");
        hi_node_1.is_text_action = true;
        hi_node_1.answer_text = "Джек?";
        hi_node_1.is_finishing_task = true;
        root.AddNextNode(hi_node_1);

        SpeachNode hi_node_2 = new SpeachNode(npc_name, "*грустно скулит*");
        hi_node_2.is_text_action = true;
        hi_node_2.answer_text = "*пройти мимо*";
        root.AddNextNode(hi_node_2);

        TheLostGrandson_ask_for_help_1.root = root;
    }
}
