using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public Image slot_image;
    public TextMeshProUGUI slot_amount;
    public Item slot_item;

    void Start()
    {
        slot_image.sprite = null;
        slot_amount.text = string.Empty;
    }

    public void UpdateSlot()
    {

    }
}
