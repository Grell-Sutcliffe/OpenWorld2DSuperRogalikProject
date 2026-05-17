using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamagable
{
    BackPackController backpackControler;

    [SerializeField] protected Weapon weapon;
    [SerializeField] protected SpriteRenderer handledElement;
    protected EffectStalker effectController;

    public ElementType elementType_of_creature;

    public bool isStopped = false;
    abstract public void TakeDamage(Damage dmg);
    protected virtual void Awake()
    {
        backpackControler = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        effectController = GetComponentInChildren<EffectStalker>();
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public void ChangeHandledElement(ElementType elementType = ElementType.None)
    {
        if (elementType == ElementType.None)
        {
            handledElement.sprite = null;
            return;
        }
        handledElement.sprite = backpackControler.GetElementByElementType(elementType).sprite;

    }
    
}
