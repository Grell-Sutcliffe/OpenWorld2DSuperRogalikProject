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
        enemy = transform.parent?.parent?.GetComponent<EnemyAbstract>();
        hitIds = new HashSet<int>();

    }
    private void OnTriggerEnter2D(Collider2D collision)  // add setitititititititititiititi
    {
        int id = collision.GetInstanceID();
        if (hitIds.Contains(id)) return;
        hitIds.Add(id);
        Debug.Log("IDDDD");
        Debug.Log(id);
        Debug.Log(collision);
        var dmgable = collision.GetComponentInParent<IDamagable>();
        Debug.Log(dmgable);
        if (dmgable != null)
            dmgable.TakeDamage(dmg, gameObject);
        dmgable = collision.GetComponent<IDamagable>();
        Debug.Log(dmgable);
        if (dmgable != null)
            dmgable.TakeDamage(dmg, gameObject);

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

    public void ChangeHit()
    {
        hitIds.Clear();
        if (enemy != null)
            enemy.SingleScript(); // лучше искать енеми в родители
    }

}
