using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanelScript : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;

    public GameObject craftingLeftPanel_GO;

    public Image image;
    public TextMeshProUGUI titleTMP;

    public GameObject weaponInfo;
    public Image element_image;
    public TextMeshProUGUI starsTMP;

    public GameObject content_of_craftingIcons_GO;
    public GameObject content_of_craftingSlots_GO;

    public GameObject craftingIconPrefab;
    public GameObject craftingSlotPrefab;

    RectTransform content_rect_transform;

    public int item_height = 350;
    public int space_between_items = 15;

    Item current_crafting_item;

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        content_rect_transform = content_of_craftingIcons_GO.GetComponent<RectTransform>();
    }

    public void OpenPanel(ItemType itemType = ItemType.Food)
    {
        gameObject.SetActive(true);

        UpdateCraftType(itemType);

        craftingLeftPanel_GO.SetActive(false);
    }

    public void SetCraftType_Food()
    {
        UpdateCraftType(ItemType.Food);
    }

    public void SetCraftType_Drink()
    {
        UpdateCraftType(ItemType.Drink);
    }

    public void SetCraftType_Weapon()
    {
        UpdateCraftType(ItemType.Weapon);
    }

    public void SetCraftType_Material()
    {
        UpdateCraftType(ItemType.Materials);
    }

    public void UpdateCraftType(ItemType itemType)
    {
        foreach (Transform child in content_of_craftingIcons_GO.transform)
        {
            Destroy(child.gameObject);
        }

        int item_counter = 0;

        foreach (int id in backpackController.dict_id_to_item.Keys)
        {
            if (backpackController.dict_id_to_item[id].item_type == itemType && backpackController.dict_id_to_item[id].is_craftable)
            {
                item_counter++;
            }
        }

        int new_height = item_counter * item_height + (item_counter - 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (int id in backpackController.dict_id_to_item.Keys)
        {
            if (backpackController.dict_id_to_item[id].item_type == itemType && backpackController.dict_id_to_item[id].is_craftable)
            {
                SpawnCraftingIcon(backpackController.dict_id_to_item[id]);
            }
        }
    }

    public void ChangeCraftingLeftPanel(string item_name)
    {
        current_crafting_item = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[item_name]];

        craftingLeftPanel_GO.SetActive(true);

        weaponInfo.SetActive(false);

        image.sprite = current_crafting_item.sprite;
        titleTMP.text = current_crafting_item.item_name;

        if (current_crafting_item is Weapon weapon)
        {
            weaponInfo.SetActive(true);

            element_image.sprite = backpackController.GetElementByElementType(weapon.element_type).sprite;
            starsTMP.text = weapon.stars.ToString();
        }

        SpawnCraftingSlots();
    }

    void SpawnCraftingSlots()
    {
        foreach (Transform child in content_of_craftingSlots_GO.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < current_crafting_item.item_amounts_for_craft.Count; i++)
        {
            GameObject new_prefab = Instantiate(craftingSlotPrefab, content_of_craftingSlots_GO.transform);
            CraftingSlotScript new_prefab_script = new_prefab.GetComponent<CraftingSlotScript>();

            new_prefab_script.SetSlot(backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[current_crafting_item.item_names_for_craft[i]]], current_crafting_item.item_amounts_for_craft[i]);
        }
    }

    void SpawnCraftingIcon(Item item)
    {
        GameObject new_prefab = Instantiate(craftingIconPrefab, content_of_craftingIcons_GO.transform);
        CraftingIconScript new_prefab_script = new_prefab.GetComponent<CraftingIconScript>();

        new_prefab_script.SetSlot(item);
    }
}
