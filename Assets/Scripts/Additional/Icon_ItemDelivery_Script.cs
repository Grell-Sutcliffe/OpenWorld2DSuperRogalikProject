using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Icon_ItemDelivery_Script : MonoBehaviour
{
    Item item;

    public Image image;
    public TextMeshProUGUI amountTMP;

    public void SetItem(CollectableItem collectableItem, Item item)
    {
        this.item = item;

        image.sprite = item.sprite;
        amountTMP.text = item.amount.ToString() + "/" + collectableItem.amount.ToString();

        if (item.amount < collectableItem.amount)
        {
            amountTMP.color = Color.red;
        }
    }
}
