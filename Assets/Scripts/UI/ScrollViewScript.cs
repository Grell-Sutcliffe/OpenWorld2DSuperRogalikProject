using UnityEngine;
using UnityEngine.UI;

public class ScrollViewScript : MonoBehaviour
{
    private ScrollRect scrollRect;

    private void OnEnable()
    {
        UpdateScrollView();
    }

    public void UpdateScrollView()
    {
        scrollRect = GetComponent<ScrollRect>();

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
