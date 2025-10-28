using UnityEngine;

public class WishPanelScript : MonoBehaviour
{
    public GameObject starsGO;
    public GameObject wishMadePanel;
    public GameObject wishInteractPanel;

    private Animator animator;

    [SerializeField]
    float chance_5_star = 0.05f;
    [SerializeField]
    float chance_4_star = 0.2f;

    void Start()
    {
        animator = starsGO.GetComponent<Animator>();
    }

    public void StartWish()
    {
        CloseWishInteractPanel();

        animator.SetBool("is_wishing", true);

        Invoke("StopWish", 0.3f);
    }

    void StopWish()
    {
        animator.SetBool("is_wishing", false);
    }

    public void CompleteWish()
    {
        Debug.Log("WISH MADE");

        OpenWishInteractPanel();
        OpenWishMade();
    }

    void OpenWishInteractPanel()
    {
        wishInteractPanel.SetActive(true);
    }

    void CloseWishInteractPanel()
    {
        wishInteractPanel.SetActive(false);
    }

    void OpenWishMade()
    {
        wishMadePanel.SetActive(true);
    }

    public void CloseWishMade()
    {
        wishMadePanel.SetActive(false);
    }
}
