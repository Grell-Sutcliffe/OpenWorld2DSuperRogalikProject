using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackpackIconScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler
{
    BackPackController backpackController;

    public GameObject weaponIcon;

    public GameObject crossIcon;
    public TextMeshProUGUI timer_text;

    public TextMeshProUGUI starTMP;
    public Image elementImage;

    public Image item_image_TMP;
    public TextMeshProUGUI item_counter_TMP;
    public InventoryStalker inventory_stalker;
    int id;
    Sprite sprite;
    int count;

    public float longPressThreshold = 0.3f; // 0.1f слишком мало для человека

    private bool longPressHandled = false;
    private bool isPointerDown = false;
    private bool isDragging = false;

    private Coroutine longPressCoroutine;

    void Start()
    {
        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("MOUSE DOWN");
        isPointerDown = true;
        isDragging = false;
        longPressHandled = false;

        inventory_stalker.ChangeMouse(backpackController.dict_id_to_item[id]);

        longPressCoroutine = StartCoroutine(LongPressRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("MOUSE UP");

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

    public void SetNewId(int new_id)
    {
        id = new_id;

        if (backpackController == null)
            backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();

        sprite = backpackController.dict_id_to_item[id].sprite;
        count = backpackController.dict_id_to_item[id].amount;

        if (backpackController.dict_id_to_item[id].item_type == ItemType.Weapon)
        {
            ActivateWeaponIcon();
        }
        else
        {
            weaponIcon.SetActive(false);
        }
        crossIcon.SetActive(false);

        item_image_TMP.sprite = sprite;
        item_counter_TMP.text = count.ToString();
    }

    void ActivateWeaponIcon()
    {
        weaponIcon.SetActive(true);

        Weapon weapon = null;
        if (backpackController.dict_id_to_item[id] is Weapon temp)
        {
            weapon = temp;
        }
        if (weapon == null) return;

        elementImage.sprite = backpackController.GetElementByElementType(weapon.elementalDamage.element_type).sprite;
        starTMP.text = weapon.stars.ToString();
    }

    public void CloseIconForTime(int seconds)
    {
        Debug.Log($"CloseIconForTime seconds = {seconds}");

        if (seconds == 0)
        {
            OpenIcon();
            return;
        }

        crossIcon.SetActive(true);
        timer_text.text = seconds.ToString();

        //StartCoroutine(CountdownCoroutine(seconds));
    }

    public void OpenIcon()
    {
        crossIcon.SetActive(false);
    }

    /*
    private IEnumerator CountdownCoroutine(int seconds)
    {
        int remaining = seconds;

        while (remaining > 0)
        {
            timer_text.text = remaining.ToString();
            yield return new WaitForSeconds(1f);
            remaining--;
        }

        timer_text.text = "0";

        crossIcon.SetActive(false);
    }
    */
}
