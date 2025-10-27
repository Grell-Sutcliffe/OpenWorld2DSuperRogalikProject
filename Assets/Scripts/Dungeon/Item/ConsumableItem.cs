using UnityEngine;

public class ConsumableItem : Item
{
    public int amount = 1;

    public override void OnPickup(GameObject player)
    {
        Debug.Log($"�������� ��������� {itemName} x{amount}");
        Destroy(gameObject);
    }
}