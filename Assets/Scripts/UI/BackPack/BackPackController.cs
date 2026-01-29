using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterPanelScript;

public class BackPackController : MonoBehaviour
{
    public ShopPanelScript shopPanelScript;
    public CharacterPanelScript characterPanelScript;

    public GameObject content_GO;
    public GameObject backpackIconPrefab;

    RectTransform content_rect_transform;

    public int gold_amount = 2500;
    public int primogem_amount = 3400;
    public int pink_wish_amount = 12;
    public int blue_wish_amount = 67;

    public int item_counter = 0;
    public int current_selected_id;  // !!!

    public int item_height = 400;
    public int space_between_items = 50;
    public int item_in_row = 7;

    public Sprite empty_sprite;

    public Image selectedItemImage;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descriptionTMP;

    public GameObject weaponIconGO;
    public TextMeshProUGUI starTMP;
    public Image elementImage;

    public Dictionary<string, int> dict_item_name_to_id = new Dictionary<string, int>();
    public Dictionary<int, Item> dict_id_to_item = new Dictionary<int, Item>();

    public InventoryStalker inventory_stalker;

    [Header("Reward & Currency Items")]
    public ConsumableItemSO primogemSO;
    public ConsumableItemSO goldSO;
    public ItemForSaleSO pinkWishSO;
    public ItemForSaleSO blueWishSO;

    [Header("Сonsumable Items")]
    public ConsumableItemSO[] list_consumableItem_so;

    [Header("Items for sale")]
    public ItemForSaleSO[] list_itemsForSale_so;

    [Header("Weapons")]
    public WeaponSO[] list_weapon_so;

    // public List<int> player_items_id;

    private void Awake()
    {
        MakeDictionary();
    }

    void Start()
    {
        weaponIconGO.SetActive(false);
        // shopPanelScript = GameObject.Find("ShopPanel").GetComponent<ShopPanelScript>();

        content_rect_transform = content_GO.GetComponent<RectTransform>();
        
        UpdateBackpack();
        ClearShowerPanel();
    }

    public void MakeDictionary()
    {
        int ind = 0;

        ind++;
        Item pink_wish = new ItemForSale(pinkWishSO, ind, pink_wish_amount);  // pink_wish must have id = 1
        dict_id_to_item[ind] = pink_wish;
        dict_item_name_to_id[pink_wish.item_name] = ind;
        shopPanelScript.MakeCurrency(CostType.PinkWish, pink_wish);

        ind++;
        Item blue_wish = new ItemForSale(blueWishSO, ind, blue_wish_amount);
        dict_id_to_item[ind] = blue_wish;
        dict_item_name_to_id[blue_wish.item_name] = ind;
        shopPanelScript.MakeCurrency(CostType.BlueWish, blue_wish);

        ind++;
        Item gold = new ConsumableItem(goldSO, ind, gold_amount);
        //Debug.LogError($"gold == null ? --- {gold == null}");
        dict_id_to_item[ind] = gold;
        dict_item_name_to_id[gold.item_name] = ind;
        shopPanelScript.MakeCurrency(CostType.Gold, gold);

        ind++;
        Item primogem = new ConsumableItem(primogemSO, ind, primogem_amount);
        dict_id_to_item[ind] = primogem;
        dict_item_name_to_id[primogem.item_name] = ind;
        shopPanelScript.MakeCurrency(CostType.Primogem, primogem);

        foreach (ItemForSaleSO itemForSale_so in list_itemsForSale_so)
        {
            ind++;
            Item temp_item = new ItemForSale(itemForSale_so, ind);
            dict_id_to_item[ind] = temp_item;
            dict_item_name_to_id[temp_item.item_name] = ind;
        }

        foreach (ConsumableItemSO consumableItem_so in list_consumableItem_so)
        {
            ind++;
            Item temp_item = new ConsumableItem(consumableItem_so, ind);
            dict_id_to_item[ind] = temp_item;
            dict_item_name_to_id[temp_item.item_name] = ind;
        }

        foreach (WeaponSO weapon_so in list_weapon_so)
        {
            ind++;
            Item temp_weapon = new Weapon(weapon_so, ind);
            dict_id_to_item[ind] = temp_weapon;
            dict_item_name_to_id[temp_weapon.item_name] = ind;
        }
    }

