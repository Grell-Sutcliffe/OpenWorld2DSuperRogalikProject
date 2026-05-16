using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;

public abstract class EnemyAbstract : Creature, IDamagable, IAttacker
{
    [HideInInspector] public Vector2 externalForce;
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
    public Rigidbody2D rb;
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

    /*
    float crit_chance = 0.7f;
    float crit_dmg = 2.5f;
    */

    protected Animator anim;
    protected float offset;
    public GameObject pivot;

    [SerializeField] WeaponSO dataW;

    protected float current_dmg;
    public Damage currentDmg => new Damage(current_dmg, 0, ElementType.Physical);

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
        if (isHitting) return;

        facingRight = shouldFaceRight;
        sr.flipX = !facingRight;
        Flip();
    }

    void Flip()
    {
        pivot.transform.localPosition = new Vector3(
            -pivot.transform.localPosition.x,
            pivot.transform.localPosition.y,
            pivot.transform.localPosition.z
        );
    }

    public void FaceTarget(Transform target)
    {
        float dirX = target.position.x - transform.position.x;
        FaceByDirX(dirX);
    }

    void ShowDamage(Damage dmg)
    {
        if (dmg.physical_dmg > 0)
        {
            var txt = Instantiate(
                damageTextPrefab,
                new Vector3(damageTextPoint.position.x - 0.2f, damageTextPoint.position.y, damageTextPoint.position.z),
                Quaternion.identity
            );
            txt.InitPhysical(dmg);
        }
        if (dmg.elemental_dmg > 0)
        {
            var txt = Instantiate(
                damageTextPrefab,
                new Vector3(damageTextPoint.position.x + 0.2f, damageTextPoint.position.y, damageTextPoint.position.z),
                Quaternion.identity
            );
            txt.InitElemental(dmg);
        }
    }

    override public void TakeDamage(Damage dmg)
    {
        if (isDead) return;
        //LoggerName($"took dmg = {dmg.damage}", true);

        Damage new_damage = new Damage(dmg);

        if (dmg.element_type != ElementType.Physical)
        {
            effectController.HandleEffect(new_damage);
        }

        int dmg_amount = (int)new_damage.physical_dmg + (int)new_damage.elemental_dmg;
        hp -= dmg_amount;

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

        EventBus.Raise(new EnemyKilledEvent(name));

        Destroy(gameObject);
    }
    public void DieInAnimation()
    {
        //Destroy(gameObject);
        Die();

        SpawnZone sz = GetComponentInParent<SpawnZone>();
        if (sz != null) sz.Died();
    }

    protected override void Awake()
    {
        base.Awake();
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
    protected Vector2 frameMovement;

    protected virtual void FixedUpdate()
    {
        if (isDead || isStopped) return;

        frameMovement = Vector2.zero;
        bool suppressMovement = externalForce.sqrMagnitude > 0.1f;

        anim.SetBool("isTriggered", isTriggered);

        if (isTriggered && playerTrans != null)
        {
            float distToPlayer = Vector2.Distance(rb.position, playerTrans.position);

            if (distToPlayer < reachDisttoRotatePivot && !isHitting)
            {
                RotatePivot(playerTrans, offset);
            }

            HandleCombat(distToPlayer);
        }
        else
        {
            Wander();
        }
        frameMovement += externalForce * Time.fixedDeltaTime;
        externalForce = Vector2.zero;
        rb.MovePosition(rb.position + frameMovement);
        /*
        if (externalForce.sqrMagnitude > 0.001f)
        {
            rb.MovePosition(rb.position + externalForce * Time.fixedDeltaTime);
            externalForce = Vector2.zero; // сбрасываем каждый кадр
        }
        */
    }
    protected abstract void HandleCombat(float distToPlayer);
    protected void StopMovement()
    {
        rb.MovePosition(rb.position);
    }
    protected virtual void RotatePivot(Transform playerPos, float offs = 0f)
    {
        Vector2 dir = ((Vector2)playerPos.position - (Vector2)pivot.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2) поворачиваем pivot меча
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle - offs); // оффсет под спрайт
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

        Vector2 newPos = Vector2.MoveTowards(rb.position, moveTarget, speed * Time.fixedDeltaTime);
        frameMovement += newPos - rb.position;
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
        Vector2 newPos = Vector2.MoveTowards(rb.position, playerTrans.position, speed * Time.fixedDeltaTime);
        frameMovement += newPos - rb.position;
    }
    protected virtual void RunFrom(Transform t)
    {
        Vector2 dir = (rb.position - (Vector2)t.position).normalized;
        frameMovement += dir * speed * Time.fixedDeltaTime;
    }
    protected void PickNewTarget()
    {
        if (walkZone != null)
        {
            Bounds b = walkZone.bounds;
            moveTarget = new Vector2(
                UnityEngine.Random.Range(b.min.x, b.max.x),
                UnityEngine.Random.Range(b.min.y, b.max.y)
            );
        }
    }

    public virtual void SetAggro(Transform target)  // ???
    {
        playerTrans = target;
        isTriggered = true;
    }

    protected abstract void TryAttack();

    public virtual void UnActivePivot() { }

    public void DealDamage()
    {
        /*
        int delta_damage = 0;

        System.Random rand = new System.Random();
        int chance = rand.Next(0, 101);

        if (chance <= crit_chance * 100)
        {
            delta_damage += RoundToMax(weapon.stats.physical_attack * crit_dmg);
        }
        */

        this.current_dmg = weapon.stats.physical_attack;

        //LoggerName($"now have {this.currentDmg.damage} damage, delta_damage = {delta_damage}\ncrit_chance = {crit_chance}, crit_dmg = {crit_dmg}, chance = {chance}");
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



    int RoundToMax(float number)
    {
        return ((number * 10 % 10 > 0) ? ((int)number + 1) : ((int)number));
    }

}
