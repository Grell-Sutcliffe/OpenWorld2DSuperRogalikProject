using UnityEngine;

[CreateAssetMenu(fileName = "EnemyOfAmount", menuName = "Enemy/EnemyOfAmount")]
public class EnemyOfAmountSO : ScriptableObject
{
    public GameObject enemy_prefab;
    public int amount;
}
