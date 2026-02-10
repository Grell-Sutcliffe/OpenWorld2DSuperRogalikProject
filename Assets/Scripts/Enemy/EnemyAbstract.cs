using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;

public abstract class EnemyAbstract : MonoBehaviour, IDamagable, IAttacker
{
    [Header("Movement")]
    protected bool canHit = true;
    protected float timeLastHit;
    public GameObject owner => gameObject;

    [SerializeField] protected int music;
    [SerializeField] protected float attackDur = 1;

    [SerializeField] protected float speed;
    [SerializeField] public BoxCollider2D walkZone;
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

    [SerializeField] EnemyHealthBar enemyHealthBar;
    [SerializeField] protected float max_hp = 10f;
    [SerializeField] protected float hp = 10f;

    [SerializeField] protected string eName;
    [SerializeField] protected float reachDisttoRotatePivot; // чтобы 

    float crit_chance = 0.7f;
    float crit_dmg = 2.5f;

    protected Animator anim;
    protected float offset;
    public GameObject pivot;

    [SerializeField] WeaponSO dataW;
    protected Weapon weapon;

    protected float current_dmg;
    public Damage currentDmg => new Damage(current_dmg);

    protected bool isDead = false;

    [SerializeField] DamageText damageTextPrefab;
    [SerializeField] Transform damageTextPoint;


    [SerializeField] private float deadZone = 0.02f; // чтобы не дрожало около 0
    private bool facingRight = true; // “логическое” направление
    [SerializeField] private SpriteRenderer sr;

    
    private void Update()
    {
        if (isTriggered) FaceTarget(playerTrans);

    }

    public void FaceByDirX(float dirX)
    {
        if (Mathf.Abs(dirX) < deadZone) return;

        bool shouldFaceRight = dirX > 0f;
        if (shouldFaceRight == facingRight) return;

        facingRight = shouldFaceRight;
        sr.flipX = !facingRight;
        // если твой спрайт по умолчанию смотрит ВЛЕВО, поменяй на sr.flipX = facingRight;
    }

    // Если хочешь “смотреть на цель”
    public void FaceTarget(Transform target)
    {
        float dirX = target.position.x - transform.position.x;
        FaceByDirX(dirX);
    }

    void ShowDamage(Damage dmg)
    {
        var txt = Instantiate(
            damageTextPrefab,
            damageTextPoint.position,
            Quaternion.identity
        );
        txt.Init(dmg);
    }
    public virtual void TakeDamage(Damage dmg)
    {
        LoggerName($"took dmg = {dmg.damage}", true);
        hp -= dmg.damage;

        //if (hp <= 0) Die();
        if (hp <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("die");
            hp = 0;
            MusicManager.Instance.PlayByIndex(6);
        }
        else
        {
            MusicManager.Instance.PlayByIndex(music);
        }

        ChangeHealthBar();
        ShowDamage(dmg);
    }

    void ChangeHealthBar()
    {
        enemyHealthBar.ChangeHealthBarFillingScale(hp / max_hp);
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
    public void DieInAnimation()
    {
        Destroy(gameObject);

        SpawnZone sz = GetComponentInParent<SpawnZone>();
        if (sz != null) sz.Died();
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        weapon = new Weapon(dataW);
        strafeSign *= Random.value < 0.5f ? -1 : 1;
        sr = GetComponent<SpriteRenderer>();
        PickNewTarget();
        StartCoroutine(ChangeStrafe());
        ChangeHealthBar();
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
        int delta_damage = 0;

        System.Random rand = new System.Random();
        int chance = rand.Next(0, 101);

        if (chance <= crit_chance * 100)
        {
            delta_damage += RoundToMax(weapon.stats.attack * crit_dmg);
        }

        this.current_dmg = weapon.stats.attack + delta_damage;

        LoggerName($"now have {this.currentDmg.damage} damage, delta_damage = {delta_damage}\ncrit_chance = {crit_chance}, crit_dmg = {crit_dmg}, chance = {chance}");
    }
    public bool isHitting;
    protected virtual IEnumerator Delay(float time)
    {
        StartWalk();
        yield return new WaitForSeconds(time);
        LoggerName(canHit.ToString());
        canHit = true;
    }
    public void StartDelay()
    {
        LoggerName($"Can't attack on {attackDur} sec");
        isHitting = false;
        canHit = false;
        StartCoroutine(Delay(attackDur));
    }



    int RoundToMax(float number)
    {
        return ((number * 10 % 10 > 0) ? ((int)number + 1) : ((int)number));
    }

}
