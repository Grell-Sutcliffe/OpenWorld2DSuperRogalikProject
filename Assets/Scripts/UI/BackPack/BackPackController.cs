using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackController : MonoBehaviour
{
    public Sprite book_sprite;

    public GameObject content_GO;
    public GameObject backpackIconPrefab;

    RectTransform content_rect_transform;

    public int item_counter = 0;

    public int item_height = 400;
    public int space_between_items = 50;
    public int item_in_row = 7;

    public string book_name = "Книга";

    public class Item
    {
        public string name;
        public string description;
        public int count;

        public Sprite sprite;

        public Item()
        {
            name = "";
            description = "";
            count = 0;
        }

        public Item(string name_)
        {
            name = name_;
            description = "";
            count = 0;
        }

        public Item(string name_, string description_, Sprite sprite_)
        {
            name = name_;
            description = description_;
            count = 0;
            sprite = sprite_;
        }
    }

    public Dictionary<int, Item> dict_id_to_item = new Dictionary<int, Item>();

    // public List<int> player_items_id;

    void Start()
    {
        MakeDictionary();

        content_rect_transform = content_GO.GetComponent<RectTransform>();
    }

    void MakeDictionary()
    {
        int ind = 1;

        // book
        Item book = new Item(book_name, "Вы можете прочитать эту книгу.", book_sprite);
        dict_id_to_item[ind] = book;
        ind++;
    }

    public void TakeByName(string name)
    {
        foreach (int id in dict_id_to_item.Keys)
        {
            if (dict_id_to_item[id].name == name)
            {
                // Debug.Log("Book found");

                dict_id_to_item[id].count++;
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
            Debug.Log($"current id = {id}");
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
            SpawnBackpackIconPrefab(id);
        }
    }

    void SpawnBackpackIconPrefab(int id)
    {
        GameObject new_prefab = Instantiate(backpackIconPrefab, content_GO.transform);
        BackpackIconScript new_prefab_script = new_prefab.GetComponent<BackpackIconScript>();

        new_prefab_script.SetNewId(id);
    }

    public void OpenBackpackPanel()
    {
        gameObject.SetActive(true);
        UpdateBackpack();
    }

    public void CloseBackpackPanel()
    {
        gameObject.SetActive(false);
    }
}
