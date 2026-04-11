using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamagable
{
    [SerializeField] protected Weapon weapon;
    protected EffectController effectController;
    ElementType elementType_of_creature;
    abstract public void TakeDamage(Damage dmg);
    protected virtual void Awake()
    {
        effectController = GetComponentInChildren<EffectController>();
    }
    public Weapon GetWeapon()
    {
        return weapon;
    }
}
