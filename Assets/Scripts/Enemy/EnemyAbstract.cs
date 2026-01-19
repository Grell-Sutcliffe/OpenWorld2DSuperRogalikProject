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

    protected Vector2 moveTarget;
    protected bool isTriggered;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        PickNewTarget();
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
