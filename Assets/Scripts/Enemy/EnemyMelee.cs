using System.Collections;
using UnityEngine;

public abstract class EnemyMelee : EnemyAbstract
{
    protected float t;  // from 0 to 1
    protected bool reload; // ЗАЧЕМ
    
    

    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer) // по идее разные рич дист
            {
                rb.linearVelocity = Vector2.zero;
                if (canStrafe) StrafeAround(playerTrans);

                if (canHit)
                {
                    //StartCoroutine(Hit(playerTrans));
                    if(isStopWhileHit) StopWalk();
                    RotatePivot(playerTrans, offset);
                    Hit();
                }
                return;
                
            }
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoRotatePivot)
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
        pivot.gameObject.SetActive(true);

    }
    protected virtual void RotatePivot(Transform playerPos, float offs = 0f)
    {
        Vector2 dir = ((Vector2)playerPos.position - (Vector2)pivot.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2) поворачиваем pivot меча
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle - offs); // оффсет под спрайт
    }

    protected virtual IEnumerator Delay(float time)
    {
        StartWalk();
        yield return new WaitForSeconds(time);
        canHit = true;
        reload = false;
        
    }
    public override void StartDelay()
    {
        canHit = false;
        reload = true;
        timeLastHit = Time.time;
        StartCoroutine(Delay(attackDur));
    }
}
