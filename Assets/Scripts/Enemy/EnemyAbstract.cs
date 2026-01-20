using System.Collections;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float speed;
    [SerializeField] protected BoxCollider2D walkZone;
    [SerializeField] protected float reachDist = 0.1f;
    [SerializeField] protected float reachDisttoPlayer = 5f;
    protected Rigidbody2D rb;
    protected Transform playerTrans; //??

    int strafeSign = 1; // рандомить вначале 1 и -1
    [SerializeField] protected float strafeTime = 2;
    [SerializeField] protected float strafeSpeed = 1;
    [SerializeField] protected bool canStrafe = false;
    protected Vector2 moveTarget;
    protected bool isTriggered;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        strafeSign *= Random.value < 0.5f ? -1 : 1;

        PickNewTarget();
        StartCoroutine(ChangeStrafe());
    }
    IEnumerator ChangeStrafe()
    {
        while (true)
        {
            strafeSign *= -1;
            yield return new WaitForSeconds(strafeTime);

        }
    }
    protected void StrafeAround(Transform playerTrans)
    {
        Vector2 toPlayer = ((Vector2)playerTrans.position - rb.position).normalized;
        Vector2 perp = new Vector2(-toPlayer.y, toPlayer.x) * strafeSign; // 90° в сторону

        rb.MovePosition(rb.position + perp * strafeSpeed * Time.fixedDeltaTime);
    }
    protected virtual void FixedUpdate()
    {
        if (isTriggered && playerTrans != null)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }
    }

    protected virtual void Wander()
    {
        if (Vector2.Distance(rb.position, moveTarget) < reachDist)
            PickNewTarget();

        rb.MovePosition(
            Vector2.MoveTowards(rb.position, moveTarget, speed * Time.fixedDeltaTime)
        );
    }
    public virtual void OnTrigger(Player player)
    {
        playerTrans = player.GetTarget();
        isTriggered = true;
    }
    public virtual void OffTrigger()
    {
        isTriggered = false;
    }
    protected virtual void ChasePlayer()
    {
        rb.MovePosition(
            Vector2.MoveTowards(rb.position, playerTrans.position, speed * Time.fixedDeltaTime)
        );
    }
    protected virtual void RunFrom(Transform t)
    {
        Vector2 dir = (rb.position - (Vector2)t.position).normalized;

        rb.MovePosition(
        rb.position + dir * speed * Time.fixedDeltaTime);
    }
    protected void PickNewTarget()
    {
        Bounds b = walkZone.bounds;
        moveTarget = new Vector2(
            UnityEngine.Random.Range(b.min.x, b.max.x),
            UnityEngine.Random.Range(b.min.y, b.max.y)
        );
    }

    public virtual void SetAggro(Transform target)  // ???
    {
        playerTrans = target;
        isTriggered = true;
    }

    protected abstract void TryAttack();
}
