using UnityEngine;

public class WeaponItem : Item
{
    public WeaponData weaponData;
    // add characteristics 
    public override void OnPickup(GameObject player)
    {
        Debug.Log($"Подобрано оружие {itemName}");
        // InventorySystem.Instance.AddWeapon(this); if player dont have any weapon - equip it!
        Destroy(gameObject);
    }
}