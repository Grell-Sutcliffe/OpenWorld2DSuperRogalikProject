using UnityEngine;

public class DedusDialogScript : MonoBehaviour
{
    public string npc_name = "�����";

    string text_hello = "����������, ��������������!";

    MainController mainController;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void StartDialog()
    {
        mainController.StartDialog(npc_name, text_hello);
    }
}
