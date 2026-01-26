using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spearer : EnemyMelee
{
    [SerializeField] GameObject pivot;


    protected override void Start()
    {
        canHit = true;
        canStrafe = false;
        base.Start();

    }
    protected override void Hit(Transform playerPos)
    {
        pivot.gameObject.SetActive(true);
        Vector2 dir = ((Vector2)playerPos.position - (Vector2)pivot.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2) поворачиваем pivot меча
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // оффсет под спрайт
    }
    protected override void TryAttack()
    {

    }

    public override void SingleScript()
    {
        pivot.gameObject.SetActive(false);
    }
    /*
    protected override IEnumerator Hit(Transform playerPos)
    {
        Vector2 toPlayer = ((Vector2)playerPos.position - (Vector2)hitPoint.position).normalized;
        float ang = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        // pivot всегда "в руке"
        spear.position = hitPoint.position;
        spear.rotation = Quaternion.Euler(0, 0, ang - 90f); // оффсет под спрайт

        Vector3 basePos = hitPoint.position;
        Vector3 backPos = basePos - (Vector3)toPlayer * backOffset;
        Vector3 fwdPos = basePos + (Vector3)toPlayer * forwardOffset;

        float t = 0f;
        while (t < 1f) // windup
        {
            t += Time.deltaTime / windupDur;
            spear.position = Vector3.Lerp(basePos, backPos, t);
            yield return null;
        }

        col.enabled = true;
        t = 0f;
        while (t < 1f) // thrust
        {
            t += Time.deltaTime / thrustDur;
            spear.position = Vector3.Lerp(backPos, fwdPos, t);
            yield return null;
        }
        col.enabled = false;

        spear.position = basePos; // вернуть в hitPoint
        isHitting = false;
    }
    */
    
}
