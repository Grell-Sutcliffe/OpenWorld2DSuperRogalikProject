using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Sworder : EnemyMelee
{

    [SerializeField] GameObject sword;
    [SerializeField] float attackDur;
    
    [SerializeField] float angleHit;

    [SerializeField] Collider2D col;


    protected override void Start()
    {
        canHit = true;
        canStrafe = true;
        base.Start();

    }
    protected override IEnumerator Hit(Transform playerPos)
    {
        col.enabled = true;
        Vector2 toPlayer = ((Vector2)playerPos.position - (Vector2)transform.position).normalized;
        float center = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        float startAngle = center - angleHit - 90;
        float endAngle = center + angleHit - 90;

        Debug.Log($"start = {startAngle}");
        Debug.Log($"end = {endAngle}");
        t = 0;
        while (true)
        {
            t += Time.deltaTime / attackDur;   // t: 0→1
            if (t > 1){
                break;
            }
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            sword.transform.localRotation = Quaternion.Euler(0, 0, angle);
            
            yield return null;
        }
        isHitting = false;
        col.enabled = false;        //attack delay
    }

    protected override void TryAttack()
    {

    }

}
