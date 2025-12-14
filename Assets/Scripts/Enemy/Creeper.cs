using UnityEngine;
using System.Collections;

public class Creeper : EnemyBase
{
    [Header("Explosion Config")]
    [SerializeField] private float explodeDelay = 2f;
    [SerializeField] private float explodeRange = 2f;
    [SerializeField] private GameObject explosionEffectPrefab;

    [Header("Wander Config")]
    [SerializeField] private float wanderInterval = 2.5f;
    [SerializeField] private float wanderDistance = 2f;
    [SerializeField] private float wanderSpeedMultiplier = 0.6f;

    private bool isExploding = false;

    private float wanderTimer = 0f;
    private Vector2 wanderTarget;
    private bool isWandering = false;

    protected override void AI()
    {
        CheckPlayerDetection();

        if (!isPlayerDetected)
        {
            Wander();
            return;
        }

        UpdateCreeperBehavior();
    }

    private void Wander()
    {
        if (isExploding) return;

        wanderTimer -= Time.deltaTime;

        if (isWandering)
        {
            float dist = Vector2.Distance(transform.position, wanderTarget);
            if (dist > 0.1f)
            {
                MoveTowards(wanderTarget, wanderSpeedMultiplier);
                return;
            }
            else
            {
                StopMovement();
                isWandering = false;
            }
        }

        if (wanderTimer <= 0f)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            wanderTarget = (Vector2)transform.position + randomDir * wanderDistance;

            isWandering = true;
            wanderTimer = wanderInterval;
        }
    }

    private void UpdateCreeperBehavior()
    {
        if (isExploding) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowards(player.position);
        }
        else
        {
            StopMovement();
            StartCoroutine(IE_Explode());
        }
    }

    private IEnumerator IE_Explode()
    {
        Debug.Log("Creeper is about to explode!");
        isExploding = true;
        canAttack = false;
        isAttacking = true;

        StopMovement();

        yield return new WaitForSeconds(explodeDelay);

        ExecuteAttack();
    }

    protected override void ExecuteAttack()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = new Color(1, 0.4f, 0.4f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, explodeRange);
    }
}
