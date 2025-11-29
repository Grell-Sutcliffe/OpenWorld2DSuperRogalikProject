using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    public InventoryStalker inventory_stalker;

    public Image slot_image;
    public TextMeshProUGUI slot_amount;
    public Item slot_item;
    
    public int slot_index;

    public float longPressThreshold = 0.3f; // 0.1f слишком мало для человека

    private bool longPressHandled = false;
    private bool isPointerDown = false;
    private bool isDragging = false;

    private Coroutine longPressCoroutine;

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

    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("SLOT MOUSE DOWN");
        isPointerDown = true;
        isDragging = false;
        longPressHandled = false;

        inventory_stalker.ChangeMouse(backpackController.dict_id_to_item[slot_item.id]);

        longPressCoroutine = StartCoroutine(LongPressRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("MOUSE UP");

        isPointerDown = false;

        if (longPressCoroutine != null)
        {
            StopCoroutine(longPressCoroutine);
            longPressCoroutine = null;
        }

        // Если не было долгого нажатия и не было перетаскивания — считаем кликом
        if (!longPressHandled && !isDragging)
        {
            ItemOnClick();
        }

        // Если был drag — кладём предмет в слот под мышкой
        if (longPressHandled || isDragging)
        {
            GameObject current_GO = eventData.pointerCurrentRaycast.gameObject;
            Debug.Log($"mouse on {current_GO}");
            if (current_GO != null)
            {
                SlotScript currentSlotScript = current_GO.GetComponent<SlotScript>();
                if (currentSlotScript != null)
                {
                    int current_slot_index = currentSlotScript.slot_index;
                    inventory_stalker.UpdateSlotItem(current_slot_index, backpackController.dict_id_to_item[id]);
                    // currentSlotScript.UpdateSlotItem(backpackController.dict_id_to_item[id]);
                    // backpackController.MoveItemToInventoryById(backpackController.dict_id_to_item[id].id);
                }
            }
        }

        if (inventory_stalker != null && inventory_stalker.mouse_stalker != null)
        {
            inventory_stalker.mouse_stalker.MakeDefault();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Ушли курсором с иконки — можно отменять "долгое нажатие", если надо
        if (isPointerDown && !longPressHandled)
        {
            if (longPressCoroutine != null)
            {
                StopCoroutine(longPressCoroutine);
                longPressCoroutine = null;
            }
        }
    }

    private IEnumerator LongPressRoutine()
    {
        yield return new WaitForSecondsRealtime(longPressThreshold);

        longPressHandled = true;
        longPressCoroutine = null;

        StartDrag();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Если хочешь начинать drag не только по long press, можно отметить это тут
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Тут можно обновлять позицию "сталкера" мыши, если нужно
    }

    private void ItemOnClick()
    {
        backpackController.UpdateShowerPanel(id);
    }

    private void StartDrag()
    {
        isDragging = true;
        // inventory_stalker.ChangeMouse(backpackController.dict_id_to_item[id]);
    }
    */
}
