using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotScript : MonoBehaviour
{
    BackPackController backpackController;

    public Image image;
    public TextMeshProUGUI textTMP;

    public void SetSlot(Item item, int amount)
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        image.sprite = item.sprite;
        textTMP.text = item.amount + "/" + amount.ToString();

        if (item.amount < amount)
        {
            textTMP.color = Color.red;
        }
    }
}
