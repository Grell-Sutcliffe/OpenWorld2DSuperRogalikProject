using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPackController : MonoBehaviour
{
    public Sprite gold_sprite;
    public Sprite primogem_sprite;

    public string gold_name = "Çîëîòàÿ ìîíåòà";
    public string primogem_name = "Êðèñòàëë ñîòâîðåíèÿ";

    public GameObject content_GO;
    public GameObject backpackIconPrefab;

    RectTransform content_rect_transform;

    public int item_counter = 0;
    public int current_selected_id;  // !!!

    public int item_height = 400;
    public int space_between_items = 50;
    public int item_in_row = 7;

    public Sprite empty_sprite;

    public Image selectedItemImage;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descriptionTMP;

    public Dictionary<string, int> dict_item_name_to_id = new Dictionary<string, int>();
    public Dictionary<int, Item> dict_id_to_item = new Dictionary<int, Item>();

    public InventoryStalker inventory_stalker;

    [Header("Оружки")]
    public WeaponSO[] list_weapon_so;

    // public List<int> player_items_id;

    void Start()
    {
        // MakeDictionary();

        content_rect_transform = content_GO.GetComponent<RectTransform>();

        UpdateBackpack();
        ClearShowerPanel();
    }

    public void MakeDictionary()
    {
        int ind = 0;

        ind++;
        // gold
        Item gold = new ConsumableItem(ind, gold_name, "Валюта.", gold_sprite, ItemType.Materials, 150);
        dict_id_to_item[ind] = gold;
        dict_item_name_to_id[gold_name] = ind;

        ind++;
        // primogem
        Item primogem = new ConsumableItem(ind, primogem_name, "Âàëþòà. Èñïîëüçóåòñÿ äëÿ âíóòðåèãðîâûõ ïîêóïîê.", primogem_sprite, ItemType.Materials, 1200);
        dict_id_to_item[ind] = primogem;
        dict_item_name_to_id[primogem_name] = ind;

        ind++;
        // pink wish
        Item pink_wish = new ConsumableItem(ind, pink_wish_name, "Ìîëèòâà íà ïåðñîíàæà.", pink_wish_sprite, ItemType.Materials, 3);
        dict_id_to_item[ind] = pink_wish;
        dict_item_name_to_id[pink_wish_name] = ind;

        ind++;
        // blue wish
        Item blue_wish = new ConsumableItem(ind, blue_wish_name, "Ìîëèòâà íà îðóæèå.", blue_wish_sprite, ItemType.Materials, 11);
        dict_id_to_item[ind] = blue_wish;
        dict_item_name_to_id[blue_wish_name] = ind;

        foreach (WeaponSO weapon_so in list_weapon_so)
        {
            ind++;
            Item temp_weapon = new Weapon(weapon_so);
            dict_id_to_item[ind] = temp_weapon;
            dict_item_name_to_id[temp_weapon.name] = ind;
        }
    }

    public bool DecreaceItemByName(string item_name, int number)
    {
        int item_id = dict_item_name_to_id[item_name];

        if (dict_id_to_item[item_id].count - number >= 0)
        {
            dict_id_to_item[item_id].count -= number;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetItemCounterByName(string name)
    {
        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].name == name)
            {
                return dict_id_to_item[id].count;
            }
        }
        Debug.LogError("ÍÅÒÓ ÒÀÊÎÃÎ Â ÈÍÂÅÍÒÀÐÅ");
        return -1;
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
        current_selected_id = new_id;  // !!!

        selectedItemImage.sprite = dict_id_to_item[new_id].sprite;
        nameTMP.text = dict_id_to_item[new_id].name;
        descriptionTMP.text = dict_id_to_item[new_id].description;
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

            Debug.Log(dict_id_to_item[id].name);
            Debug.Log(name);
            if (dict_id_to_item[id].name == name)
            {
                Debug.Log("Book found");

                dict_id_to_item[id].count++;
                Debug.Log(dict_id_to_item[id].count);

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
        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        CountItems(type);
        ClearBackpack();
        ChangeBackpackPanelHeight(type);

        Debug.Log(item_counter);
    }

    void CountItems(ItemType type)
    {
        item_counter = 0;

        foreach (int id in dict_id_to_item.Keys)
        {
            Debug.Log($"current item id = {id}, amount = {dict_id_to_item[id].count}");
            if (dict_id_to_item[id].count > 0 && (dict_id_to_item[id].type == type || type == ItemType.Everything))
            {
                item_counter++;
            }
        }
    }

    void ClearBackpack()
    {
        foreach (Transform child in content_GO.transform)
        {
            Debug.Log($"delete item");
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }    

    void ChangeBackpackPanelHeight(ItemType type)
    {
        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount + 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].count > 0 && (dict_id_to_item[id].type == type || type == ItemType.Everything))
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
}

public enum ItemType
{
    Everything = 0,
    Weapon = 1,
    Food = 2,
    Drink = 3,
    Materials = 4,
    Quest = 5,
}
