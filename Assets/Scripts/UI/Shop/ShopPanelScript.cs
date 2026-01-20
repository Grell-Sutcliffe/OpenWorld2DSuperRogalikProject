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

    public class Currency
    {
        public string name;
        public Sprite sprite;

        public Currency(string name_, Sprite sprite_)
        {
            name = name_;
            sprite = sprite_;
        }
    }

    public class ShopItem
    {
        public int id;
        public string name;
        public int cost;
        public Sprite sprite;
        public Currency currency;

        public ShopItem(int id_, string name_, int cost_, Sprite sprite_, Currency currency_)
        {
            id = id_;
            name = name_;
            cost = cost_;
            sprite = sprite_;
            currency = currency_;
        }
    }

    Currency primogem;
    Currency gold;

    public Dictionary<int, ShopItem> dict_id_to_shop_item = new Dictionary<int, ShopItem>();

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        //backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();

        content_rect_transform = content_GO.GetComponent<RectTransform>();

        gold = new Currency(backpackController.gold_name, backpackController.gold_sprite);
        primogem = new Currency(backpackController.primogem_name, backpackController.primogem_sprite);

        MakeDictionary();
    }

    void Start()
    {

    }

    void MakeDictionary()
    {
        int ind = 0;

        // pink_wish
        ind++;
        ShopItem pink_wish = new ShopItem(ind, backpackController.pink_wish_name, 150, backpackController.pink_wish_sprite, primogem);
        dict_id_to_shop_item[ind] = pink_wish;

        // blue_wish
        ind++;
        ShopItem blue_wish = new ShopItem(ind, backpackController.blue_wish_name, 150, backpackController.blue_wish_sprite, primogem);
        dict_id_to_shop_item[ind] = blue_wish;

        // green_crystal
        ind++;
        ShopItem green_crystal = new ShopItem(ind, backpackController.green_crystal_name, 100, backpackController.green_crystal_sprite, gold);
        dict_id_to_shop_item[ind] = green_crystal;

        // green_crystal
        ind++;
        ShopItem red_crystal = new ShopItem(ind, backpackController.red_crystal_name, 120, backpackController.red_crystal_sprite, gold);
        dict_id_to_shop_item[ind] = red_crystal;

        // green_crystal
        ind++;
        ShopItem almaz = new ShopItem(ind, backpackController.almaz_name, 160, backpackController.almaz_sprite, gold);
        dict_id_to_shop_item[ind] = almaz;

        // green_crystal
        ind++;
        ShopItem purple_crystal = new ShopItem(ind, backpackController.purple_crystal_name, 140, backpackController.purple_crystal_sprite, gold);
        dict_id_to_shop_item[ind] = purple_crystal;

        item_counter = ind;
    }

    public void Buy()
    {
        // !!!
        if (current_cost > backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[dict_id_to_shop_item[current_selected_id].currency.name]].count)
        {
            return;
        }

        backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[dict_id_to_shop_item[current_selected_id].name]].count += current_amount;
        backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[dict_id_to_shop_item[current_selected_id].currency.name]].count -= current_cost;

        UpdateTopInfoPanel();
    }

    public void UpdateTopInfoPanel()
    {
        gold_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.gold_name]].count.ToString();
        primogem_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.primogem_name]].count.ToString();
        pink_wish_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.pink_wish_name]].count.ToString();
        blue_wish_amount_TMP.text = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[backpackController.blue_wish_name]].count.ToString();

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

    public void IncreaceCurrentAmount()
    {
        if (current_cost + current_currency_amount <= backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[dict_id_to_shop_item[current_selected_id].currency.name]].count)
        {
            current_amount++;
            UpdateCurrentAmount();
        }
    }

    public void MaxIncreaceCurrentAmount()
    {
        current_amount = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[dict_id_to_shop_item[current_selected_id].currency.name]].count / current_currency_amount;
        UpdateCurrentAmount();
    }

    public void UpdateShowerPanel(int new_id)
    {
        current_selected_id = new_id;  // !!!
        current_amount = 1;
        current_currency_amount = dict_id_to_shop_item[new_id].cost;

        selectedItemImage.sprite = dict_id_to_shop_item[new_id].sprite;
        selectedItemNameTMP.text = dict_id_to_shop_item[new_id].name;

        UpdateCurrentAmount();
        currentCurrencyImage.sprite = dict_id_to_shop_item[new_id].currency.sprite;
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
        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount - 1) * space_between_items + extra_space;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (int id in dict_id_to_shop_item.Keys)
        {
            SpawnShopIconPrefab(id);
        }
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
}
