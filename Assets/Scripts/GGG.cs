using System.Collections.Generic;
using UnityEngine;

public class TwoRoomsCorridorGenerator : GeneratorAbstractDung
{
    [Header("Room 1")]
    [SerializeField] private Vector2Int room1Size = new Vector2Int(10, 6);

    [Header("Room 2")]
    [SerializeField] private Vector2Int room2Size = new Vector2Int(12, 7);
    [Tooltip("Смещение второй комнаты относительно startPos")]
    [SerializeField] private Vector2Int room2Offset = new Vector2Int(20, 0);

    [Header("Corridor")]
    [SerializeField] private int corridorWidth = 1; // 1 = узкий, 2+ = шире
    [SerializeField] private bool horizontalFirst = true;

    protected override void RunProcGen()
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        // Комната 1: от startPos как от левого-нижнего угла
        Vector2Int room1Origin = startPos;
        var room1 = CreateRectRoom(room1Origin, room1Size);
        floor.UnionWith(room1);

        // Комната 2: смещённая
        Vector2Int room2Origin = startPos + room2Offset;
        var room2 = CreateRectRoom(room2Origin, room2Size);
        floor.UnionWith(room2);

        // Центры комнат (целочисленно)
        Vector2Int c1 = room1Origin + new Vector2Int(room1Size.x / 2, room1Size.y / 2);
        Vector2Int c2 = room2Origin + new Vector2Int(room2Size.x / 2, room2Size.y / 2);

        // Коридор
        var corridor = CreateLCorridor(c1, c2, corridorWidth, horizontalFirst);
        floor.UnionWith(corridor);

        // Рендер
        tilemapVis.PaintFloor(floor);
        WallGen.CreateWalls(floor, tilemapVis);
    }

    private HashSet<Vector2Int> CreateRectRoom(Vector2Int origin, Vector2Int size)
    {
        HashSet<Vector2Int> room = new HashSet<Vector2Int>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                room.Add(origin + new Vector2Int(x, y));
            }
        }
        return room;
    }

    private HashSet<Vector2Int> CreateLCorridor(Vector2Int from, Vector2Int to, int width, bool horizFirst)
    {
        HashSet<Vector2Int> corr = new HashSet<Vector2Int>();

        Vector2Int corner = horizFirst
            ? new Vector2Int(to.x, from.y)
            : new Vector2Int(from.x, to.y);

        AddThickLine(corr, from, corner, width);
        AddThickLine(corr, corner, to, width);

        return corr;
    }

    // Линия по сетке только по X или только по Y, с толщиной width
    private void AddThickLine(HashSet<Vector2Int> set, Vector2Int a, Vector2Int b, int width)
    {
        int w = Mathf.Max(1, width);

        if (a.x == b.x)
        {
            // вертикаль
            int x = a.x;
            int yMin = Mathf.Min(a.y, b.y);
            int yMax = Mathf.Max(a.y, b.y);

            for (int y = yMin; y <= yMax; y++)
            {
                for (int dx = 0; dx < w; dx++)
                {
                    set.Add(new Vector2Int(x + dx, y));
                }
            }
        }
        else if (a.y == b.y)
        {
            // горизонталь
            int y = a.y;
            int xMin = Mathf.Min(a.x, b.x);
            int xMax = Mathf.Max(a.x, b.x);

            for (int x = xMin; x <= xMax; x++)
            {
                for (int dy = 0; dy < w; dy++)
                {
                    set.Add(new Vector2Int(x, y + dy));
                }
            }
        }
        else
        {
            // на всякий случай, если вдруг передали диагональ — сделаем шагами по Манхэттену
            Vector2Int cur = a;
            while (cur != b)
            {
                if (cur.x != b.x) cur.x += (b.x > cur.x) ? 1 : -1;
                else if (cur.y != b.y) cur.y += (b.y > cur.y) ? 1 : -1;

                // "толщина" тут условная
                for (int dx = 0; dx < w; dx++)
                    for (int dy = 0; dy < w; dy++)
                        set.Add(cur + new Vector2Int(dx, dy));
            }
        }
    }
}
