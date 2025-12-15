using System;
using UnityEngine;
using System.Collections;

public class Soldier : EnemyBase
{
    [Header("Animation Settings")]
    [SerializeField] private float attackAnimationDelay = 1f;
    [SerializeField] private bool useAnimationEvents = true;

    private Coroutine attackCoroutine;

    protected override void AI()
    {
        CheckPlayerDetection();
        UpdateAnimations();
        if (!isPlayerDetected)
        {
            StopMovement();
            return;
        }

        UpdateSoldierBehavior();
    }

    private void UpdateSoldierBehavior()
    {
        if (isAttacking) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowards(player.position);
        }
        else
        {
            StopMovement();
            if (attackCoroutine == null)
                attackCoroutine = StartCoroutine(IE_Attack());
        }
    }

    IEnumerator IE_Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        if (!useAnimationEvents)
            ExecuteAttack();
        yield return new WaitForSeconds(attackAnimationDelay);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown - attackAnimationDelay);

        attackCoroutine = null;
    }

    protected override void ExecuteAttack()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            //PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(damage);
            //}
        }
    }


    protected override void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
    }

}