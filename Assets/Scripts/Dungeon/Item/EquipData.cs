using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Items/Equipment Data")]
public class EquipData : ScriptableObject
{
    public string equipmentName;
    public EquipmentSlot slot;
    public Sprite icon;
    public GameObject visualPrefab; 
    public int defenseBonus;
    // rewrite 
}
public enum EquipmentSlot
{
    Head,
    Body,
    Legs
}