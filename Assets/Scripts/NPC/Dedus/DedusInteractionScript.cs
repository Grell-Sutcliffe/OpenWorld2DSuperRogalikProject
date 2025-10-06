using UnityEngine;

public class DedusInteractionScript : MonoBehaviour
{
    MainController mainController;

    public GameObject interactIcon;

    private void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        OffInteraction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnInteraction();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OffInteraction();
    }

    void OnInteraction()
    {
        interactIcon.SetActive(true);

        mainController.DedusOn();
    }

    void OffInteraction()
    {
        interactIcon.SetActive(false);

        mainController.DedusOff();
    }
}
