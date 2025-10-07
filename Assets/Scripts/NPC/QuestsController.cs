using UnityEngine;

public class QuestsController : MonoBehaviour
{
    MainController mainController;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }
}
