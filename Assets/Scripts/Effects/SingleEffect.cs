using Unity.VisualScripting;
using UnityEngine;

public class SingleEffect : Effect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        hitCollider = GetComponent<Collider2D>();

    }

    void Start()
    {

    }

    // Update is called once per frame
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
