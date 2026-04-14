using UnityEngine;

public class Damage
{
    public float physical_dmg;
    public float elemental_dmg;
    public ElementType element_type;
    public bool isPhysicalCrit;
    public bool isElementalCrit;
    public bool isCombinated;
    public Damage(float physical_dmg, float elemental_dmg, ElementType element_type, bool isPhysicalCrit = false, bool isElementalCrit = false, bool isCombinated = true)
    {
        this.physical_dmg = physical_dmg;
        this.elemental_dmg = elemental_dmg;
        this.element_type = element_type;
        this.isPhysicalCrit = isPhysicalCrit;
        this.isElementalCrit = isElementalCrit;
        this.isCombinated = isCombinated;
    }

    public Damage(Damage damage)
    {
        this.physical_dmg = damage.physical_dmg;
        this.elemental_dmg = damage.elemental_dmg;
        this.element_type = damage.element_type;
        this.isPhysicalCrit = damage.isPhysicalCrit;
        this.isElementalCrit = damage.isElementalCrit;
        this.isCombinated = damage.isCombinated;
    }
}

public class Stats
{
    public float health = 0f;
    public float physical_attack = 0f;
    public float elemental_attack = 0f;
    public float crit_chance = 0f;
    public float crit_dmg = 0f;
    public float elemental_mastery = 0f;
    //public float defence = 0f;

    public float physical_dmg;
    public float elemental_dmg;

    public bool is_physical_crit;
    public bool is_elemental_crit;

    public Stats(Stats stats)
    {
        this.health = stats.health;
        this.physical_attack = stats.physical_attack;
        this.elemental_attack = stats.elemental_attack;
        this.crit_chance = stats.crit_chance;
        this.crit_dmg = stats.crit_dmg;
        this.elemental_mastery = stats.elemental_mastery;
        //this.defence = stats.defence;
    }

    public Stats(Stats stats1, Stats stats2)
    {
        this.health = stats1.health + stats2.health;
        this.physical_attack = stats1.physical_attack + stats2.physical_attack;
        this.elemental_attack = stats1.elemental_attack + stats2.elemental_attack;
        this.crit_chance = stats1.crit_chance + stats2.crit_chance;
        this.crit_dmg = stats1.crit_dmg + stats2.crit_dmg;
        this.elemental_mastery = stats1.elemental_mastery + stats2.elemental_mastery;
        //this.defence = stats1.defence + stats2.defence;
    }

    public Stats(float health = 0, float physical_attack = 0, float elemental_attack = 0, float crit_chance = 0, float crit_dmg = 0, float defence = 0, float elementsl_mastery = 0)
    {
        this.health = health;
        this.physical_attack = physical_attack;
        this.elemental_attack = elemental_attack;
        this.crit_chance = crit_chance;
        this.crit_dmg = crit_dmg;
        this.elemental_mastery = elementsl_mastery;
        //this.defence = defence;
    }
}
