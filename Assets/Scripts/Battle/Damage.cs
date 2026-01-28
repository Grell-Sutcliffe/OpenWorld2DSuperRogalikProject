using UnityEngine;

public class Damage
{
    public float damage;
    ElementType element;

    public Damage(float damage_, ElementType element_ = ElementType.None)
    {
        damage = damage_;
        element = element_;
    }

    public float GetDamage()
    {
        return damage;
    }

    public ElementType GetElement()
    {
        return element;
    }
}