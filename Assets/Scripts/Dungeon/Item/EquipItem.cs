using UnityEngine;

public class EquipItem : Item
{
    public EquipData equipData;
    public override void OnPickup(GameObject player)
    {
        Debug.Log($"��������� ����������: {equipData.equipmentName}");

        //PlayerEquipment playerEquipment = player.GetComponent<PlayerEquipment>();
        //if (playerEquipment != null)
        //{
        //    playerEquipment.Equip(equipData);
        //}

        Destroy(gameObject);
    }
}

