using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    MainController mainController;

    public GameObject mapPrefab;

    public Dictionary<int, GameObject> dict_map_GOs = new Dictionary<int, GameObject>();

    List<MapPointScript> list_of_map_point_scripts;

    public int bebebe = 0;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        FillDict();
    }

    void FillDict()
    {
        int temp_index = 1;

        dict_map_GOs[temp_index] = mainController.Dedus;
        temp_index++;

        dict_map_GOs[temp_index] = mainController.GrandsonEugene;
        temp_index++;

        dict_map_GOs[temp_index] = mainController.Doggy;
        temp_index++;
    }
}
