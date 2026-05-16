using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostIconScript : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI costTMP;

    void Start()
    {
        
    }

    public void SetCostIcon(Sprite sprite, string text)
    {
        image.sprite = sprite;
        costTMP.text = text;
    }
}
