using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemScript : MonoBehaviour
{
    ShopPanelScript shopPanelScript;
    BackPackController backpackController;

    public int id;
    public Image itemImage;
    public TextMeshProUGUI itemNameTMP;
    public TextMeshProUGUI itemCostTMP;
    public Image itemCurrencyImage;

    void Awake()
    {
        shopPanelScript = GameObject.Find("ShopPanel").GetComponent<ShopPanelScript>();
        //backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    public void SetShopItem(int new_id)
    {
        if (shopPanelScript == null) shopPanelScript = GameObject.Find("ShopPanel").GetComponent<ShopPanelScript>();

        id = new_id;

        ItemForSale this_item = null;
        if (shopPanelScript.GetItemById(id) is ItemForSale temp_item)
        {
            this_item = temp_item;
        }
        if (this_item == null) return;

        itemImage.sprite = this_item.sprite;
        itemNameTMP.text = this_item.item_name;
        itemCostTMP.text = this_item.cost.cost_amount.ToString();
        itemCurrencyImage.sprite = shopPanelScript.dict_costType_to_Item[this_item.cost.cost_type].sprite;
    }

    public void OnClick()
    {
        shopPanelScript.UpdateShowerPanel(id);
    }
}
