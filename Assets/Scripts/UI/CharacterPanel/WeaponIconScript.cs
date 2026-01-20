using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconScript : MonoBehaviour
{
    public int id;
    public Image itemImage;
    public TextMeshProUGUI itemNameTMP;
    public TextMeshProUGUI itemLevelTMP;
    public TextMeshProUGUI itemStarTMP;

    void Start()
    {

    }

    public void OnClick()
    {
        // return id;
    }

    public void SetWeaponItem(int new_id)
    {
        //if (shopPanelScript == null) shopPanelScript = GameObject.Find("ShopPanel").GetComponent<ShopPanelScript>();

        id = new_id;
        /*
        itemImage.sprite = shopPanelScript.dict_id_to_shop_item[id].sprite;
        itemNameTMP.text = shopPanelScript.dict_id_to_shop_item[id].name;
        itemCostTMP.text = shopPanelScript.dict_id_to_shop_item[id].cost.ToString();
        itemCurrencyImage.sprite = shopPanelScript.dict_id_to_shop_item[id].currency.sprite;
        */
    }
}
