using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPackController : MonoBehaviour
{
    public Sprite book_sprite;
    public Sprite pink_wish_sprite;
    public Sprite blue_wish_sprite;
    public Sprite sword_red_sprite;
    public Sprite sword_white_sprite;
    public Sprite sword_gold_sprite;
    public Sprite sword_death_sprite;
    public Sprite sword_purple_sprite;
    public Sprite sword_grey_sprite;
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

    const string type_everything = "everyting";
    const string type_weapon = "weapon";
    const string type_food = "food";
    const string type_drink = "drink";
    const string type_materials = "materials";
    const string type_quest = "quest";

    public string book_name = "Книга";
    public string pink_wish_name = "Ìîëèòâà áåçáðåæíûõ íåáåñ";
    public string blue_wish_name = "Ìîëèòâà òèõîãî ïîäíåáåñüÿ";
    public string sword_red_name = "Àäñêîå ïëàìÿ";
    public string sword_white_name = "Êàðà áåçáðåæíûõ íåáåñ";
    public string sword_gold_name = "Âîçíåñåíèå ê ñîëíöó";
    public string sword_death_name = "Êîñà ñìåðòè";
    public string sword_purple_name = "Áåçûìÿííàÿ ïàìÿòü";
    public string sword_grey_name = "Ïðèçðà÷íàÿ èãëà";
    public string gold_name = "Çîëîòàÿ ìîíåòà";
    public string primogem_name = "Êðèñòàëë ñîòâîðåíèÿ";
    public string green_crystal_name = "Ïåùåðíûé èçóìðóä";
    public string red_crystal_name = "Ðóáèíîâûé êâàðö";
    public string almaz_name = "Ãëóáèííûé àëìàç";
    public string purple_crystal_name = "Îñêîëîê Àëåêñàíäðèòà";

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
        Item gold = new ConsumableItem(ind, gold_name, "Âàëþòà. Èñïîëüçóåòñÿ ïîâñåìåñòíî.", gold_sprite, type_materials, 150);
        dict_id_to_item[ind] = gold;
        dict_item_name_to_id[gold_name] = ind;

        ind++;
        // primogem
        Item primogem = new ConsumableItem(ind, primogem_name, "Âàëþòà. Èñïîëüçóåòñÿ äëÿ âíóòðåèãðîâûõ ïîêóïîê.", primogem_sprite, type_materials, 1200);
        dict_id_to_item[ind] = primogem;
        dict_item_name_to_id[primogem_name] = ind;

        ind++;
        // pink wish
        Item pink_wish = new ConsumableItem(ind, pink_wish_name, "Ìîëèòâà íà ïåðñîíàæà.", pink_wish_sprite, type_materials, 3);
        dict_id_to_item[ind] = pink_wish;
        dict_item_name_to_id[pink_wish_name] = ind;

        ind++;
        // blue wish
        Item blue_wish = new ConsumableItem(ind, blue_wish_name, "Ìîëèòâà íà îðóæèå.", blue_wish_sprite, type_materials, 11);
        dict_id_to_item[ind] = blue_wish;
        dict_item_name_to_id[blue_wish_name] = ind;

        ind++;
        // sword_red
        Item sword_red = new ConsumableItem(ind, sword_red_name, "Ìå÷ àäñêîãî ïëàìåíè.", sword_red_sprite, type_weapon, 5, 1);
        dict_id_to_item[ind] = sword_red;
        dict_item_name_to_id[sword_red_name] = ind;

        ind++;
        // sword_white
        Item sword_white = new ConsumableItem(ind, sword_white_name, "Ìå÷ áåçáðåæíûõ íåáåñ.", sword_white_sprite, type_weapon, 5, 1);
        dict_id_to_item[ind] = sword_white;
        dict_item_name_to_id[sword_white_name] = ind;

        ind++;
        // sword_gold
        Item sword_gold = new ConsumableItem(ind, sword_gold_name, "Ìå÷ èç ÷èñòîãî çîëîòà.", sword_gold_sprite, type_weapon, 5, 1);
        dict_id_to_item[ind] = sword_gold;
        dict_item_name_to_id[sword_gold_name] = ind;

        ind++;
        // sword_death
        Item sword_death = new ConsumableItem(ind, sword_death_name, "Ìå÷ ñîçäàííûé ñàìèì áîãîì ñìåðòè.", sword_death_sprite, type_weapon, 4, 1);
        dict_id_to_item[ind] = sword_death;
        dict_item_name_to_id[sword_death_name] = ind;

        ind++;
        // sword_purple
        Item sword_purple = new ConsumableItem(ind, sword_purple_name, "Ìå÷ óòåðÿííûé íàâå÷íî.", sword_purple_sprite, type_weapon, 4, 1);
        dict_id_to_item[ind] = sword_purple;
        dict_item_name_to_id[sword_purple_name] = ind;

        ind++;
        // sword_grey
        Item sword_grey = new ConsumableItem(ind, sword_grey_name, "Íàèáîëåå îñòðûé ìå÷ èç æåëåçà.", sword_grey_sprite, type_weapon, 4, 1);
        dict_id_to_item[ind] = sword_grey;
        dict_item_name_to_id[sword_grey_name] = ind;

        ind++;
        // green_crystal
        Item green_crystal = new ConsumableItem(ind, green_crystal_name, "Çåë¸íûé êðèñòàëë. Îáðàçóåòñÿ â ñàìûõ ïîòà¸ííûõ óãîëêàõ ïåùåð. Èñïîëüçóåòñÿ äëÿ óëó÷øåíèÿ îðóæèÿ.", green_crystal_sprite, type_materials, 2);
        dict_id_to_item[ind] = green_crystal;
        dict_item_name_to_id[green_crystal_name] = ind;

        ind++;
        // red_crystal
        Item red_crystal = new ConsumableItem(ind, red_crystal_name, "Êëàñíûé êðèñòàëë. Îáðàçóåòñÿ íà ìåñòíîñòè, íàèáîëåå îçàð¸ííîé ñîëíå÷íûì ñâåòîì. Èñïîëüçóåòñÿ äëÿ óëó÷øåíèÿ îðóæèÿ.", red_crystal_sprite, type_materials, 1);
        dict_id_to_item[ind] = red_crystal;
        dict_item_name_to_id[red_crystal_name] = ind;

        ind++;
        // almaz
        Item almaz = new ConsumableItem(ind, almaz_name, "Íàèáîëåå òâ¸ðäûé ìèíåðàëë â ìèðå. Îáðàçóåòñÿ ãëóáîêî â íåäðàõ çåìëè. Èñïîëüçóåòñÿ äëÿ óëó÷øåíèÿ îðóæèÿ.", almaz_sprite, type_materials, 4);
        dict_id_to_item[ind] = almaz;
        dict_item_name_to_id[almaz_name] = ind;

        ind++;
        // purple_crystal
        Item purple_crystal = new ConsumableItem(ind, purple_crystal_name, "Îñêîëîê îäíîãî èç ñàìûõ ðåäêèõ ìèíåðàëëîâ. Íàéòè åãî â ïðèðîäå - áîëüøàÿ óäà÷à. Èñïîëüçóåòñÿ äëÿ óëó÷øåíèÿ îðóæèÿ.", purple_crystal_sprite, type_materials, 3);
        dict_id_to_item[ind] = purple_crystal;
        dict_item_name_to_id[purple_crystal_name] = ind;

        ind++;
        // book
        Item book = new ConsumableItem(ind, book_name, "Вы можете прочитать эту книгу.", book_sprite, type_quest, 0);
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
        UpdateBackpack(type_weapon);
    }

    public void ShowFood()
    {
        UpdateBackpack(type_food);
    }

    public void ShowDrink()
    {
        UpdateBackpack(type_drink);
    }

    public void ShowMaterials()
    {
        UpdateBackpack(type_materials);
    }

    public void ShowQuest()
    {
        UpdateBackpack(type_quest);
    }

    void UpdateBackpack(string type = type_everything)
    {
        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        CountItems(type);
        ClearBackpack();
        ChangeBackpackPanelHeight(type);

        Debug.Log(item_counter);
    }

    void CountItems(string type)
    {
        item_counter = 0;

        foreach (int id in dict_id_to_item.Keys)
        {
            Debug.Log($"current item id = {id}, amount = {dict_id_to_item[id].count}");
            if (dict_id_to_item[id].count > 0 && (dict_id_to_item[id].type == type || type == type_everything))
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

    void ChangeBackpackPanelHeight(string type)
    {
        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount + 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].count > 0 && (dict_id_to_item[id].type == type || type == type_everything))
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
