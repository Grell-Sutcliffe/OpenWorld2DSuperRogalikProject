using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPackController : MonoBehaviour
{
    public Sprite book_sprite;
    public Sprite pink_wish_sprite;
    public Sprite blue_wish_sprite;
    public Sprite gold_sprite;
    public Sprite primogem_sprite;
    public Sprite green_crystal_sprite;
    public Sprite red_crystal_sprite;
    public Sprite almaz_sprite;
    public Sprite purple_crystal_sprite;

    public GameObject content_GO;
    public GameObject backpackIconPrefab;

    RectTransform content_rect_transform;

    public int item_counter = 0;
    public int current_selected_id;  // !!!

    public int item_height = 400;
    public int space_between_items = 50;
    public int item_in_row = 7;

    public string book_name = "Книга";
    public string pink_wish_name = "Молитва безбрежных небес";
    public string blue_wish_name = "Молитва тихого поднебесья";
    public string gold_name = "Золотая монета";
    public string primogem_name = "Кристалл сотворения";
    public string green_crystal_name = "Пещерный изумруд";
    public string red_crystal_name = "Рубиновый кварц";
    public string almaz_name = "Глубинный алмаз";
    public string purple_crystal_name = "Осколок Александрита";

    public Sprite empty_sprite;

    public Image selectedItemImage;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descriptionTMP;

    public Dictionary<string, int> dict_item_name_to_id = new Dictionary<string, int>();
    public Dictionary<int, Item> dict_id_to_item = new Dictionary<int, Item>();

    public InventoryStalker inventory_stalker;

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
        Item gold = new ConsumableItem(ind, gold_name, "Валюта. Используется повсеместно.", gold_sprite, 150);
        dict_id_to_item[ind] = gold;
        dict_item_name_to_id[gold_name] = ind;

        ind++;
        // primogem
        Item primogem = new ConsumableItem(ind, primogem_name, "Валюта. Используется для внутреигровых покупок.", primogem_sprite, 400);
        dict_id_to_item[ind] = primogem;
        dict_item_name_to_id[primogem_name] = ind;

        ind++;
        // pink wish
        Item pink_wish = new ConsumableItem(ind, pink_wish_name, "Молитва на персонажа.", pink_wish_sprite, 3);
        dict_id_to_item[ind] = pink_wish;
        dict_item_name_to_id[pink_wish_name] = ind;

        ind++;
        // blue wish
        Item blue_wish = new ConsumableItem(ind, blue_wish_name, "Молитва на оружие.", blue_wish_sprite, 11);
        dict_id_to_item[ind] = blue_wish;
        dict_item_name_to_id[blue_wish_name] = ind;

        ind++;
        // green_crystal
        Item green_crystal = new ConsumableItem(ind, green_crystal_name, "Зелёный кристалл. Образуется в самых потаённых уголках пещер. Используется для улучшения оружия.", green_crystal_sprite, 11);
        dict_id_to_item[ind] = green_crystal;
        dict_item_name_to_id[green_crystal_name] = ind;

        ind++;
        // red_crystal
        Item red_crystal = new ConsumableItem(ind, red_crystal_name, "Класный кристалл. Образуется на местности, наиболее озарённой солнечным светом. Используется для улучшения оружия.", red_crystal_sprite, 11);
        dict_id_to_item[ind] = red_crystal;
        dict_item_name_to_id[red_crystal_name] = ind;

        ind++;
        // almaz
        Item almaz = new ConsumableItem(ind, almaz_name, "Наиболее твёрдый минералл в мире. Образуется глубоко в недрах земли. Используется для улучшения оружия.", almaz_sprite, 11);
        dict_id_to_item[ind] = almaz;
        dict_item_name_to_id[almaz_name] = ind;

        ind++;
        // purple_crystal
        Item purple_crystal = new ConsumableItem(ind, purple_crystal_name, "Осколок одного из самых редких минераллов. Найти его в природе - большая удача. Используется для улучшения оружия.", purple_crystal_sprite, 11);
        dict_id_to_item[ind] = purple_crystal;
        dict_item_name_to_id[purple_crystal_name] = ind;

        ind++;
        // book
        Item book = new ConsumableItem(ind, book_name, "Вы можете прочитать эту книгу, она интересная.", book_sprite);
        dict_id_to_item[ind] = book;
        dict_item_name_to_id[book_name] = ind;

        ind++;
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
        Debug.LogError("НЕТУ ТАКОГО В ИНВЕНТАРЕ");
        return -1;
    }

    public void MoveItemToInventoryById(int new_id)
    {
        dict_id_to_item[new_id].count--;
        UpdateBackpack();
    }

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

    void UpdateBackpack()
    {
        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        CountItems();
        ClearBackpack();
        ChangeBackpackPanelHeight();

        Debug.Log(item_counter);
    }

    void CountItems()
    {
        item_counter = 0;

        foreach (int id in dict_id_to_item.Keys)
        {
            Debug.Log($"current item id = {id}, amount = {dict_id_to_item[id].count}");
            if (dict_id_to_item[id].count > 0)
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

    void ChangeBackpackPanelHeight()
    {
        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount + 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].count > 0)
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
