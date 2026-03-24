using UnityEngine;

public class WeaponRange : Weapon
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float range;
    public WeaponRange(WeaponSO data, GameObject projectilePrefab, float projectileSpeed, float range) : base(data)
    {
        this.projectilePrefab = projectilePrefab;
        this.projectileSpeed = projectileSpeed;
        this.range = range;
    }

    public WeaponRange(WeaponSO data, int id, GameObject projectilePrefab, float projectileSpeed, float range) : base(data, id)
    {
        this.projectilePrefab = projectilePrefab;
        this.projectileSpeed = projectileSpeed;
        this.range = range;
    }

    public WeaponRange(WeaponSO data, int id, int amount, GameObject projectilePrefab, float projectileSpeed, float range) : base(data, id, amount)
    {
        this.projectilePrefab = projectilePrefab;
        this.projectileSpeed = projectileSpeed;
        this.range = range;
    }
}
