using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon")]
public class WeaponSO : ScriptableObject
{
    public Sprite sprite;
    public string nameW;
    public string description;
    public int stars; // ?

    public float range; // ?
    public float cooldown;

    public int damage;
    public Element element; // possible elements?
    public int max_level;
    public int current_level;


    public GameObject projectilePrefab;
    public AttackType attackType;
}


public enum AttackType
{
    Range = 0,
    Melee = 1,
}