using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public Image slot_image;
    public TextMeshProUGUI slot_amount;
    public Item slot_item;
    
    public int slot_index;

    void Start()
    {
        EmptySlot();
    }

    public void EmptySlot()
    {
        slot_image.sprite = null;
        slot_amount.text = string.Empty;
    }

    public void UpdateSlotItem(Item item)
    {
        slot_item = item;
        UpdateSlot();
    }

    void UpdateSlot()
    {
        slot_amount.text = slot_item.count.ToString();
        slot_image.sprite = slot_item.sprite;
    }
}
