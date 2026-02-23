using System.Collections;
using UnityEngine;

public abstract class EnemyMelee : EnemyAbstract
{
    protected float t;  // from 0 to 1    


    protected override void HandleCombat(float distToPlayer)
    {
        // Достаточно близко для атаки
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

        // Далеко — догоняем
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
