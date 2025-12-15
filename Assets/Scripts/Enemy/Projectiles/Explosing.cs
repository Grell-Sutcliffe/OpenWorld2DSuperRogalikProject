using UnityEngine;

public class Explosing : TeEmPe
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider2D collider;

    void Start()
    {
        Destroy(gameObject, 3);
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(dmg);
            }


        }
        if (isDestroyOnEnter) Destroy(gameObject);

    }
}
