using System;
using UnityEngine;

public class Weapon  
{
    Sprite sprite;
    string nameW;
    string description;
    int stars;
    public int damage;
    public Element element;
    int max_level;
    int current_level;

    public float cooldown;

    WeaponSO data;
    public Weapon(WeaponSO data)
    {
        this.sprite = data.sprite;
        this.nameW = data.nameW;
        this.description = data.description;
        this.stars = data.stars;
        this.damage = data.damage;
        this.element = data.element;
        this.max_level = data.max_level;

        this.current_level = 1;
        this.cooldown = data.cooldown;
    }


    public Weapon(Sprite sprite, string name, string description, int stars, int damage, Element element, int max_level)
    {
        this.sprite = sprite;
        this.nameW = name;
        this.description = description;
        this.stars = stars;
        this.damage = damage;
        this.element = element;
        this.max_level = max_level;

        this.current_level = 1;
    }
}