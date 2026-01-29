using System;
using UnityEngine;

public class Weapon : Item
{
    public int stars;
    public int damage;
    //public ElementType element;
    public ElementalDamage elementalDamage;
    public WeaponType weapon_type;

    int max_level;
    public int current_level;

    public float crit_chance;
    public float crit_dmg;

    public float elemental_mastery;
    public float cooldown;

    WeaponSO data;

    public Weapon(WeaponSO data)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.weapon_name;
        this.description = data.description;
        this.stars = data.stars;
        this.damage = data.damage;
        this.crit_chance = data.crit_chance;
        this.crit_dmg = data.crit_dmg;
        this.elementalDamage = new ElementalDamage(data.elemental_damage, data.element_type, data.elemental_mastery);
        //this.element = data.element;
        this.elemental_mastery = data.elemental_mastery;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;

        this.item_type = ItemType.Weapon;

        this.current_level = 1;
        this.cooldown = data.cooldown;

        this.id = -100;

        this.amount = 0;
    }

    public Weapon(WeaponSO data, int id)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.weapon_name;
        this.description = data.description;
        this.stars = data.stars;
        this.damage = data.damage;
        this.crit_chance = data.crit_chance;
        this.crit_dmg = data.crit_dmg;
        this.elementalDamage = new ElementalDamage(data.elemental_damage, data.element_type, data.elemental_mastery);
        //this.element = data.element;
        this.elemental_mastery = data.elemental_mastery;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;

        this.item_type = ItemType.Weapon;

        this.current_level = 1;
        this.cooldown = data.cooldown;

        this.id = id;

        this.amount = 0;
    }

    public Weapon(WeaponSO data, int id, int amount)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.weapon_name;
        this.description = data.description;
        this.stars = data.stars;
        this.damage = data.damage;
        this.crit_chance = data.crit_chance;
        this.crit_dmg = data.crit_dmg;
        this.elementalDamage = new ElementalDamage(data.elemental_damage, data.element_type, data.elemental_mastery);
        //this.element = data.element;
        this.elemental_mastery = data.elemental_mastery;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;

        this.item_type = ItemType.Weapon;

        this.current_level = 1;
        this.cooldown = data.cooldown;

        this.id = id;

        this.amount = amount;
    }
}