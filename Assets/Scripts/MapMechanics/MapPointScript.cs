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
            if (mapController.dict_map_GOs[index].name == "GrandsonEugene")
            {
                if (mapController.dict_map_GOs[index].GetComponent<GrandsonEugineMoveScript>().need_to_move)
                {
                    return;
                }
            }
            Teleport();
        }
    }

    void Teleport()
    {
        mapController.dict_map_GOs[index].transform.position = transform.position;
    }
}
