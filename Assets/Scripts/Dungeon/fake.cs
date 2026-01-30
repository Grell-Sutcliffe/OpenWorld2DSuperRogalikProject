using UnityEngine;

public class fake : MonoBehaviour
{
    public CorrtidorsGen ss;
    public TileMapVisualize pipi;
    public GameObject playerPref;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        pipi.Clear();
        //ss.fake();
        Instantiate(playerPref);
    }
    void Start()
    {
        //pipi.Clear();
        //ss.fake();
        //Instantiate(playerPref);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
