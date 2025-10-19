using System.Collections.Generic;
using UnityEngine;

public class ProcedurGenerationAlg
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLen)
    {

        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var prevPos = startPos;

        for (int i = 0; i < walkLen; i++)
        {
            var newPos = prevPos + Direction2D.GetRandCardDir();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;

    }


    public static List<Vector2Int> RandWalkCorr(Vector2Int startPos, int corrLen, int corrW = 1)
    {
        List<Vector2Int> corr = new List<Vector2Int>();
        var dir = Direction2D.GetRandCardDir();
        var currPos = startPos;
        corr.Add(currPos);
        var dop = new Vector2Int(0, 0);
        var corner = new Vector2Int(0, 0);
        if (dir[0] == -1 || dir[1] == -1){
            dop = new Vector2Int(-1, -1);
        }
        else
        {
            dop = new Vector2Int(1, 1);
        }

        for (int i = 0; i < corrLen; i++)
        {
            /*
            if (i == 0)
            {   corner = dop + (-1 * dir);
                corr.Add(corner);
            }else if (i == corrLen - 1)
            {

            }
            */
            if (corrW == 2)
            {
                corr.Add(currPos + dop + (-1 * dir));
                if (i == corrLen - 1) corr.Add(currPos + dop + (-1 * dir) + dir);
            }
            corr.Add(currPos + dir);
            
            currPos += dir;

        }

        return corr;
    }


   public static List<BoundsInt> BinarySpace( BoundsInt spaceToSplit, int minW, int minH)
    {
        Queue<BoundsInt> roomsQ = new Queue<BoundsInt>();
        List<BoundsInt> roomL = new List<BoundsInt> ();
        roomsQ.Enqueue(spaceToSplit);

        while(roomsQ.Count!=0)
        {
            var room = roomsQ.Dequeue();    
            if(room.size.y >= minH && room.size.x >= minW)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minH * 2)
                    {
                        SplitHor(minW, roomsQ, room);

                    }else if(room.size.x >= minW * 2)
                    {
                        SplitVer( minH, roomsQ, room);
                    }
                    else if(room.size.y >= minH && room.size.x >= minW)
                    {
                        roomL.Add(room);
                    }
                }
                else
                {   if (room.size.x >= minW * 2 )
                    {
                        SplitVer(minW, roomsQ, room);
                    }else if (room.size.y >= minH * 2)
                    {
                        SplitHor( minH, roomsQ, room);

                    }
                    else if (room.size.y >= minH && room.size.x >= minW)
                    {
                        roomL.Add(room);
                    }
                }
            }
        }
        return roomL;
    }

    private static void SplitVer(int minW, Queue<BoundsInt> roomsQ, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x); // can be changed

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQ.Enqueue(room1);
        roomsQ.Enqueue(room2);


    }

    private static void SplitHor( int minH, Queue<BoundsInt> roomsQ, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQ.Enqueue(room1);
        roomsQ.Enqueue(room2);
    }
}


public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // UP
        new Vector2Int(1,0), // RIG
        new Vector2Int(0,-1), // DOWN
        new Vector2Int(-1,0)  // LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };


    public static Vector2Int GetRandCardDir()
    {
        return cardinalDirectList[Random.Range(0, cardinalDirectList.Count)];
    }

}



