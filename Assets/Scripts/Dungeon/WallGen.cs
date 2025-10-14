using System.Collections.Generic;
using UnityEngine;

public static class WallGen
{


    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualize tileMapVis)
    {
        var basicWallPos = FindWallsInDir(floorPos, Direction2D.cardinalDirectList);
        foreach (var wall in basicWallPos)
        {
            tileMapVis.PaintWall(wall);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDir(HashSet<Vector2Int> floorPos, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var fPos in floorPos)
        {
            foreach (var dir in dirList)
            {
                var neghPos = fPos + dir;
                if (floorPos.Contains(neghPos) == false)
                {
                    wallPos.Add(neghPos);
                }
            }
        }
        return wallPos;
    }
}
