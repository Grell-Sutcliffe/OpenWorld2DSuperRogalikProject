using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualize : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;

    [SerializeField]
    private TileBase floorTile, wallTop;

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

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintWall(Vector2Int wall)
    {
        PaitSingleTile(wallTilemap, wallTop, wall);
    }
}
