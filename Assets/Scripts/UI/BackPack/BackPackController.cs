using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPackController : MonoBehaviour
{
    public Sprite book_sprite;

    public GameObject content_GO;
    public GameObject backpackIconPrefab;

    RectTransform content_rect_transform;

    public int item_counter = 0;
    public int current_selected_id;  // !!!

    public int item_height = 400;
    public int space_between_items = 50;
    public int item_in_row = 7;

    public string book_name = "Книга";

    public Sprite empty_sprite;

    public Image selectedItemImage;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI descriptionTMP;

    public Dictionary<int, Item> dict_id_to_item = new Dictionary<int, Item>();

    public InventoryStalker inventory_stalker;

    // public List<int> player_items_id;

    void Start()
    {
        // MakeDictionary();

        content_rect_transform = content_GO.GetComponent<RectTransform>();

        UpdateBackpack();
        ClearBackpack();
    }

    public void MakeDictionary()
    {
        int ind = 1;
        // book
        Item book = new ConsumableItem(ind, book_name, "Вы можете прочитать эту книгу.", book_sprite);
        dict_id_to_item[ind] = book;
        Debug.Log("AAAAAAAAAAA");
        Debug.Log(dict_id_to_item[ind].name);
        Debug.Log(ind);

        ind++;
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
            Debug.Log("BBBBBBBBBBBB");

            Debug.Log(dict_id_to_item[id].name);
            Debug.Log(name);
            if (dict_id_to_item[id].name == name)
            {
                // Debug.Log("Book found");
                Debug.Log("BABABBABA BEBEBEBEB");

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
