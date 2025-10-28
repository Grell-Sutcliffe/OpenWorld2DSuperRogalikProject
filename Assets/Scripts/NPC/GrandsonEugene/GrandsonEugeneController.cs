using UnityEngine;

public class GrandsonEugeneController : MonoBehaviour
{
    public GameObject interactIcon;

    public SpriteRenderer interactIconSR;

    Color active = new Color(0.1f, 1.0f, 0.1f, 0.5f);
    Color deactive = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    void Start()
    {
        StuffSetActiveFalse();

        interactIconSR = interactIcon.GetComponent<SpriteRenderer>();
    }

    void StuffSetActiveFalse()
    {

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