    public bool DecreaceItemByName(string item_name, int number)
    {
        int item_id = dict_item_name_to_id[item_name];

        if (dict_id_to_item[item_id].amount - number >= 0)
        {
            dict_id_to_item[item_id].amount -= number;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaceItemByName(string item_name, int number)
    {
        int item_id = dict_item_name_to_id[item_name];

        dict_id_to_item[item_id].amount += number;

        //Debug.Log($"[INC] this={name} id={GetInstanceID()} dictHash={dict_id_to_item.GetHashCode()}");
        /*
        Debug.LogError($"----------> INCREACING AMOUNT OF {item_name}. item_id = {item_id}\n" +
            $"dict_id_to_item[item_id].item_name = {dict_id_to_item[item_id].item_name}, " +
            $"type = {dict_id_to_item[item_id].item_type}, amount = {dict_id_to_item[item_id].amount}\n" +
            $"{dict_id_to_item[item_id].description}");
        */
    }

    public int GetItemCounterByName(string name)
    {
        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].item_name == name)
            {
                return dict_id_to_item[id].amount;
            }
        }
        Debug.LogError("ERROR: !!! NOT COUNTABLE !!!");
        return -1;
    }

    public Item GetItemByName(string name)
    {
        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].item_name == name)
            {
                return dict_id_to_item[id];
            }
        }
        Debug.LogError("ERROR: !!! NOT FOUND !!!");
        return null;
    }

    /*
    public void MoveItemToInventoryById(int new_id)
    {
        dict_id_to_item[new_id].count--;
        UpdateBackpack();
    }
    */

    public void UpdateShowerPanel(int new_id)
    {
        weaponIconGO.SetActive(false);

        current_selected_id = new_id;  // !!!

        selectedItemImage.sprite = dict_id_to_item[new_id].sprite;
        nameTMP.text = dict_id_to_item[new_id].item_name;
        descriptionTMP.text = dict_id_to_item[new_id].description;

        if (dict_id_to_item[current_selected_id].item_type == ItemType.Weapon)
        {
            ActivateWeaponIcon();
        }
    }

    public void ClearShowerPanel()
    {
        selectedItemImage.sprite = empty_sprite;
        nameTMP.text = "";
        descriptionTMP.text = "";
    }

    public void TakeByName(string name)
    {
        foreach (int id in dict_id_to_item.Keys)
        {
            Debug.Log("Take by name");

            Debug.Log(dict_id_to_item[id].item_name);
            Debug.Log(name);
            if (dict_id_to_item[id].item_name == name)
            {
                Debug.Log("Book found");

                dict_id_to_item[id].amount++;
                Debug.Log(dict_id_to_item[id].amount);

                break;
            }
        }
    }

    public void ShowEverything()
    {
        UpdateBackpack();
    }

    public void ShowWeapon()
    {
        UpdateBackpack(ItemType.Weapon);
    }

    public void ShowFood()
    {
        UpdateBackpack(ItemType.Food);
    }

    public void ShowDrink()
    {
        UpdateBackpack(ItemType.Drink);
    }

    public void ShowMaterials()
    {
        UpdateBackpack(ItemType.Materials);
    }

    public void ShowQuest()
    {
        UpdateBackpack(ItemType.Quest);
    }

    void UpdateBackpack(ItemType type = ItemType.Everything)
    {
        weaponIconGO.SetActive(false);

        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        CountItems(type);
        ClearBackpack();
        ChangeBackpackPanelHeight(type);

        //Debug.Log(item_counter);
    }

    void CountItems(ItemType type)
    {
        item_counter = 0;

        foreach (int id in dict_id_to_item.Keys)
        {
            //Debug.Log($"current item id = {id}, amount = {dict_id_to_item[id].amount}");
            if (dict_id_to_item[id].amount > 0 && (dict_id_to_item[id].item_type == type || type == ItemType.Everything))
            {
                item_counter++;
            }
        }
    }

    void ClearBackpack()
    {
        foreach (Transform child in content_GO.transform)
        {
            //Debug.Log($"delete item");
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }    

    void ChangeBackpackPanelHeight(ItemType type)
    {
        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount + 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        //Debug.Log($"[SPAWN] this={name} id={GetInstanceID()} dictHash={dict_id_to_item.GetHashCode()}");

        foreach (int id in dict_id_to_item.Keys)
        {
            //Debug.LogError($"{dict_id_to_item[id].item_name}.amount = {dict_id_to_item[id].amount}");
            if (dict_id_to_item[id].amount > 0 && (dict_id_to_item[id].item_type == type || type == ItemType.Everything))
            {
                SpawnBackpackIconPrefab(id);
            }
        }
    }

    void SpawnBackpackIconPrefab(int id)
    {
        GameObject new_prefab = Instantiate(backpackIconPrefab, content_GO.transform);
        BackpackIconScript new_prefab_script = new_prefab.GetComponent<BackpackIconScript>();

        new_prefab_script.SetNewId(id);
        new_prefab_script.inventory_stalker = inventory_stalker;
    }

    public void OpenBackpackPanel()
    {
        gameObject.SetActive(true);
        ClearShowerPanel();
        UpdateBackpack();
    }

    public void CloseBackpackPanel()
    {
        gameObject.SetActive(false);
    }

    public Element GetElementByElementType(ElementType element_type)
    {
        return characterPanelScript.dict_element_type_to_element[element_type];
    }

    void ActivateWeaponIcon()
    {
        weaponIconGO.SetActive(true);

        Weapon weapon = null;
        if (dict_id_to_item[current_selected_id] is Weapon temp)
        {
            weapon = temp;
        }
        if (weapon == null) return;

        elementImage.sprite = GetElementByElementType(weapon.element).sprite;
        starTMP.text = weapon.stars.ToString();
    }
}
