using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static QuestsController;

public class InventoryStalker : MonoBehaviour
{
    public GameObject slot_prefab;

    List<SlotScript> slotScripts;

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
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void FillInventory(int amount = 10)
    {
        slotScripts = new List<SlotScript>();

        for (int i = 0; i < amount; i++)
        {
            GameObject new_prefab = Instantiate(slot_prefab, gameObject.transform);

            slotScripts.Add(new_prefab.GetComponent<SlotScript>());
        }
    }
}
