using System;
using UnityEngine;

public class Weapon : Item
{
    public int stars;

    public WeaponType weapon_type;
    public ElementType element_type;

    public int max_level;
    public int current_level;

    public Stats stats = new Stats();

    public float cooldown;

    public WeaponSO data;

    public float upgrade_percent = 1.05f;
    public Weapon()
    {
        
    }

    public Weapon(WeaponSO data)
    {
        this.data = data;

        this.sprite = data.sprite;
        this.item_name = data.weapon_name;
        this.description = data.description;
        this.stars = data.stars;
        this.stats.physical_attack = data.physical_attack;
        this.stats.elemental_attack = data.elemental_attack;
        this.stats.crit_chance = data.crit_chance;
        this.stats.crit_dmg = data.crit_dmg;
        this.stats.elemental_mastery = data.elemental_mastery;
        //this.element = data.element;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;
        this.element_type = data.element_type;

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
        this.stats.physical_attack = data.physical_attack;
        this.stats.elemental_attack = data.elemental_attack;
        this.stats.crit_chance = data.crit_chance;
        this.stats.crit_dmg = data.crit_dmg;
        this.stats.elemental_mastery = data.elemental_mastery;
        //this.element = data.element;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;
        this.element_type = data.element_type;

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
        this.stats.physical_attack = data.physical_attack;
        this.stats.elemental_attack = data.elemental_attack;
        this.stats.crit_chance = data.crit_chance;
        this.stats.crit_dmg = data.crit_dmg;
        this.stats.elemental_mastery = data.elemental_mastery;
        //this.element = data.element;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;
        this.element_type = data.element_type;

        this.item_type = ItemType.Weapon;

        this.current_level = 1;
        this.cooldown = data.cooldown;

        this.id = id;

        this.amount = amount;
    }

    public void WeaponUpgrade()
    {
        this.stats.physical_attack = RoundToMax(this.stats.physical_attack * upgrade_percent);
        this.stats.elemental_attack = RoundToMax(this.stats.elemental_attack * upgrade_percent);
        this.stats.crit_chance *= upgrade_percent;
        this.stats.crit_dmg *= upgrade_percent;
        this.stats.elemental_mastery *= upgrade_percent;

        this.current_level++;

        BackPackController.Instance.SaveInventory();
    }

    int RoundToMax(float number)
    {
        return (int)(number * 10 % 10 > 0 ? number + 1 : number);
    }
}