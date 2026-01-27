using UnityEngine;

public class DamageZoneS : MonoBehaviour, IAttacker
{
    [SerializeField] float dmg;
    public GameObject owner => gameObject;
    protected float current_dmg;
    public Damage currentDmg => new Damage(current_dmg);

    private void Start()
    {
        DealDamage();
    }
    public void DealDamage()
    {
        this.current_dmg = dmg;
    }
 
    public void StartDelay()
    {
        
    }
    public virtual void UnActivePivot() { }

}
