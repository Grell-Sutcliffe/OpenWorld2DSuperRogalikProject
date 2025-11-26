using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class GrandsonEugeneDialogScript : MonoBehaviour
{
    public string npc_name = "Юджин";

    Quest current_quest;

    public SpeachTree text_hello;
    public SpeachTree TheLostGrandson_ask_for_help_1;

    MainController mainController;
    QuestsController questsController;

    GrandsonEugineQuestScript grandsonEugineQuestScript;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        grandsonEugineQuestScript = gameObject.GetComponent<GrandsonEugineQuestScript>();

        CreateSpeach();
    }

    public void StartDialog()
    {
        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();
        if (grandsonEugineQuestScript == null) grandsonEugineQuestScript = gameObject.GetComponent<GrandsonEugineQuestScript>();
        //mainController.StartDialog(npc_name, text_hello.root);

        //mainController.StartDialog(npc_name, TheLostGrandson_ask_for_search_grandson_1);
        mainController.StartDialog(npc_name, grandsonEugineQuestScript.GetCurrentSpeachTree());
    }

    void CreateSpeach()
    {
        CreateSpeach_Hello();
        CreateSpeach_TheLostGrandson_ask_for_help_1();
    }

    void CreateSpeach_Hello()
    {
        text_hello = new SpeachTree();
        text_hello.npc_name = questsController.grandsonEugene;
        text_hello.quest_title = questsController.none_quest_name;

        SpeachNode root = new SpeachNode("Привет, путник!");
        root.is_answering = true;

        SpeachNode bye_node_1 = new SpeachNode("Ещё увидимся!");
        bye_node_1.answer_text = "Мне пора идти.";
        root.AddNextNode(bye_node_1);

        SpeachNode help_node_1 = new SpeachNode("Вообще-то я не маленький, мне уже 10!");
        help_node_1.answer_text = "Привет, малыш!";
        root.AddNextNode(help_node_1);

        text_hello.root = root;
    }

    void CreateSpeach_TheLostGrandson_ask_for_help_1()
    {
        TheLostGrandson_ask_for_help_1 = new SpeachTree();
        TheLostGrandson_ask_for_help_1.npc_name = questsController.grandsonEugene;
        TheLostGrandson_ask_for_help_1.quest_title = questsController.quest_TheLostGrandson;

        SpeachNode root = new SpeachNode("Дедушка запрещает мне разговаривать с незнакомцами..."); // -------------------------------+
        root.is_answering = true; //                                                                                                 |
        //                                                                                                                           |
        SpeachNode bye_node_1 = new SpeachNode("Да, а как вы узнали моё имя?"); // -------------------------------------+            |
        bye_node_1.answer_text = "Ты Юджин?"; // <----------------------------------------------------------------------|------------+
        bye_node_1.is_answering = true; //                                                                              |            |
        root.AddNextNode(bye_node_1); //                                                                                |            |
        //                                                                                                              |            |
        SpeachNode help_node_1 = new SpeachNode("Вообще-то я не маленький, мне уже 10!"); //                            |            |
        help_node_1.answer_text = "Ну и стой тут, раз такой важный."; // <----------------------------------------------|------------+ end (no help)
        root.AddNextNode(help_node_1); //                                                                               |
        //                                                                                                              |
        SpeachNode new_node_2 = new SpeachNode("Так вы пришли мне помочь! Ура!"); // -----------------------------------|------------+
        new_node_2.answer_text = "Твой дедушка попросил меня помочь тебя найти."; // <----------------------------------+            |
        new_node_2.is_answering = true; //                                                                                           |
        bye_node_1.AddNextNode(new_node_2); //                                                                                       |
        //                                                                                                                           |
        SpeachNode new_node_3 = new SpeachNode("Пойдём!"); //                                                                        |
        new_node_3.answer_text = "Иди за мной, я покажу дорогу."; // <---------------------------------------------------------------+ end (end of dialog)
        new_node_3.is_ending = true;
        new_node_2.AddNextNode(new_node_3);

        TheLostGrandson_ask_for_help_1.root = root;
    }
}
