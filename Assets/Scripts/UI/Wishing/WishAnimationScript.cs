using UnityEngine;

public class WishAnimationScript : MonoBehaviour
{
    private Animator animator;

    WishPanelScript wishPanelScript;

    void Start()
    {
        wishPanelScript = GameObject.Find("WishPanel").GetComponent<WishPanelScript>();

        animator = GetComponent<Animator>();
    }

    public void OnWishAnimationEnded()
    {
        wishPanelScript.CompleteWish();
    }
}
