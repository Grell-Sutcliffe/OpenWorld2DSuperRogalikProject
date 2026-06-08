using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Icon_ItemAccept_Script : MonoBehaviour
{
    Item item;

    public Image image;
    public TextMeshProUGUI amountTMP;

    public void SetItem(CollectableItem collectableItem, Item item)
    {
        this.item = item;

        image.sprite = item.sprite;
        amountTMP.text = collectableItem.amount.ToString();
    }
}
