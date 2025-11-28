using NUnit.Framework;
using UnityEngine;
using static QuestsController;

public class InventoryStalker : MonoBehaviour
{
    public GameObject slot_prefab;

    //List<GameObject> pipi;

    void Start()
    {
        
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
        for (int i = 0; i < amount; i++)
        {
            GameObject new_prefab = Instantiate(slot_prefab, gameObject.transform);

        }
    }
}
