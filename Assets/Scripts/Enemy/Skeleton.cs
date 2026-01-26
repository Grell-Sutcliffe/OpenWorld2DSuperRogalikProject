using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skeleton : EnemyRange
{
    [SerializeField] GameObject projectilePref;
    [SerializeField] float attackDelay; // weapon

    [SerializeField] Transform spawnArrowPos;

    float lastHit;
    //Transform t;
    GameObject player;

    Coroutine coroutine;
    bool canHit = true;


    protected override void Start()
    {
        canStrafe = true;
        base.Start();


    }

    
    

    

    IEnumerator AttackLoop(Transform player)
    {
        while (true)
        {
            if (isTriggered == false)
            {
                yield break;
            }
            if (!canHit && Time.time - lastHit < attackDelay)
            {
                yield return new WaitForSeconds(attackDelay - (Time.time - lastHit));

            }
            Shoot(player);
            yield return new WaitForSeconds(attackDelay);
            canHit = true;
        }
    }

    protected override void TryAttack()
    {

    }
    public override void OnTrigger(Player player)
    {
        base.OnTrigger(player);
        StartCoroutine(AttackLoop(player.GetTarget()));
    }
    private void Shoot(Transform playerT)
    {
        Debug.Log("Attacked");
        Vector2 dir = playerT.position - spawnArrowPos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject arrowGO = Instantiate(
            projectilePref,         // тут нада от оружия брать
            spawnArrowPos.position,
            Quaternion.Euler(0, 0, angle)
        );
        arrowGO.GetComponent<Projectiles>().SetDir(dir);
        lastHit = Time.time;
        Debug.Log(lastHit);
        canHit = false;
    }
}
