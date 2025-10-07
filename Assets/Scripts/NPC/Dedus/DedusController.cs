using UnityEngine;

public class DedusController : MonoBehaviour
{
    public GameObject iconTask_1;
    public GameObject iconTask_7;
    public GameObject iconDialog;

    void Start()
    {
        StuffSetActiveFalse();
    }
    
    void StuffSetActiveFalse()
    {
        iconTask_1.SetActive(false);
        iconTask_7.SetActive(false);
        iconDialog.SetActive(false);
    }
}
