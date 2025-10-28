using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorrtidorsGen : SimpleWalkGenerator
{
    [SerializeField]
    int corLen = 14, corW = 1, corrCount = 5;

    [Range(0f, 1)]
    [SerializeField] float roomPercent = 0f;
    int s = 0;

    public bool is_rand_rooms = true;

    // Список всех комнат
    private List<RoomData> allRooms = new List<RoomData>();

    // Префабы для разных типов комнат
    [SerializeField] private GameObject enemyRoomPrefab;
    [SerializeField] private GameObject chestRoomPrefab;
    [SerializeField] private GameObject startRoomPrefab;
    [SerializeField] private GameObject bossRoomPrefab;

    protected override void RunProcGen()
    {
        allRooms.Clear();
        CorrFirstGen();
    }

    private void CorrFirstGen()
    {
        
        Debug.Log(12);
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        s += potentialRoomPos.Count();
        CreateCorr(floorPos, potentialRoomPos);
        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);


        //List<Vector2Int> deadEnd = FindAllDeadEnd(floorPos);
        //CreateRoomAtEnds(deadEnd, roomPos);

        floorPos.UnionWith(roomPos);

        // CreateRoomObjects();

        tilemapVis.PaintFloor(floorPos);
        WallGen.CreateWalls(floorPos, tilemapVis);
        Debug.LogWarning(potentialRoomPos.Count());
        //Debug.LogWarning(s); ??????????
    }
    
    private void CreateRoomAtEnds(List<Vector2Int> deadEnd, HashSet<Vector2Int> roomPos)
    {
        foreach (var pos in deadEnd)
        {
            if (roomPos.Contains(pos) == false)
            {
                var room = RunRandWalk(randWalkPar, pos);
                roomPos.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnd(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPos)
        {
            int neibCount = 0;
            foreach (var dir in Direction2D.cardinalDirectList)
            {
                if (floorPos.Contains(dir + pos))
                {
                    neibCount++;
                }
            }
            if (neibCount == 1)
            {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos){
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
        int roomCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);
        List<Vector2Int> roomToCreate;
        if (is_rand_rooms)
        {
            roomToCreate = potentialRoomPos.ToList();
        }
        else
        {
            roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();

        }

        foreach (var rp in roomToCreate)
        {
            Debug.Log(rp);
            var roomFloor = RunRandWalk(randWalkPar, rp);
            roomPos.UnionWith(roomFloor);

            RoomData roomData = new RoomData
            {
                centerPosition = rp,
                floorPositions = new HashSet<Vector2Int>(roomFloor),
                roomType = DetermineRoomType(allRooms.Count) // Определяем тип
            };

            allRooms.Add(roomData);

        }
        return roomPos;
    }

    private RoomType DetermineRoomType(int roomIndex)
    {
        // Первая комната - стартовая
        if (roomIndex == 0) return RoomType.Start;

        // Последняя комната - босс
        if (roomIndex == allRooms.Count - 1) return RoomType.Boss;

        // Случайное распределение остальных комнат
        float rand = UnityEngine.Random.value;
        if (rand < 0.6f) return RoomType.Enemy;    // 60% вражеских
        if (rand < 0.8f) return RoomType.Chest;    // 20% сундуков
        return RoomType.Empty;                     // 20% пустых
    }

    private void CreateCorr(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var currPos = startPos;
        potentialRoomPos.Add(currPos);


        var bibi = new List<Vector2Int>() { currPos+ new Vector2Int (0,3),
        currPos+ new Vector2Int (3,0),
        currPos+ new Vector2Int (0,-3),
        currPos+ new Vector2Int (-3,0),};


        floorPos.UnionWith(bibi);
        var last_dirt = 0;
        for (int i = 0; i < corrCount; i++)
        {
            
            var corr = ProcedurGenerationAlg.RandWalkCorr(currPos, corLen, ref last_dirt, corW);
            Debug.Log("LAST");
            Debug.Log(last_dirt);
            currPos = corr[corr.Count - 1];

            //bibi = new List<Vector2Int>() { currPos+ new Vector2Int (0,3), currPos+ new Vector2Int (3,0),currPos+ new Vector2Int (0,-3),currPos+ new Vector2Int (-3,0),};
            //floorPos.UnionWith(bibi);

            potentialRoomPos.Add(currPos);
            floorPos.UnionWith(corr);

        }
    }
}
