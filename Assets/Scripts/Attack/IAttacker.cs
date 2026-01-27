using UnityEngine;

public interface IAttacker 
{
    GameObject owner { get; }
    void DealDamage();
    Damage currentDmg {  get; }







    void UnActivePivot();
    void StartDelay();
}
