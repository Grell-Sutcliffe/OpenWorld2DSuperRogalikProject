using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skeleton : EnemyRange
{
    [SerializeField] GameObject projectilePref;

    [SerializeField] Transform spawnArrowPos;

    //Transform t;



    protected override void Start()
    {
        offset = 45;
        canStrafe = true;
        base.Start();


    }


    protected override void TryAttack()
    {

    }
  
    public override void Shoot()
    {
        Debug.Log("Attacked");
        Vector2 dir = playerTrans.position - spawnArrowPos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject arrowGO = Instantiate(
            projectilePref,         // тут нада от оружия брать
            spawnArrowPos.position,
            Quaternion.Euler(0, 0, angle)
        );
        arrowGO.GetComponent<Projectiles>().SetDir(dir);
        canHit = false;
    }
    
}
