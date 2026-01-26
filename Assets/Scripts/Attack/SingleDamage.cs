using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : MonoBehaviour
{

    [SerializeField] float dmg;
    [SerializeField] EnemyAbstract enemy;
    HashSet<int> hitIds;
    bool wasHitted = false;
    private void Awake()
    {
        if (transform != null && transform.parent != null && transform.parent.parent != null)
            enemy = transform.parent.parent.GetComponent<EnemyAbstract>();
        hitIds = new HashSet<int>();
    }
    private void OnTriggerEnter2D(Collider2D collision)  // add setitititititititititiititi
    {
        int id = collision.GetInstanceID();
        if (hitIds.Contains(id)) return;
        hitIds.Add(id);
        var dmgable = collision.GetComponentInParent<IDamagable>();
        if (dmgable != null){
            dmgable.TakeDamage(dmg, gameObject);
            Debug.Log($"Single Damage on {collision} and {collision.gameObject.name}");
            return;
        }
        dmgable = collision.GetComponent<IDamagable>();
        //Debug.Log(dmgable);
        if (dmgable != null){
            dmgable.TakeDamage(dmg, gameObject);
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
        if (enemy != null){
            enemy.UnActivePivot(); // лучше искать енеми в родители
            enemy.StartDelay();
        }

    }

}
