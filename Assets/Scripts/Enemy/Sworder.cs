using System;
using System.Collections;
using UnityEngine;

public class Sworder : MonoBehaviour
{

    [SerializeField] GameObject sword;
    [SerializeField] float attackDur;

    [SerializeField] GameObject player;
    [SerializeField] float angleHit;
    [SerializeField] BoxCollider2D walkZone;
    float t;

    [SerializeField] float speed;
    bool isTriggered = false;
    bool isHitting = false;
    Vector2 moveTarget;
    [SerializeField] float reachDist = 0.1f;
    [SerializeField] float reachDisttoPlayer = 5f;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       // StartCoroutine(Hit(player.transform));

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
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, player.transform.position) < reachDisttoPlayer) // по идее разные рич дист
            {
                rb.linearVelocity = Vector2.zero;
                if (!isHitting)
                {
                    isHitting = true;
                    StartCoroutine(Hit(player.transform));
                }
            }
            else
            {
                rb.MovePosition(
                    Vector2.MoveTowards(
                        rb.position,
                        player.transform.position,
                        speed * Time.fixedDeltaTime
                    ));
            }
            return;
        }
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!");
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
    private void OnTriggerEnter2D(Collider2D collision)  // не ентер а просто
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player!");
            var player = collision.GetComponent<Player>();
            isTriggered = true;
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHitting) return; //???
            isTriggered = false;
        }
    }
    IEnumerator Hit(Transform playerPos)
    {
        Vector2 toPlayer = ((Vector2)playerPos.position - (Vector2)transform.position).normalized;
        float center = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        float startAngle = center - angleHit - 90;
        float endAngle = center + angleHit - 90;

        Debug.Log($"start = {startAngle}");
        Debug.Log($"end = {endAngle}");
        t = 0;
        while (true)
        {
            t += Time.deltaTime / attackDur;   // t: 0→1
            if (t > 1){
                break;
            }
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            sword.transform.localRotation = Quaternion.Euler(0, 0, angle);
            
            yield return null;
        }
        isHitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
