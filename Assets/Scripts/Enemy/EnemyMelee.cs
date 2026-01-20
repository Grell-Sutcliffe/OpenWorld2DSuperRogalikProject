using System.Collections;
using UnityEngine;

public abstract class EnemyMelee : EnemyAbstract
{
    protected bool isHitting = false;
    protected bool canHit = false;
    protected float t;  // from 0 to 1
    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer) // по идее разные рич дист
            {
                //rb.linearVelocity = Vector2.zero;
                if (canStrafe) StrafeAround(playerTrans);
                if (canHit && !isHitting)
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
    protected virtual IEnumerator Hit(Transform playerPos)
    {
       yield return null;
    }

    
}
