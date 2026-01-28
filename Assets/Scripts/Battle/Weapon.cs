using System;
using UnityEngine;

public class Weapon : Item
{
    public int damage;
    public ElementType element;
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
        this.sprite = data.sprite;
        this.name = data.name;
        this.description = data.description;
        this.stars = data.stars;
        this.damage = data.damage;
        this.crit_chance = data.crit_chance;
        this.crit_dmg = data.crit_dmg;
        this.element = data.element;
        this.elemental_mastery = data.elemental_mastery;
        this.max_level = data.max_level;
        this.weapon_type = data.weaponType;

        this.current_level = 1;
        this.cooldown = data.cooldown;
    }

    public Weapon(Sprite sprite, string name, string description, int stars, int damage, float crit_chance, float crit_dmg, WeaponType type, ElementType element, float elemental_mastery, int max_level)
    {
        this.sprite = sprite;
        this.name = name;
        this.description = description;
        this.stars = stars;
        this.weapon_type = type;
        this.damage = damage;
        this.crit_chance = crit_chance;
        this.crit_dmg = crit_dmg;
        this.element = element;
        this.elemental_mastery = elemental_mastery;
        this.max_level = max_level;

        this.current_level = 1;
    }
}