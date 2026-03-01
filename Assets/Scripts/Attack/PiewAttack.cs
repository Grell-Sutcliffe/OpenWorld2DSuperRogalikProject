using UnityEngine;

public class PiewAttack : MonoBehaviour
{
    //нужен ли риджитбоди...
    [SerializeField] GameObject ShootPoint;
    [SerializeField] GameObject projectilePref;
    GameObject prefab;
    IAttacker owner;
    private void Awake()
    {
        owner = GetComponentInParent<IAttacker>();
        Creature creature = GetComponentInParent<Creature>();
        if (creature != null)
        {
            Weapon weapon = creature.GetWeapon();

            WeaponRange weaponRange = weapon as WeaponRange;

            if (weaponRange != null)
            {
                prefab = weaponRange.projectilePrefab;
            }
        }
    }
    public void Shoot()
    {
        Vector2 dir = ShootPoint.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject arrowGO = Instantiate(
            projectilePref,         // тут нада от оружия брать
            ShootPoint.transform.position,
            Quaternion.Euler(0, 0, angle)
        );
        var arrow = arrowGO.GetComponent<Projectiles>();
        arrow.SetDir(dir);
        arrow.dmg = owner.currentDmg;
    }
    public void ChangeHit()
    {
        if (owner != null)
        {
            Shoot();
            owner.UnActivePivot();
            owner.StartDelay();
        }

    }
}
