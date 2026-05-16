using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public abstract class EnemyRange : EnemyAbstract
{
    [SerializeField] float reachDistfromPlayer = 3;

    protected override void HandleCombat(float distToPlayer)
    {
        // —лишком близко Ч убегаем и стрел€ем
        if (distToPlayer < reachDistfromPlayer)
        {
            RunFrom(playerTrans);
            TryShoot();
            return;
        }

        // ¬ зоне комфорта Ч стоим/стрейфим и стрел€ем
        if (distToPlayer < reachDisttoPlayer)
        {
            StopMovement();
            if (canStrafe) StrafeAround(playerTrans);
            TryShoot();
            return;
        }

        // ƒалеко Ч догон€ем
        if (canWalk) ChasePlayer();
    }

    protected virtual void Hit()
    {
        pivot.gameObject.SetActive(true);

    }
    private void TryShoot()
    {
        if (!canHit) return;

        if (isStopWhileHit) StopWalk();
        RotatePivot(playerTrans, offset);
        Hit();
    }


    public override void UnActivePivot()
    {
        pivot.gameObject.SetActive(false);
    }

    public virtual void Shoot()
    {
        
    }
}
