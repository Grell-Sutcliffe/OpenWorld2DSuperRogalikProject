using UnityEngine;
using UnityEngine.SceneManagement;

public class DoggyInteractionScript : MonoBehaviour
{
    MainController mainController;

    public GameObject interactIcon;

    public SpriteRenderer interactIconSR;

    Color active = new Color(0.1f, 1.0f, 0.1f, 0.5f);
    Color deactive = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    private void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        OffInteraction();

        interactIconSR = interactIcon.GetComponent<SpriteRenderer>();
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

        mainController.DoggyOn();
    }

    void OffInteraction()
    {
        interactIcon.SetActive(false);

        mainController.DoggyOff();
    }

    public void InteractIconActivate()
    {
        interactIconSR.color = active;
    }

    public void InteractIconDeactivate()
    {
        interactIconSR.color = deactive;
    }
}
