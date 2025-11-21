using UnityEngine;

public class EquipItem : Item
{
    public EquipData equipData;
    public virtual void OnPickup(GameObject player)
    {
        Debug.Log($"Подобрана экипировка: {equipData.equipmentName}");

        //PlayerEquipment playerEquipment = player.GetComponent<PlayerEquipment>();
        //if (playerEquipment != null)
        //{
        //    playerEquipment.Equip(equipData);
        //}

        Destroy(gameObject);
    }
}

