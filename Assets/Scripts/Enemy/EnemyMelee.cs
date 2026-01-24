using System.Collections;
using UnityEngine;

public abstract class EnemyMelee : EnemyAbstract
{
    protected bool canHit = false;
    [SerializeField] float attackDur;
    protected float t;  // from 0 to 1
    protected bool reload = false;
    protected float timeLastHit;
    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer) // по идее разные рич дист
            {
                //rb.linearVelocity = Vector2.zero;
                if (canStrafe) StrafeAround(playerTrans);

                if (canHit)
                {
                    //StartCoroutine(Hit(playerTrans));
                    Hit(playerTrans);
                }
                if (!canHit && reload)
                {

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
    
    protected virtual IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        canHit = true;
    }
    public void StartDelay()
    {
        timeLastHit = Time.time;
        StartCoroutine(Delay(attackDur));
    }
    protected virtual void Hit(Transform playerPos)
    {
    }
}
