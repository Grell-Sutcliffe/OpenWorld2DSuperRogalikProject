using UnityEngine;

public enum EnemyType
{
    RogueKnight,
    ShadowCreeper,
    StoneGolem
}

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Basic Properties")]
    [SerializeField] protected string enemyName;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float armour;
    [SerializeField] protected float damage; 
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected EnemyType enemyType;

    [Header("Sensing")]
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float keepDistance;

    [Header("Attack (Base Flags)")]
    [SerializeField] protected float attackCooldown;
    protected bool canAttack = true;
    protected bool isAttacking = false;

    [Header("Status")]
    [SerializeField] protected bool isPlayerDetected = false;

    [Header("Config")]
    [SerializeField] protected EnemyConfig enemyConfig;

    [Header("Components")]
    protected Transform player;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D enemyCollider;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        ApplyConfig();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    protected virtual void Update()
    {
        if (player == null) return;
        AI();
    }

    protected virtual void ApplyConfig()
    {
        if (enemyConfig == null)
        {
            Debug.LogWarning($"{name} has no EnemyConfig!");
            return;
        }

        enemyName = enemyConfig.enemyName;
        maxHealth = enemyConfig.maxHealth;
        currentHealth = maxHealth;
        armour = enemyConfig.armour;
        damage = enemyConfig.damage;
        moveSpeed = enemyConfig.moveSpeed;
        enemyType = enemyConfig.enemyType;

        detectionRange = enemyConfig.detectionRange;
        attackRange = enemyConfig.attackRange;
        keepDistance = enemyConfig.keepDistance;
        attackCooldown = enemyConfig.attackCooldown;
    }

    // ================= AI =================

    protected abstract void AI();

    protected virtual void CheckPlayerDetection()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        bool prevDetected = isPlayerDetected;

        isPlayerDetected = dist <= detectionRange;

        if (!prevDetected && isPlayerDetected)
            OnPlayerDetected();
        else if (prevDetected && !isPlayerDetected)
            OnPlayerLost();
    }

    protected virtual void OnPlayerDetected()
    {
        Debug.Log($"{enemyName} detected player");
    }

    protected virtual void OnPlayerLost()
    {
        Debug.Log($"{enemyName} lost player");
    }

    // ================= Movement =================

    protected virtual void MoveTowards(Vector2 target, float speedMultiplier = 1f)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed * speedMultiplier;
    }

    protected virtual void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
    }

    // ================= Attack =================

    protected abstract void ExecuteAttack();

    // ================= Debug =================

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
