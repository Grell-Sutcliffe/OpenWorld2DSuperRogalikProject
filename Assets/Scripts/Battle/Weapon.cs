using System;
using UnityEngine;

public class Weapon : Item
{
    public int stars;
    public int damage;
    //public ElementType element;
    public ElementalDamage elementalDamage;
    public WeaponType weapon_type;

    public int max_level;
    public int current_level;

    public float crit_chance;
    public float crit_dmg;

    public float cooldown;

    WeaponSO data;

    public float upgrade_percent = 1.05f;

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
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;

        this.item_type = ItemType.Weapon;

        this.current_level = 1;
        this.cooldown = data.cooldown;

        this.id = id;

        this.amount = amount;
    }

    public void WeaponUpgrade()
    {
        this.damage = RoundToMax(this.damage * upgrade_percent);
        this.crit_chance *= upgrade_percent;
        this.crit_dmg *= upgrade_percent;
        this.elementalDamage.elemental_damage *= upgrade_percent;
        this.elementalDamage.elemental_mastery *= upgrade_percent;

        this.current_level++;
    }

    int RoundToMax(float number)
    {
        return (int)(number * 10 % 10 > 0 ? number + 1 : number);
    }
}