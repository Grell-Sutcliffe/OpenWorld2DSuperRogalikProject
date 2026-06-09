using System.Collections.Generic;
using UnityEngine;

public static class MapFragmentRegistry
{
    private static List<MapFragment> allFragments = new List<MapFragment>();

    public static void Register(MapFragment fragment)
    {
        if (!allFragments.Contains(fragment))
            allFragments.Add(fragment);
    }

    public static void Deregister(MapFragment fragment)
    {
        allFragments.Remove(fragment);
    }

    public static MapFragment GetNearest(Vector3 position)
    {
        MapFragment nearest = null;
        float minSqrDist = Mathf.Infinity;

        foreach (var frag in allFragments)
        {
            if (frag == null) continue;
            float sqrDist = (frag.transform.position - position).sqrMagnitude;
            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                nearest = frag;
            }
        }
        return nearest;
    }
}
