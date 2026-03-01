using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Icon_ItemDelivery_Script : MonoBehaviour
{
    BackPackController backpackController;

    public Image image;
    public TextMeshProUGUI amountTMP;

    private void Awake()
    {
        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    public void SetItem(CollectableItem collectableItem)
    {
        if (backpackController == null) backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();

        image.sprite = backpackController.GetItemByName(collectableItem.item_name).sprite;
        amountTMP.text = collectableItem.amount.ToString();
    }
}
