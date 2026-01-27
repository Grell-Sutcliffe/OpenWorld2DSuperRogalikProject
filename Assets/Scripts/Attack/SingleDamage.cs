using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : MonoBehaviour
{

    [SerializeField] float dmg;
    [SerializeField] IAttacker owner;
    HashSet<int> hitIds;
    bool wasHitted = false;
    private void Awake()
    {
        if (transform != null && transform.parent != null && transform.parent.parent != null)
            owner = transform.parent.parent.GetComponent<IAttacker>();
        if (owner == null)
            owner = transform.parent.GetComponent<IAttacker>();
        hitIds = new HashSet<int>();
    }
    private void OnTriggerEnter2D(Collider2D collision)  // add setitititititititititiititi
    {
        int id = collision.GetInstanceID();
        if (hitIds.Contains(id)) return;
        hitIds.Add(id);
        var dmgable = collision.GetComponentInParent<IDamagable>();                         // »«ћ≈Ќ»“№!
        if (dmgable != null){
            dmgable.TakeDamage(owner.currentDmg);
            Debug.Log($"Single Damage on {collision} and {collision.gameObject.name}");
            return;
        }
        dmgable = collision.GetComponent<IDamagable>();
        //Debug.Log(dmgable);
        if (dmgable != null){
            dmgable.TakeDamage(owner.currentDmg);
            Debug.Log($"Single Damage on {collision} and {collision.gameObject.name}");

        }

        /*
        if (collision.CompareTag("Player"))
        {

            //Debug.Log(222);
            var player = collision.GetComponent<Player>();
            //Debug.Log(player);

            if (player != null)
            {
                
                if (!wasHitted){
                    player.TakeDamage(dmg);
                    wasHitted = true;

                }


        //Destroy(gameObject);
        */

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
            owner.UnActivePivot(); // лучше искать енеми в родители
            owner.StartDelay();
        }

    }

}
