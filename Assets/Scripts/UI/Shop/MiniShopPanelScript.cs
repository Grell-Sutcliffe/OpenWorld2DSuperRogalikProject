using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniShopPanelScript : MonoBehaviour
{
    BackPackController backpackController;
    MainController mainController;

    public ItemSO valutaItemSO;

    public int cost = 75;

    public TextMeshProUGUI moneyAmountTMP;
    public TextMeshProUGUI costAmountTMP;

    public GameObject sugarGO;
    public GameObject saltGO;
    public GameObject paprerGO;

    public List<ConsumableItemSO> consumableItemSOs;

    public Dictionary<string, int> dict_item_name_to_amount;

    int current_sum = 0;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        dict_item_name_to_amount = new Dictionary<string, int>();

        foreach (ConsumableItemSO consumableItemSO in consumableItemSOs)
        {
            dict_item_name_to_amount[consumableItemSO.item_name] = 0;
        }

        sugarGO.GetComponent<SpecieBuyScript>().SetUp();
        saltGO.GetComponent<SpecieBuyScript>().SetUp();
        paprerGO.GetComponent<SpecieBuyScript>().SetUp();

        //ClearPanel();
    }

    void ClearPanel()
    {
        moneyAmountTMP.text = backpackController.GetItemAmountByName(valutaItemSO.item_name).ToString();

        current_sum = 0;

        foreach (ConsumableItemSO consumableItemSO in consumableItemSOs)
        {
            dict_item_name_to_amount[consumableItemSO.item_name] = 0;
        }

        sugarGO.GetComponent<SpecieBuyScript>().SetUp();
        saltGO.GetComponent<SpecieBuyScript>().SetUp();
        paprerGO.GetComponent<SpecieBuyScript>().SetUp();
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);

        ClearPanel();
    }

    public bool UpdateMoneyAmount(string item_name, int amount)
    {
        dict_item_name_to_amount[item_name] += amount;

        current_sum = 0;
        foreach (string key in dict_item_name_to_amount.Keys)
        {
            current_sum += cost * dict_item_name_to_amount[key];
        }

        costAmountTMP.text = current_sum.ToString();

        if (dict_item_name_to_amount[item_name] <= 0)
        {
            return false;
        }
        else 
        { 
            return true; 
        }
    }

    public void BuyButton()
    {
        if (backpackController.GetItemAmountByName(valutaItemSO.item_name) < current_sum)
        {
            mainController.OpenErrorPanel(ErrorType.NotEnoughMaterials);
            return;
        }

        backpackController.DecreaceItemByName(valutaItemSO.item_name, current_sum);

        foreach (string key in dict_item_name_to_amount.Keys)
        {
            backpackController.IncreaceItemByName(key, dict_item_name_to_amount[key]);
        }

        moneyAmountTMP.text = backpackController.GetItemAmountByName(valutaItemSO.item_name).ToString();

        ClearPanel();
    }
}
