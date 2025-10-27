using UnityEngine;

public class StarItem : Item
{
    public int spins = 1;

    public override void OnPickup(GameObject player)
    {
        Debug.Log($"Подобрана звезда (+{spins} круток)");
        // GameManager.Instance.AddSpins(spins);
        Destroy(gameObject);
    }
}