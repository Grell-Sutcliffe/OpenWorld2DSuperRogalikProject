using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static QuestsController;

public class InventoryStalker : MonoBehaviour
{
    public GameObject inventoryPlayerPanel;
    public GameObject slot_prefab;
    public MouseStalker mouse_stalker;

    List<int> slots_id;
    List<SlotScript> slotScripts_backpackPanel;
    List<SlotScript> slotScripts_playerPanel;

    public void ChangeMouse(Item item)
    {
        mouse_stalker.ChangeImage(item.sprite);
    }
    void Start()
    {
        SetInventory();
    }

    void SetInventory()
    {
        ClearInventory();
        FillInventory();
    }

    void ClearInventory()
    {
        // this inventory
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // small inventory
        foreach (Transform child in inventoryPlayerPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateSlotItem(int new_index, Item new_item)
    {
        Debug.Log($"slots_id.Count = {slots_id.Count}, new_item.id = {new_item.id}");
        for (int i = 0; i < slots_id.Count; i++)
        {
            if (slots_id[i] == new_item.id) // если в инвентаре уже лежит
            {
                Debug.Log("В инвентаре уже есть это");
                slots_id[i] = -1000;
                slotScripts_backpackPanel[i].EmptySlot();
                slotScripts_playerPanel[i].EmptySlot();
            }
        }

        slots_id[new_index] = new_item.id;
        slotScripts_backpackPanel[new_index].UpdateSlotItem(new_item);
        slotScripts_playerPanel[new_index].UpdateSlotItem(new_item);
    }

    public void EmptySlotItem(int new_index)
    {
        slotScripts_backpackPanel[new_index].EmptySlot();
        slotScripts_playerPanel[new_index].EmptySlot();
    }

    void FillInventory(int amount = 10)
    {
        slots_id = new List<int>();
        slotScripts_backpackPanel = new List<SlotScript>();
        slotScripts_playerPanel = new List<SlotScript>();

        for (int i = 0; i < amount; i++)
        {
            slots_id.Add(-1000);

            GameObject new_prefab1 = Instantiate(slot_prefab, gameObject.transform);
            SlotScript new_slotScript1 = new_prefab1.GetComponent<SlotScript>();
            new_slotScript1.slot_index = i;
            slotScripts_backpackPanel.Add(new_slotScript1);

            GameObject new_prefab2 = Instantiate(slot_prefab, inventoryPlayerPanel.transform);
            SlotScript new_slotScript2 = new_prefab2.GetComponent<SlotScript>();
            new_slotScript2.slot_index = i;
            slotScripts_playerPanel.Add(new_slotScript2);
        }
    }
}
