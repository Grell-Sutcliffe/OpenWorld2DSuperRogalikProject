using UnityEngine;

public abstract class EnemyRange : EnemyAbstract
{
    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < 3) // убегать, добавить переменную
            {
                RunFrom(playerTrans);
            }
            else if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer)
            {
                //rb.linearVelocity = Vector2.zero; // сделать мини блуждания
                if (canStrafe) StrafeAround(playerTrans);
            }
            else
            {
                ChasePlayer();
            }
            return;
        }

        Wander();
    }
}
