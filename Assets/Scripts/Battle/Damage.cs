using UnityEngine;

public class Damage
{
    public float damage;
    public ElementalDamage elementalDamage;
    public bool isCrit;
    public Damage(float damage, ElementalDamage elementalDamage = null)
    {
        this.damage = damage;
        this.elementalDamage = elementalDamage == null ? new ElementalDamage() : elementalDamage;
    }
    
    public Damage(float damage, float elemental_damage, ElementType element_type, float elemental_mastery)
    {
        this.damage = damage;
        this.elementalDamage = new ElementalDamage(elemental_damage, element_type, elemental_mastery);
    }

    public float GetDamage()
    {
        return damage;
    }

    public ElementType GetElement()
    {
        return elementalDamage.element_type;
    }
}

public class ElementalDamage
{
    public float elemental_damage;
    public ElementType element_type;
    public float elemental_mastery;

    public ElementalDamage()
    {
        elemental_damage = 0f;
        element_type = ElementType.None;
        elemental_mastery = 0f;
    }

    public ElementalDamage(float demage, ElementType element_type, float elemental_mastery)
    {
        this.elemental_damage = demage;
        this.element_type = element_type;
        this.elemental_mastery = elemental_mastery;
    }
}
