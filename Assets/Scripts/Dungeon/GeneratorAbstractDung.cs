using UnityEngine;

public abstract class GeneratorAbstractDung : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualize tilemapVis = null;

    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;


    public void GenDung()
    {
        tilemapVis.Clear();

        RunProcGen();
    }

    protected abstract void RunProcGen();
}
