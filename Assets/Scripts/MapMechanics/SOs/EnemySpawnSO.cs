using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawn", menuName = "Location/EnemySpawn")]
public class EnemySpawnSO : LocationSO
{
    public List<EnemyOfAmountSO> enemyOfAmountSOs = new List<EnemyOfAmountSO>();
}
