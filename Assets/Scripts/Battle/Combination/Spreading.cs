using System.Collections.Generic;
using UnityEngine;

public class Spreading : MonoBehaviour
{

    CircleCollider2D circleCollider;
    Damage damage;
    private HashSet<IDamagable> hitIds = new HashSet<IDamagable>();
    public void Init(Damage dmg, float radius = 3)
    {
        circleCollider = GetComponent<CircleCollider2D>();
        damage = dmg;
        circleCollider.radius = radius;
        Destroy(gameObject, 1f); // лучше под конец анимации
        Debug.Log($"BOOM");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Spread effect");
        //if (collision.gameObject == owner.owner) return;

        var damageable = collision.GetComponent<IDamagable>();
        if (damageable == null)
            damageable = collision.GetComponentInParent<IDamagable>();

        if (hitIds.Contains(damageable)) return;

        hitIds.Add(damageable);


        damageable.TakeDamage(damage);
    }
    public void Remove()
    {
        Destroy(gameObject);
    }
}
