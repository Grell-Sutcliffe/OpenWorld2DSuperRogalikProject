using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon")]
public class WeaponSO : ScriptableObject
{
    public Sprite sprite;
    public string weapon_name;
    public string description;
    public int stars;

    public float range;
    public float cooldown;

    public int damage;

    public float elemental_damage;
    public ElementType element_type;
    public float elemental_mastery;

    public float crit_chance;
    public float crit_dmg;
    public int max_level;
    public int current_level;

    public GameObject projectilePrefab;
    public AttackType attackType;
    public WeaponType weaponType;
}


public enum AttackType
{
    Range = 0,
    Melee = 1,
}

public enum WeaponType
{
    Sword = 1,
    Melee = 1,
    Range = 2,
}
