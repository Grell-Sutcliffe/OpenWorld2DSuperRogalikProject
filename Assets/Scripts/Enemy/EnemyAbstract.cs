using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class EnemyAbstract : MonoBehaviour, IDamagable, IAttacker
{
    [Header("Movement")]
    protected bool canHit = true;
    protected float timeLastHit;
    public GameObject owner => gameObject;
    

    [SerializeField] protected float attackDur = 1;

    [SerializeField] protected float speed;
    [SerializeField] protected BoxCollider2D walkZone;
    [SerializeField] protected float reachDist = 0.1f;  // for walk
    [SerializeField] protected float reachDisttoPlayer = 5f;
    [SerializeField] protected float reachDisttoPlayerWithWindow = 4f;
    protected Rigidbody2D rb;
    protected Transform playerTrans; //??

    int strafeSign = 1; // рандомить вначале 1 и -1
    [SerializeField] protected float strafeTimeM = 2;
    [SerializeField] protected float strafeSpeed = 1;
    [SerializeField] protected bool canStrafe = false;
    [SerializeField] protected bool canWalk = true;
    [SerializeField] protected bool isStopWhileHit = true;
    protected Vector2 moveTarget;
    protected bool isTriggered;

    [SerializeField] protected float hp = 10f;

    [SerializeField] protected string eName;
    [SerializeField] protected float reachDisttoRotatePivot; // чтобы 


    protected float offset;
    public GameObject pivot;

    [SerializeField] WeaponSO dataW;
    protected Weapon weapon;

    protected float current_dmg;
    public Damage currentDmg => new Damage(current_dmg);
    public virtual void TakeDamage(Damage dmg)
    {
        LoggerName($"took dmg = {dmg.damage}", true);
        hp -= dmg.damage;
        if (hp <= 0) Die();

    }
    protected virtual void LoggerName(string s = null, bool isWarn = false)
    {
        if (isWarn)
            Debug.Log($"{eName} massage: {s}");
        else
            Debug.LogWarning($"{eName} massage: {s}");


    }

    protected virtual void Die()
    {
        LoggerName($"{name} dead");
        Destroy(gameObject);
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        weapon = new Weapon(dataW);
        strafeSign *= Random.value < 0.5f ? -1 : 1;

        PickNewTarget();
        StartCoroutine(ChangeStrafe());
    }
    IEnumerator ChangeStrafe()
    {
        while (true)
        {
            strafeSign *= -1;
            yield return new WaitForSeconds(strafeTimeM * Random.Range(0.5f, 1f));

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
    public void StopWalk()
    {
        rb.linearVelocity = Vector2.zero;
        canWalk = false;
    }
    public void StartWalk()
    {
        canWalk = true;
        //Debug.LogWarning($"!!!!!!!!!!!!!{savedSpeed}");
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

    public virtual void UnActivePivot(){}

    public void DealDamage()
    {
        this.current_dmg = weapon.damage;
        //weapon.damage -= 1;
        LoggerName($"now have {this.currentDmg.damage} damage");

    }
    public bool isHitting;
    protected virtual IEnumerator Delay(float time)
    {
        StartWalk();
        yield return new WaitForSeconds(weapon.cooldown);
        canHit = true;
    }
    public void StartDelay()
    {
        isHitting = false;
        canHit = false;
        timeLastHit = Time.time;
        StartCoroutine(Delay(attackDur));
    }



}
