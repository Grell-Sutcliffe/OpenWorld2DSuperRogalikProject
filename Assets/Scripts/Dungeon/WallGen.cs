using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class WallGen
{


    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualize tileMapVis)
    {
        var basicWallPos = FindWallsInDir(floorPos, Direction2D.cardinalDirectList);
        var cornerWallPositions = FindWallsInDir(floorPos, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tileMapVis, basicWallPos, floorPos);
        CreateCornerWalls(tileMapVis, cornerWallPositions, floorPos);

    }

    private static void CreateBasicWalls(TileMapVisualize tileMapVis, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in basicWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectList)
            {
                var neighbourPosition = pos + direction;
                if (floorPos.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVis.PaintWall(pos, neighboursBinaryType);
        }
    }
    private static void CreateCornerWalls(TileMapVisualize tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
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
