using TMPro;
using UnityEngine;

public class TaskShowerScript : MonoBehaviour
{
    MainController mainController;
    QuestsController questsController;

    public TextMeshProUGUI taskShowerText;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        HideTaskShower();
    }

    public void ShowNewTask(string task_name)
    {
        Invoke("HideTaskShower", 1f);
        taskShowerText.text = task_name;

        Invoke("HideTaskShower", 10f);
    }

    public void ShowTaskShower()
    {
        gameObject.SetActive(true);
    }

    public void HideTaskShower()
    {
        gameObject.SetActive(false);
    }
}
