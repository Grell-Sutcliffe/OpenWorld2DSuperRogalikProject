using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public Vector2Int centerPosition;
    public HashSet<Vector2Int> floorPositions;
    public RoomType roomType;
    public List<Vector2Int> enemySpawnPoints;
    public Vector2Int chestPosition;
    public bool isCleared = false;
    public GameObject roomObject; // —сылка на GameObject комнаты
}

public enum RoomType
{
    Enemy,
    Chest,
    Start,
    Boss,
    Shop,
    Empty
}