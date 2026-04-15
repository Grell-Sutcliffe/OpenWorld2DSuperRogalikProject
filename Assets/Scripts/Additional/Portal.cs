using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        MainController.Instance.ShowLoadingPanel();

        MainController.Instance.GodFather.SetActive(true);
        GameObject grandParent = transform.parent.parent.gameObject;
        Destroy(grandParent);
    }
}
