using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : MonoBehaviour
{
        // ЕСЛИ ПОЧЕМУ ТО НЕ ПРОХОДИТ, СТАВЬ РИДЖИДБОДИ!!!!!!!!!ЫЫЫЫЫЫЫЫ
    [SerializeField] float dmg;
    [SerializeField] IAttacker owner;
    [SerializeField] private GameObject ownerObject;
    HashSet<int> hitIds = new HashSet<int>();
    bool wasHitted = false;

    void OnEnable()
    {
        var anim = GetComponent<Animator>();
        if (owner is Player p)
        {   var i = 0;
            if (p.spriteRender.flipX == false) i = 1;
            else i = 2;
            anim.SetInteger("attackType", i);

        }
    }

    private void Awake()
    {
        // Если не назначили в Inspector — ищем вверх по иерархии
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
            Debug.LogError($"SingleDamage: не найден IAttacker для {gameObject.name}!", this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)  // add setitititititititititiititi
    {
        //Debug.Log($"Single Damage on {collision} and {collision.gameObject.name} {transform.parent.parent.name}");
        if (collision.gameObject == owner.owner) return;

        int id = collision.GetInstanceID();
        if (hitIds.Contains(id)) return;
        hitIds.Add(id);


        var damageable = FindDamageable(collision);

        Debug.Log(collision, collision.gameObject);
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
    public void ChangeHit()
    {
        hitIds.Clear();
        if (owner != null){
            owner.UnActivePivot(); 
            owner.StartDelay();
        }

    }

}
