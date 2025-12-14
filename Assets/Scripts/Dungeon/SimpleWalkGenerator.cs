using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkGenerator : GeneratorAbstractDung
{
    [SerializeField]
    protected RandWalkSO randWalkPar;

    protected override void RunProcGen()
    {
        HashSet<Vector2Int> floorPos = RunRandWalk(randWalkPar, startPos);
        foreach (var floor in floorPos)
        {
            Debug.Log(floor);
        }
        tilemapVis.Clear();
        tilemapVis.PaintFloor(floorPos);
        WallGen.CreateWalls(floorPos, tilemapVis);
    }

    protected HashSet<Vector2Int> RunRandWalk(RandWalkSO parametrs, Vector2Int pos)
    {
        var curPos = pos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parametrs.iter; i++)
        {
            var path = ProcedurGenerationAlg.SimpleRandomWalk(curPos, parametrs.walkLen);
            floorPos.UnionWith(path);
            if (parametrs.startRandEachIter)
                curPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
        }
        return floorPos;
    }


}
