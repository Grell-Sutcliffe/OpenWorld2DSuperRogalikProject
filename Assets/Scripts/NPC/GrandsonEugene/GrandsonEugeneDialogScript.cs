using UnityEngine;

public class GrandsonEugeneDialogScript : MonoBehaviour
{
    public string npc_name = "Юджин";

    string text_hello = "Здравствуй, путешественник!";

    MainController mainController;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void StartDialog()
    {
        //mainController.StartDialog(npc_name, text_hello);
    }
}
