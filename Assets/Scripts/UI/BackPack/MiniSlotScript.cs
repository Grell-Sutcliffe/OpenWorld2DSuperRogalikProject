using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniSlotScript : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;
    public InventoryStalker inventoryStalker;

    public Image slot_image;
    public TextMeshProUGUI slot_amount;
    public Item slot_item;

    public GameObject cross_image;
    public TextMeshProUGUI timer_text;

    public int slot_index;

    bool is_clickable = true;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackPanel")?.GetComponent<BackPackController>();
        //inventoryStalker = GameObject.Find("Inventory").GetComponent<InventoryStalker>();
        inventoryStalker = gameObject.GetComponentInParent<InventoryStalker>();

        EmptySlot();
    }

    public void EmptySlot()
    {
        cross_image.SetActive(false);

        slot_item = null;
        slot_image.sprite = null;
        slot_amount.text = string.Empty;
    }

    public void UpdateSlotItem(Item item)
    {
        slot_item = item;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        cross_image.SetActive(false);
        is_clickable = true;

        if (slot_item == null || slot_item.amount <= 0)
        {
            EmptySlot();
            return;
        }

        slot_amount.text = slot_item.amount.ToString();
        slot_image.sprite = slot_item.sprite;

        if (slot_item is UsableItem usable_item)
        {
            if (mainController.dict_useType_to_seconds_left[usable_item.useEffect.useType] > 0)
            {
                cross_image.SetActive(true);
                is_clickable = false;
            }
        }
    }

    public void ItemOnClick()
    {
        if (backpackController == null) backpackController = GameObject.Find("BackpackPanel")?.GetComponent<BackPackController>();
        if (!is_clickable) return;

        if (slot_item is UsableItem usable_item)
        {
            Debug.Log("NOM-NOM");
            backpackController.UseItem(usable_item);
        }

        UpdateSlot();
    }

    public void CloseSlotForTime(int seconds)
    {
        //Debug.Log($"CloseIconForTime seconds = {seconds}");

        if (seconds == 0)
        {
            OpenIcon();
            return;
        }

        cross_image.SetActive(true);
        timer_text.text = seconds.ToString();

        //StartCoroutine(CountdownCoroutine(seconds));
    }

    public void OpenIcon()
    {
        cross_image.SetActive(false);
    }
}
