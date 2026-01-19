using UnityEngine;

public class SingleDamage : MonoBehaviour
{

    [SerializeField] float dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(dmg);
                //Destroy(gameObject);
            }
        }
    }
}
