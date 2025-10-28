using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualize : MonoBehaviour
{
    [SerializeField]
    public Tilemap floorTilemap, wallTilemap;

    [SerializeField]
    private RuleTile wallRuleTile;
    
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSiderLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    [SerializeField] bool isRule;

    public void PaintFloor(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> floorPos, Tilemap floorTilemap, TileBase floorTile)
    {
        foreach (var pos in floorPos)
        {
            PaitSingleTile(floorTilemap, floorTile, pos);
        }
    }

    private void PaitSingleTile(Tilemap floorTilemap, TileBase floorTile, Vector2Int pos)
    {
        var tilePos = floorTilemap.WorldToCell((Vector3Int)pos);
        floorTilemap.SetTile(tilePos, floorTile);
    }
    private void PaitSingleTile(Tilemap floorTilemap, RuleTile roolTile, Vector2Int pos)
    {
        var tilePos = floorTilemap.WorldToCell((Vector3Int)pos);
        floorTilemap.SetTile(tilePos, roolTile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintWall(Vector2Int wall, string binType)
    {
        
        int typeAsInt = Convert.ToInt32(binType, 2);
        TileBase tile = null;
        if (WallHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSiderLeft;
        }
        else if (WallHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        
        if (isRule){
            PaitSingleTile(wallTilemap, wallRuleTile, wall);
        }
        else{
            if (tile != null)
                PaitSingleTile(wallTilemap, tile, wall);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string neighboursBinaryType)
    {
        int typeASInt = Convert.ToInt32(neighboursBinaryType, 2);
        TileBase tile = null;

        if (WallHelper.wallInnerCornerDownLeft.Contains(typeASInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallHelper.wallInnerCornerDownRight.Contains(typeASInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallHelper.wallDiagonalCornerDownLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallHelper.wallDiagonalCornerDownRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallHelper.wallDiagonalCornerUpRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallHelper.wallDiagonalCornerUpLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallHelper.wallFullEightDirections.Contains(typeASInt))
        {
            tile = wallFull;
        }
        else if (WallHelper.wallBottmEightDirections.Contains(typeASInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
            PaitSingleTile(wallTilemap, tile, position);
    }
}

