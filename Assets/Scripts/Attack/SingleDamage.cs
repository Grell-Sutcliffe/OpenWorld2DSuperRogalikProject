using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : MonoBehaviour
{
        // ≈—À» œŒ◊≈Ã” “Œ Õ≈ œ–Œ’Œƒ»“, —“¿¬‹ –»ƒ∆»ƒ¡Œƒ»!!!!!!!!!€€€€€€€€
    [SerializeField] float dmg;
    [SerializeField] IAttacker owner;
    [SerializeField] private GameObject ownerObject;
    HashSet<int> hitIds = new HashSet<int>();
    bool wasHitted = false;

    void OnEnable()
    {
        ColliderDisnable();
        var anim = GetComponent<Animator>();
        if (owner is Player p)
        {
            anim.SetInteger("aType", Random.Range(0, 2));

        }
    }

    private void Awake()
    {
        if (ownerObject != null)
        {
            owner = ownerObject.GetComponent<IAttacker>();
        }
        else
        {
            owner = GetComponentInParent<IAttacker>();
        }

        if (owner == null)
        {
            Debug.LogError($"SingleDamage: ÌÂ Ì‡È‰ÂÌ IAttacker ‰Îˇ {gameObject.name}!", this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)  
    {
        //Debug.Log($"Single Damage on {collision} and {collision.gameObject.name} {transform.parent.parent.name}");
        if (collision.gameObject == owner.owner) return;

        int id = collision.GetInstanceID();
        if (hitIds.Contains(id)) return;
        hitIds.Add(id);


        var damageable = FindDamageable(collision);

        //Debug.Log(collision, collision.gameObject);
        damageable.TakeDamage(owner.currentDmg);
    }

    private IDamagable FindDamageable(Collider2D collision)
    {
        var result = collision.GetComponent<IDamagable>();
        if (result != null) return result;

        return collision.GetComponentInParent<IDamagable>();
    }
    public void ChangeCollider()
    {
        Collider2D col = transform.GetComponentInChildren<Collider2D>();
        if (col != null)
        {
            col.enabled = !col.enabled;

        }

    }

    public void ColliderEnable()
    {
        Collider2D col = transform.GetComponentInChildren<Collider2D>();
        if (col != null)
        {
            col.enabled = true;

        }

    }
    public void ColliderDisnable()
    {
        Collider2D col = transform.GetComponentInChildren<Collider2D>();
        if (col != null)
        {
            col.enabled = false;

        }

    }

    public void ChangeHit()
    {
        hitIds.Clear();
        if (owner != null){
            owner.UnActivePivot(); 
            owner.StartDelay();
        }

    }

}
