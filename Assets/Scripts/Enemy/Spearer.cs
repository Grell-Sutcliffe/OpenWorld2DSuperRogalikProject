using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spearer : EnemyMelee
{
    [SerializeField] Transform spear;
    [SerializeField] float attackDur;
    [SerializeField] Transform hitPoint;

    [SerializeField] Collider2D col;
    [SerializeField] float windupDur = 0.25f;   // время оттяжки назад
    [SerializeField] float thrustDur = 0.15f;   // время укола вперёд
    [SerializeField] float recoverDur = 0.05f;  // (опц.) возвращение
    [SerializeField] float backOffset = 0.5f;
    [SerializeField] float forwardOffset = 1.0f;
    protected override void Start()
    {
        canHit = true;
        canStrafe = false;
        base.Start();

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
    protected override void TryAttack()
    {

    }
}
