using TMPro;
using UnityEngine;

public class SpecieBuyScript : MonoBehaviour
{
    MiniShopPanelScript miniShopPanelScript;

    public ConsumableItemSO specieSO;

    public TextMeshProUGUI amountTMP;

    public void SetUp()
    {
        miniShopPanelScript = GameObject.Find("MiniShopPanel").GetComponent<MiniShopPanelScript>();

        bool is_active = miniShopPanelScript.UpdateMoneyAmount(specieSO.item_name, 0);

        amountTMP.text = miniShopPanelScript.dict_item_name_to_amount[specieSO.item_name].ToString();

        gameObject.SetActive(is_active);
    }

    public void AddOne()
    {
        bool is_active = miniShopPanelScript.UpdateMoneyAmount(specieSO.item_name, 1);

        amountTMP.text = miniShopPanelScript.dict_item_name_to_amount[specieSO.item_name].ToString();

        gameObject.SetActive(is_active);
    }

    public void MinusOne()
    {
        bool is_active = miniShopPanelScript.UpdateMoneyAmount(specieSO.item_name, -1);

        amountTMP.text = miniShopPanelScript.dict_item_name_to_amount[specieSO.item_name].ToString();

        gameObject.SetActive(is_active);
    }
}
