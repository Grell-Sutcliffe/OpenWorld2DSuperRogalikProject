using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static DialogPanelScript;
using static QuestsController;

public class DedusDialogScript : MonoBehaviour
{
    public string npc_name = "Дедус";

    Quest current_quest;

    public SpeachTree text_hello;
    public SpeachTree TheLostGrandson_ask_for_search_grandson_1;
    public SpeachTree TheLostGrandson_ask_for_search_grandson_2;

    MainController mainController;
    QuestsController questsController;

    DedusQuestScript dedusQuestScript;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        dedusQuestScript = gameObject.GetComponent<DedusQuestScript>();

        CreateSpeach();
    }

    public void StartDialog()
    {
        if (mainController == null) mainController = GameObject.Find("MainController").GetComponent<MainController>();
        if (dedusQuestScript == null) dedusQuestScript = gameObject.GetComponent<DedusQuestScript>();
        //mainController.StartDialog(npc_name, text_hello.root);

        //mainController.StartDialog(npc_name, TheLostGrandson_ask_for_search_grandson_1);
        mainController.StartDialog(npc_name, dedusQuestScript.GetCurrentSpeachTree());
    }

    void CreateSpeach()
    {
        CreateSpeach_Hello();
        CreateSpeach_AskForSearchGrandson_1();
        CreateSpeach_AskForSearchGrandson_2();
    }

    void CreateSpeach_Hello()
    {
        text_hello = new SpeachTree();
        text_hello.npc_name = questsController.dedus;
        text_hello.quest_title = questsController.none_quest_name;

        SpeachNode root = new SpeachNode("Здравствуй, путешественник!");
        root.is_answering = true;

        SpeachNode bye_node_1 = new SpeachNode("Как жаль. До встречи.");
        bye_node_1.answer_text = "Мне пора идти.";
        root.AddNextNode(bye_node_1);

        SpeachNode help_node_1 = new SpeachNode("Хорошего дня!");
        help_node_1.answer_text = "Привет, дедуль!";
        root.AddNextNode(help_node_1);

        text_hello.root = root;
    }

    void CreateSpeach_AskForSearchGrandson_1()
    {
        TheLostGrandson_ask_for_search_grandson_1 = new SpeachTree();
        TheLostGrandson_ask_for_search_grandson_1.npc_name = questsController.dedus;
        TheLostGrandson_ask_for_search_grandson_1.quest_title = questsController.quest_TheLostGrandson;

        SpeachNode root = new SpeachNode("Здравствуй, путешественник!");
        root.is_answering = true;

        SpeachNode bye_node_1 = new SpeachNode("Как жаль. До встречи.");
        bye_node_1.answer_text = "Мне пора идти.";
        root.AddNextNode(bye_node_1);

        SpeachNode help_node_1 = new SpeachNode("У меня случилась страшная беда. Не поможешь старичку?");
        help_node_1.is_answering = true;
        help_node_1.answer_text = "Привет, дедуль!";
        root.AddNextNode(help_node_1);

        SpeachNode help_node_2 = new SpeachNode("Ой, спасибо тебе, добрый молодой человек!");
        help_node_2.answer_text = "Конечно, помогу.";
        help_node_1.AddNextNode(help_node_2);

        SpeachNode bye_node_2 = new SpeachNode("До встречи.");
        bye_node_2.answer_text = "Мне некогда. Давай в другой раз?";
        help_node_1.AddNextNode(bye_node_2);

        SpeachNode help_node_3 = new SpeachNode("Мой внук потерялся, нигде не могу найти его уже 2 дня. Пожалуйста, найди его.");
        help_node_3.is_answering = true;
        help_node_2.AddNextNode(help_node_3);

        SpeachNode bye_node_3 = new SpeachNode("Ну и ну тебя нафиг, паразит маленький.");
        bye_node_3.answer_text = "Ой, нет. Сам ищи.";
        help_node_3.AddNextNode(bye_node_3);

        SpeachNode help_node_4 = new SpeachNode("Спасибо! До встречи.");
        help_node_4.answer_text = "Обязательно найду.";
        help_node_4.is_accepting_quest = true;
        help_node_3.AddNextNode(help_node_4);

        TheLostGrandson_ask_for_search_grandson_1.root = root;
    }

    void CreateSpeach_AskForSearchGrandson_2()
    {
        TheLostGrandson_ask_for_search_grandson_2 = new SpeachTree();
        TheLostGrandson_ask_for_search_grandson_2.npc_name = questsController.dedus;
        TheLostGrandson_ask_for_search_grandson_2.quest_title = questsController.quest_TheLostGrandson;

        SpeachNode root = new SpeachNode("Очень переживаю за внука."); // -----------------------------------------------------+
        root.is_answering = true; //                                                                                           |
        //                                                                                                                     |
        SpeachNode answer_node_1 = new SpeachNode("Попытаюсь вспомнить..."); // -----------------------------------------------|-----+
        answer_node_1.answer_text = "Где ты видел его в последний раз?"; // <--------------------------------------------------|     |
        root.AddNextNode(answer_node_1); //                                                                                    |     |
        //                                                                                                                     |     |
        SpeachNode answer_node_2 = new SpeachNode("Хммм... Дай-ка подумать..."); // -----------------------------------+       |     |
        answer_node_2.answer_text = "Не знаешь, куда он мог бы пойти?"; // <-------------------------------------------|-------+     |
        root.AddNextNode(answer_node_2); //                                                                            |             |     
        //                                                                                                             |             |
        SpeachNode answer_node_11 = new SpeachNode("Кажется, он отправился гулять с Джеком."); // <--------------------|-------------+
        answer_node_11.is_answering = true; // ------------------------------------------------------------------------|-------+
        answer_node_1.AddNextNode(answer_node_11); //                                                                  |       |
        //                                                                                                             |       |
        SpeachNode answer_node_21 = new SpeachNode("Обычно они с Джеком ходят гулять на окраину деревни."); // <-------+       |     
        answer_node_21.is_answering = true; // --------------------------------------------------------------------------------+
        answer_node_2.AddNextNode(answer_node_21); //                                                                          |
        //                                                                                                                     |
        SpeachNode answer_node_11_21 = new SpeachNode("Это наш пёс. Он очень дружелюбный и умный."); //                        |
        answer_node_11_21.answer_text = "Кто такой Джек?"; // <----------------------------------------------------------------+
        root.AddNextNode(answer_node_11_21); //                                                                            

        TheLostGrandson_ask_for_search_grandson_2.repeatable = true;
        TheLostGrandson_ask_for_search_grandson_2.root = root;
    }
}
