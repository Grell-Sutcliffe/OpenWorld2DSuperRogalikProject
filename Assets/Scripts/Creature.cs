using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected Weapon weapon;

    public Weapon GetWeapon()
    {
        return weapon;
    }
}
