using UnityEngine;

public class MapPointScript : MonoBehaviour
{
    MapController mapController;

    public int index;

    void Start()
    {
        mapController = GameObject.Find("MapController").GetComponent<MapController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        mapController.dict_map_GOs[index].transform.position = transform.position;
    }
}
