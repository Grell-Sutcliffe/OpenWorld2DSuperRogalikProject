using System.Collections;
using UnityEngine;

public abstract class EnemyMelee : EnemyAbstract
{
    protected float t; 


    protected override void HandleCombat(float distToPlayer)
    {
        if (distToPlayer < reachDisttoPlayer)
        {
            StopMovement();

            if (canStrafe) StrafeAround(playerTrans);

            if (canHit && !isHitting)
            {
                if (isStopWhileHit) StopWalk();
                RotatePivot(playerTrans, offset);
                Hit();
            }
            return;
        }

        if (canWalk) ChasePlayer();
    }

    protected virtual void Hit()
    {
        isHitting = true;
        anim.SetTrigger("hit");
        DealDamage();
        pivot.gameObject.SetActive(true);

    }
   

    
}
