using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamagable
{
    [SerializeField] protected Weapon weapon;
    protected EffectController effectController;

    public ElementType elementType_of_creature;

    // переменные для EffectController на создании
    // не используется пока что в плеере
    protected bool isStopped = false;
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
