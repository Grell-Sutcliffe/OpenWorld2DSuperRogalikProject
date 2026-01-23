using UnityEngine;

public class SingleDamage : MonoBehaviour
{

    [SerializeField] float dmg;
    [SerializeField] EnemyAbstract EnemyAbstract;

    bool wasHitted = false;
    private void OnTriggerEnter2D(Collider2D collision)  // add setitititititititititiititi
    {
        Debug.Log(1212123);
        if (collision.CompareTag("Player"))
        {
            Debug.Log(222);
            var player = collision.GetComponent<Player>();
            Debug.Log(player);

            if (player != null)
            {
                if (!wasHitted){
                    player.TakeDamage(dmg);
                    wasHitted = true;

                }

                //Destroy(gameObject);
            }
        }
    }
    public void ChangeHit()
    {
        EnemyAbstract.SingleScript(); // лучше искать енеми в родители
        wasHitted = false;
    }

}
