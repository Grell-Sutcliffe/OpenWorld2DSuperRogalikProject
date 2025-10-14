using System;
using System.Collections.Generic;
using UnityEngine;

public class DevideGenerator : SimpleWalkGenerator
{
    [SerializeField]    
    private int minRoomW = 4, minRoomH = 4;

    [SerializeField]
    private int dungW = 20, dungH = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randWalkRoom = false;


    protected override void RunProcGen()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProcedurGenerationAlg.BinarySpace(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungW, dungH, 0)), minRoomW, minRoomH);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRoom(roomList);
        Debug.Log(floor);
        tilemapVis.PaintFloor(floor);
        WallGen.CreateWalls(floor, tilemapVis);

    }

    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomList)
        {
            for(int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }
}
