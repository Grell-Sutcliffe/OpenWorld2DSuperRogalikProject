using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skeleton : MonoBehaviour
{
    [SerializeField] Collider2D killZone;
    [SerializeField] GameObject projectilePref;
    [SerializeField] float attackDelay;
    [SerializeField] Transform spawnArrowPos;

    [SerializeField] float speed;
    [SerializeField] BoxCollider2D walkZone;

    [SerializeField] float reachDist = 0.1f;
    [SerializeField] GameObject pref;
    float lastHit;
    
    //GameObject player;
    bool isTriggered;
    Coroutine coroutine;
    bool canHit = true;

    Vector2 moveTarget;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        PickNewTarget();

    }

    private void PickNewTarget()
    {
        Bounds b = walkZone.bounds;

        moveTarget = new Vector2(
            UnityEngine.Random.Range(b.min.x, b.max.x),
            UnityEngine.Random.Range(b.min.y, b.max.y)
        );
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, moveTarget) < reachDist)
            PickNewTarget();
        //Debug.Log(Vector2.MoveTowards(
          //  rb.position,
            //moveTarget,
          //  /speed * Time.fixedDeltaTime
        //));
        //Debug.Log(Vector2.Distance(rb.position, moveTarget));
        
        rb.MovePosition(
        Vector2.MoveTowards(
            rb.position,
            moveTarget,
            speed * Time.fixedDeltaTime
        ));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player!");
            var player = collision.GetComponent<Player>();
            coroutine = StartCoroutine(AttackLoop(player.GetTarget())); // add direction to player in order to create more difficult enemy
            isTriggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(coroutine);
            isTriggered = false;
        }
    }
    IEnumerator AttackLoop(Transform player)
    {
        while (true)
        {   if (!canHit && Time.time - lastHit < attackDelay)
            {
                yield return new WaitForSeconds(attackDelay - (Time.time - lastHit));

            }
            Shoot(player);
            yield return new WaitForSeconds(attackDelay);
            canHit = true;
        }
    }

    private void Shoot(Transform playerT)
    {
        Debug.Log("Attacked");
        Vector2 dir = playerT.position - spawnArrowPos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject arrowGO = Instantiate(
            projectilePref,
            spawnArrowPos.position,
            Quaternion.Euler(0, 0, angle)
        );
        arrowGO.GetComponent<Projectiles>().SetDir(dir);
        lastHit = Time.time;
        Debug.Log(lastHit);
        canHit = false;
    }
}
