using UnityEngine;

public class Damage
{
    public float damage;
    Element element;

    public Damage(float damage_, Element element_ = Element.None)
    {
        damage = damage_;
        element = element_;
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