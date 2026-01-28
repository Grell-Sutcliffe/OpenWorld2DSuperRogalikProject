using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelScript : MonoBehaviour
{
    MainController mainController;
    public BackPackController backpackController;

    public TextMeshProUGUI gold_amount_TMP;
    public TextMeshProUGUI primogem_amount_TMP;
    public TextMeshProUGUI pink_wish_amount_TMP;
    public TextMeshProUGUI blue_wish_amount_TMP;

    public TextMeshProUGUI current_amout_TMP;
    public TextMeshProUGUI current_cost_TMP;
    public Image currentCurrencyImage;

    public GameObject content_GO;
    public GameObject shopIconPrefab;

    public Image selectedItemImage;
    public TextMeshProUGUI selectedItemNameTMP;

    RectTransform content_rect_transform;

    public int item_counter = 0;
    public int item_in_row = 3;
    public int item_height = 896;
    public int space_between_items = 50;
    public int extra_space = 50;

    int current_selected_id = 1;
    int current_amount = 1;
    int current_currency_amount = 0;
    int current_cost = 0;

    Item current_cost_item;

    public class Cost
    {
        public int cost_amount;
        public CostType cost_type;

        public Cost(int cost_amount, CostType cost_type)
        {
            this.cost_amount = cost_amount;
            this.cost_type = cost_type;
        }
    }

    public class ShopItem
    {
        ConsumableItem item_for_sell;
        //public Currency currency;
        Cost cost;

        public ShopItem(ConsumableItem item_for_sell, Cost cost)
        {
            this.item_for_sell = item_for_sell;
            this.cost = cost;
        }
    }

    public Dictionary<CostType, Item> dict_costType_to_Item = new Dictionary<CostType, Item>();

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        //backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();

        content_rect_transform = content_GO.GetComponent<RectTransform>();
    }

    void Start()
    {

    }

    public void MakeCurrency(CostType cost_type, Item cost_item)
    {
        dict_costType_to_Item[cost_type] = cost_item;
    }

    public void Buy()
    {
        if (current_cost > current_cost_item.amount)
        {
            return;
        }

        Item current_selected_item = backpackController.dict_id_to_item[current_selected_id];

        current_selected_item.amount += current_amount;
        current_cost_item.amount -= current_cost;

        UpdateTopInfoPanel();
    }

    public void UpdateTopInfoPanel()
    {
        Item item_gold = dict_costType_to_Item[CostType.Gold];
        gold_amount_TMP.text = item_gold.amount.ToString();
        //gold_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.gold_name]].amount.ToString();
        Item item_primogem = dict_costType_to_Item[CostType.Primogem];
        primogem_amount_TMP.text = item_primogem.amount.ToString();
        //primogem_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.primogem_name]].count.ToString();
        Item item_pink_wish = dict_costType_to_Item[CostType.PinkWish];
        pink_wish_amount_TMP.text = item_pink_wish.amount.ToString();
        //pink_wish_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.pink_wish_name]].count.ToString();
        Item item_blue_wish = dict_costType_to_Item[CostType.BlueWish];
        blue_wish_amount_TMP.text = item_blue_wish.amount.ToString();
        //blue_wish_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.blue_wish_name]].count.ToString();

        mainController.UpdateWishPanelInfo();
    }

    public void MaxDecreaceCurrentAmount()
    {
        current_amount = 1;
        UpdateCurrentAmount();
    }

    public void DecreaceCurrentAmount()
    {
        if (current_amount > 1)
        {
            current_amount--;
            UpdateCurrentAmount();
        }
    }

    void UpdateCurrentCostItem()
    {
        current_cost_item = null;

        Item current_selected_item = backpackController.dict_id_to_item[current_selected_id];
        if (current_selected_item is ItemForSale item_for_sale)
        {
            current_cost_item = dict_costType_to_Item[item_for_sale.cost.cost_type];
        }
        if (current_cost_item == null)
        {
            Debug.LogError("ERROR: !!! current_cost_item == null !!!");
            return;
        }
    }

    public void IncreaceCurrentAmount()
    {
        //UpdateCurrentCostItem();

        if (current_cost + current_currency_amount <= current_cost_item.amount)
        {
            current_amount++;
            UpdateCurrentAmount();
        }
    }

    public void MaxIncreaceCurrentAmount()
    {
        //UpdateCurrentCostItem();

        current_amount = current_cost_item.amount / current_currency_amount;
        UpdateCurrentAmount();
    }

    public void UpdateShowerPanel(int new_id)
    {
        current_selected_id = new_id;  // !!!

        UpdateCurrentCostItem();

        current_amount = 1;

        ItemForSale temp_item = null;
        if (backpackController.dict_id_to_item[current_selected_id] is ItemForSale current_item_for_sale)
        {
            temp_item = current_item_for_sale;
        }
        if (temp_item == null) return;

        current_currency_amount = temp_item.cost.cost_amount;
        selectedItemImage.sprite = temp_item.sprite;
        selectedItemNameTMP.text = temp_item.item_name;

        UpdateCurrentAmount();
        currentCurrencyImage.sprite = dict_costType_to_Item[temp_item.cost.cost_type].sprite;
    }

    void UpdateCurrentAmount()
    {
        current_amout_TMP.text = current_amount.ToString();
        current_cost = current_currency_amount * current_amount;
        current_cost_TMP.text = current_cost.ToString();
    }

    public void UpdateShopPanelInfo()
    {
        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        ClearShopPanel();
        ChangeShopPanelHeight();

        Debug.Log(item_counter);
    }

    void ClearShopPanel()
    {
        foreach (Transform child in content_GO.transform)
        {
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void ChangeShopPanelHeight()
    {
        item_counter = 0;

        foreach (int id in backpackController.dict_id_to_item.Keys)
        {
            if (backpackController.dict_id_to_item[id] is ItemForSale new_item_for_sale)
            {
                item_counter++;
                SpawnShopIconPrefab(id);
            }
        }

        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount - 1) * space_between_items + extra_space;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);
    }

    void SpawnShopIconPrefab(int id)
    {
        GameObject new_prefab = Instantiate(shopIconPrefab, content_GO.transform);

        ShopItemScript new_prefab_script = new_prefab.GetComponent<ShopItemScript>();

        new_prefab_script.SetShopItem(id);
    }

    public void OpenShopPanel()
    {
        gameObject.SetActive(true);

        UpdateTopInfoPanel();
        UpdateShowerPanel(1);
        UpdateShopPanelInfo();
    }

    public Item GetItemById(int id)
    {
        return backpackController.dict_id_to_item[id];
    }
}

public enum CostType
{
    Primogem = 0,
    Gold = 1,
    PinkWish = 2,
    BlueWish = 3,
}
