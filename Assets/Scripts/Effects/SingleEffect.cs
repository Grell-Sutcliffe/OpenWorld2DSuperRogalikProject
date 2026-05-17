using Unity.VisualScripting;
using UnityEngine;

public class SingleEffect : Effect
{

    void Awake()
    {
        hitCollider = GetComponent<Collider2D>();

    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                if (effectType == TypeOfEffect.DMG)
                {
                    //player.TakeDamage(powerOfEffect);
                }
                else
                {
                    //player.ModifySpeed(powerOfEffect, 1);
                }
            }
            if (isDestroyAfterWork) Destroy(this.gameObject);
        }
    }



}
