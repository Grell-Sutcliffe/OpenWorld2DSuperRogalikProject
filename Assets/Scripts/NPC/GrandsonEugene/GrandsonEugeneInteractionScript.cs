using UnityEngine;

public class GrandsonEugeneInteractionScript : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            OffInteraction();
        }
    }

    void OnInteraction()
    {
        interactIcon.SetActive(true);

        mainController.GrandsonEugeneOn();
    }

    void OffInteraction()
    {
        interactIcon.SetActive(false);

        mainController.GrandsonEugeneOff();
    }
}
