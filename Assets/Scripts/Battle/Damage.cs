using UnityEngine;

public class Damage
{
    public float damage;
    Element element;
    public bool isCrit;
    public Damage(float damage_, bool isCrit = false, Element element_ = Element.None)
    {
        damage = damage_;
        element = element_;
        this.isCrit = isCrit;
    }

    public float GetDamage()
    {
        return damage;
    }

    public Element GetElement()
    {
        return element;
    }
}