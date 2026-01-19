using System;
using System.Collections;
using UnityEngine;

public class Sworder : EnemyAbstract
{

    [SerializeField] GameObject sword;
    [SerializeField] float attackDur;

    [SerializeField] float angleHit;
    float t;

    bool isHitting = false;

    [SerializeField] Collider2D col;

    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer) // по идее разные рич дист
            {
                rb.linearVelocity = Vector2.zero;
                if (!isHitting)
                {
                    isHitting = true;
                    StartCoroutine(Hit(playerTrans));
                }
            }
            else
            {
                ChasePlayer();
            }
            return;
        }
        Wander();
    }
    
    IEnumerator Hit(Transform playerPos)
    {
        col.enabled = true;
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
        col.enabled = false;
    }

    protected override void TryAttack()
    {

    }

}
