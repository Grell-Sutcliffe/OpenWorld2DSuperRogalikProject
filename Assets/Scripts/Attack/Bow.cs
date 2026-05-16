using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] EnemyRange owner;
    public void ChangeCollider()
    {
       
    }
    public void ChangeHit()
    {
        if (owner != null)
        {
            owner.Shoot();
            owner.UnActivePivot(); // лучше искать енеми в родители
            owner.StartDelay();
        }

    }
}
