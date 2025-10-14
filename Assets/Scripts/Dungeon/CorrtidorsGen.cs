using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorrtidorsGen : SimpleWalkGenerator
{
    [SerializeField]
    int corLen = 14, corrCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    float roomPercent = 0.8f;



    protected override void RunProcGen()
    {
        CorrFirstGen();
    }

    private void CorrFirstGen()
    {
        Debug.Log(12);
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();


        CreateCorr(floorPos, potentialRoomPos);
        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);


        List<Vector2Int> deadEnd = FindAllDeadEnd(floorPos);
        CreateRoomAtEnds(deadEnd, roomPos);

        floorPos.UnionWith(roomPos);

        tilemapVis.PaintFloor(floorPos);
        WallGen.CreateWalls(floorPos, tilemapVis);
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

        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();
        foreach (var rp in roomToCreate)
        {
            var roomFloor = RunRandWalk(randWalkPar, rp);
            roomPos.UnionWith(roomFloor);

        }
        return roomPos;
    }


    private void CreateCorr(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var currPos = startPos;
        potentialRoomPos.Add(currPos);
        for (int i = 0; i < corrCount; i++)
        {
            var corr = ProcedurGenerationAlg.RandWalkCorr(currPos, corLen);
            currPos = corr[corr.Count - 1];
            potentialRoomPos.Add(currPos);
            floorPos.UnionWith(corr);

        }
    }
}
