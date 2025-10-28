using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;
    public float damage;
    public float attackRange;
    public float attackRate;
    public WeaponType type;
    public GameObject weaponPrefab; // here will be Weapon Script
    public AttackEffect effect; // rewrite
}

public enum WeaponType { Melee, Ranged } // maybe do it just through range 
public enum AttackEffect { None, Fire, Ice, Poison } // just simple example
