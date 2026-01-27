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
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(new Damage(dmg.damage));
                Destroy(gameObject);
            }
        }
    }
}
