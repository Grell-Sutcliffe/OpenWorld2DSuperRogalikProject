using Unity.VisualScripting;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] float lifeLong;
    
    //Collider2D col;

    //Effect
    [SerializeField] float speed;
    [SerializeField] Vector3 dir;
    public Damage dmg;
    Rigidbody2D rb;

    private void Awake()
    {
        //col = GetComponent<Collider2D>();
        Destroy(gameObject, lifeLong);
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetDir(Vector3 newDir)
    {
        dir = newDir.normalized;
        rb.linearVelocity = dir * speed;
    }

    // Update is called once per frame
    private IDamagable FindDamageable(Collider2D collision)
    {
        var result = collision.GetComponent<IDamagable>();
        if (result != null) return result;

        return collision.GetComponentInParent<IDamagable>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = FindDamageable(collision);

        Debug.Log(collision, collision.gameObject);
        damageable.TakeDamage(dmg);
    }
}
