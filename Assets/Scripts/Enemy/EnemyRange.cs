using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public abstract class EnemyRange : EnemyAbstract
{
    [SerializeField] float reachDistfromPlayer = 3;
    
    protected override void FixedUpdate()
    {
        if (isTriggered)
        {
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoRotatePivot)
            {
                RotatePivot(playerTrans, offset);

            }
            if (Vector2.Distance(rb.position, playerTrans.position) < reachDistfromPlayer) // убегать, добавить переменную
            {
                RunFrom(playerTrans);
                if (canHit){
                    if (isStopWhileHit) StopWalk();
                    RotatePivot(playerTrans, offset);
                    Hit();
                }
                return;
            }
            else if (Vector2.Distance(rb.position, playerTrans.position) < reachDisttoPlayer)
            {
                rb.linearVelocity = Vector2.zero; // сделать мини блуждания
                if (canStrafe) StrafeAround(playerTrans);
                if (canHit)
                {
                    if (isStopWhileHit) StopWalk();
                    RotatePivot(playerTrans, offset);
                    Hit();
                }
                return;
            }

            
            if (canWalk)
                ChasePlayer();
            return;
        }

        Wander();
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

    }
    public override void UnActivePivot()
    {
        pivot.gameObject.SetActive(false);
    }
    public override void StartDelay()
    {
        canHit = false;
        timeLastHit = Time.time;
        StartCoroutine(Delay(attackDur));
    }

    public virtual void Shoot()
    {
        
    }
}
