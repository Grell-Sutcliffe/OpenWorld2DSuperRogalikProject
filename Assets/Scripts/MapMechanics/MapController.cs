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

    public List<string> consumableItems_names = null;

    public List<bool> list_is_consumableItem_collected = null;

    private const string SAVE_KEY_PREFIX = "QuestItemCollected_";

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        FillDict();

        SetUpController();

        LoadCollected();
    }

    void SetUpController()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Map");

        List<MapScript> mapScripts = new List<MapScript>();

        foreach (GameObject go in gameObjects)
        {
            MapScript mapScript = go.GetComponent<MapScript>();

            if (mapScript != null)
            {
                mapScripts.Add(mapScript);
            }
        }

        if (mapScripts.Count == 0)
        {
            Debug.LogWarning("MapController: не найдены объекты с тегом Map и компонентом MapScript.");
            return;
        }

        if (consumableItems_names == null)
        {
            consumableItems_names = new List<string>();
        }

        if (list_is_consumableItem_collected == null)
        {
            list_is_consumableItem_collected = new List<bool>();
        }

        consumableItems_names.Clear();
        list_is_consumableItem_collected.Clear();

        foreach (ConsumableItemSO consumableItemSO in mapScripts[0].quest_consumableItemSOs)
        {
            if (consumableItemSO == null) continue;

            consumableItems_names.Add(consumableItemSO.item_name);
            list_is_consumableItem_collected.Add(false);
        }
    }

    private string GetSaveKey(string itemName)
    {
        return SAVE_KEY_PREFIX + itemName;
    }

    public void SaveCollected()
    {
        if (consumableItems_names == null || list_is_consumableItem_collected == null) return;

        int count = Mathf.Min(consumableItems_names.Count, list_is_consumableItem_collected.Count);

        for (int i = 0; i < count; i++)
        {
            string itemName = consumableItems_names[i];

            if (string.IsNullOrEmpty(itemName)) continue;

            PlayerPrefs.SetInt(GetSaveKey(itemName), list_is_consumableItem_collected[i] ? 1 : 0);
        }

        PlayerPrefs.Save();

        Debug.Log("MapController: прогресс квестовых предметов сохранён.");
    }

    public void LoadCollected()
    {
        if (consumableItems_names == null || list_is_consumableItem_collected == null) return;

        int count = Mathf.Min(consumableItems_names.Count, list_is_consumableItem_collected.Count);

        for (int i = 0; i < count; i++)
        {
            string itemName = consumableItems_names[i];

            if (string.IsNullOrEmpty(itemName)) continue;

            list_is_consumableItem_collected[i] = PlayerPrefs.GetInt(GetSaveKey(itemName), 0) == 1;
        }

        Debug.Log("MapController: прогресс квестовых предметов загружен.");

        ApplyQuestGOsStateToAllMaps();
    }

    public void DeleteCollectedProgress()
    {
        if (consumableItems_names == null || list_is_consumableItem_collected == null) return;

        int count = Mathf.Min(consumableItems_names.Count, list_is_consumableItem_collected.Count);

        for (int i = 0; i < count; i++)
        {
            string itemName = consumableItems_names[i];

            if (!string.IsNullOrEmpty(itemName))
            {
                PlayerPrefs.DeleteKey(GetSaveKey(itemName));
            }

            list_is_consumableItem_collected[i] = false;
        }

        PlayerPrefs.Save();

        Debug.Log("MapController: прогресс квестовых предметов сброшен.");
    }

    private void OnEnable()
    {
        EventBus.OnEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.OnEvent -= HandleEvent;
    }

    private void HandleEvent(IEvent e)
    {
        if (e is ItemCollectedEvent itemCollectedEvent)
        {
            for (int i = 0; i < consumableItems_names.Count; i++)
            {
                if (consumableItems_names[i] == itemCollectedEvent.item_name)
                {
                    list_is_consumableItem_collected[i] = true;

                    DeleteItemByName(itemCollectedEvent.item_name);
                }
            }
        }
    }

    public void ApplyQuestGOsState(MapScript mapScript)
    {
        if (mapScript == null) return;
        if (mapScript.quest_consumableItemSOs == null) return;
        if (mapScript.quest_GOs == null) return;

        int count = mapScript.quest_consumableItemSOs.Count;

        for (int i = 0; i < count; i++)
        {
            ConsumableItemSO itemSO = mapScript.quest_consumableItemSOs[i];

            if (itemSO == null) continue;

            if (list_is_consumableItem_collected[i])  // если уже было собрано в прошлом сохранении или до перехода на новую карту
            {
                if (mapScript.quest_GOs[i] != null)
                {
                    if (mapScript.quest_GOs[i] != null)
                    {
                        Destroy(mapScript.quest_GOs[i]);
                        mapScript.quest_GOs[i] = null;
                    }
                }
            }
        }
    }

    private void ApplyQuestGOsStateToAllMaps()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Map");
        List<MapScript> mapScripts = new List<MapScript>();
        foreach (GameObject go in gameObjects)
        {
            mapScripts.Add(go.GetComponent<MapScript>());
        }

        foreach (MapScript mapScript in mapScripts)
        {
            ApplyQuestGOsState(mapScript);
        }
    }

    private void DeleteItemByName(string item_name)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Map");
        List<MapScript> mapScripts = new List<MapScript>();
        foreach (GameObject go in gameObjects)
        {
            mapScripts.Add(go.GetComponent<MapScript>());
        }

        foreach (MapScript mapScript in mapScripts)
        {
            mapScript.DeleteItemByName(item_name);
        }
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

        dict_map_GOs[MapPointType.Scientist] = mainController.Scientist;
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
    Scientist = 9,
}
