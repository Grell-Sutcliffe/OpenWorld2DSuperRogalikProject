using UnityEngine;

public class AgrZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] EnemyAbstract owner;

    private void OnTriggerEnter2D(Collider2D collision)  // не ентер а просто
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player!");
            var player = collision.GetComponent<Player>();

            owner.OnTrigger(player);

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            owner.OffTrigger();
        }
    }
}
