using System.Collections.Generic;
using UnityEngine;

public class Location
{
    public string title;
}

public class EnemySpawn : Location
{
    List<EnemyOfAmountSO> list_enemy_of_amount;

    EnemySpawnSO data;

    public EnemySpawn(EnemySpawnSO enemySpawnSO)
    {
        this.data = enemySpawnSO;

        list_enemy_of_amount = new List<EnemyOfAmountSO>();

        foreach (EnemyOfAmountSO enemyOfAmountSO in enemySpawnSO.enemyOfAmountSOs)
        {
            list_enemy_of_amount.Add(enemyOfAmountSO);
        }
    }
}

public class MapController : MonoBehaviour
{
    MainController mainController;

    public GameObject mapPrefab;

    public Dictionary<MapPointType, GameObject> dict_map_GOs = new Dictionary<MapPointType, GameObject>();

    //public int bebebe = 0;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        FillDict();
    }

    void FillDict()
    {
        dict_map_GOs[MapPointType.Dedus] = mainController.Dedus;

        dict_map_GOs[MapPointType.Eugene] = mainController.GrandsonEugene;

        dict_map_GOs[MapPointType.Doggy] = mainController.Doggy;

        dict_map_GOs[MapPointType.Woman] = mainController.Woman;

        dict_map_GOs[MapPointType.Cows] = mainController.Cows;

        dict_map_GOs[MapPointType.Chickens] = mainController.Chickens;

        dict_map_GOs[MapPointType.Gardens] = mainController.Gardens;

        dict_map_GOs[MapPointType.Melnica] = mainController.Melnica;
    }
}

public enum MapPointType
{
    Dedus = 1,
    Eugene = 2,
    Doggy = 3,
    Woman = 4,
    Cows = 5,
    Chickens = 6,
    Gardens = 7,
    Melnica = 8,
}
