using System.Collections;
using UnityEngine;

public abstract class EnemyMelee : EnemyAbstract
{
    protected float t;  // from 0 to 1    
    

    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer) // по идее разные рич дист
            {
                rb.linearVelocity = Vector2.zero;
                if (canStrafe) StrafeAround(playerTrans);  // тут надо бы поменять...

                if (canHit && !isHitting)
                {
                    //StartCoroutine(Hit(playerTrans));
                    if(isStopWhileHit) StopWalk();
                    RotatePivot(playerTrans, offset);

                    Hit();
                }
                return;
                
            }
            if (!isHitting && Vector2.Distance(rb.position, playerTrans.position) < reachDisttoRotatePivot)
            {
                RotatePivot(playerTrans, offset);
                
            }
            if (canWalk)
                ChasePlayer();
        }
        else
        {
            Wander();
        }
        
    }

    protected virtual void Hit()
    {
        isHitting = true;
        DealDamage();
        pivot.gameObject.SetActive(true);

    }
    protected virtual void RotatePivot(Transform playerPos, float offs = 0f)
    {
        Vector2 dir = ((Vector2)playerPos.position - (Vector2)pivot.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2) поворачиваем pivot меча
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle - offs); // оффсет под спрайт
    }

    
}
