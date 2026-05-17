using UnityEngine;

public class fake : MonoBehaviour
{
    public CorrtidorsGen ss;
    public TileMapVisualize pipi;
    public GameObject playerPref;
    private void Awake()
    {
        
        Instantiate(playerPref);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
