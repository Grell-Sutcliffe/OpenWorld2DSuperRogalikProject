using UnityEngine;
using System.Collections;

public class RogueKnight : EnemyBase
{
    [Header("Ranged Config")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;

    [Header("Movement")]
    [SerializeField] private float strafeSpeed = 2f;
    [SerializeField] private float retreatDistance = 3f;

    private Vector3 targetOffset = new Vector3(0, 0.5f, 0);
    private Vector2 strafeDirection = Vector2.right;
    private float strafeTimer;

    protected override void AI()
    {
        CheckPlayerDetection();

        if (!isPlayerDetected)
        {
            StopMovement();
            return;
        }

        UpdateCombatBehavior();
    }

    private Vector3 GetPlayerPosition()
    {
        return player.position + targetOffset;
    }

    private void UpdateCombatBehavior()
    {
        if (isAttacking) return;

        Vector3 playerPos = GetPlayerPosition();
        float distance = Vector2.Distance(transform.position, playerPos);

        if (distance > attackRange)
        {
            MoveTowards(playerPos);
        }
        else if (distance < retreatDistance)
        {
            Vector2 retreatDir = (transform.position - playerPos).normalized;
            MoveTowards((Vector2)transform.position + retreatDir * 2f);
        }
        else
        {
            Strafe();

            if (canAttack)
                StartCoroutine(IE_Attack());
        }
    }

    private void Strafe()
    {
        strafeTimer += Time.deltaTime;
        if (strafeTimer > 2f)
        {
            strafeDirection = -strafeDirection;
            strafeTimer = 0f;
        }

        Vector2 toPlayer = (GetPlayerPosition() - transform.position).normalized;
        Vector2 perpendicular = new Vector2(-toPlayer.y, toPlayer.x);
        rb.linearVelocity = perpendicular * strafeDirection.x * strafeSpeed;
    }

    private IEnumerator IE_Attack()
    {
        isAttacking = true;
        canAttack = false;

        yield return new WaitForSeconds(0.15f);

        ExecuteAttack();

        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    protected override void ExecuteAttack()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Vector2 dir = (GetPlayerPosition() - firePoint.position).normalized;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
            proj.Initialize(dir, projectileSpeed, damage);
    }
}
