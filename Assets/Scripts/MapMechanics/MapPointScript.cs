using UnityEngine;

public class MapPointScript : MonoBehaviour
{
    MapController mapController;

    public MapPointType mapPointType;

    void Start()
    {
        mapController = GameObject.Find("MapController").GetComponent<MapController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /*
            if (mapController.dict_map_GOs[mapPointType].name == "GrandsonEugene")
            {
                if (mapController.dict_map_GOs[mapPointType].GetComponent<GrandsonEugineMoveScript>().need_to_move)
                {
                    return;
                }
            }
            */
            Teleport();
        }
    }

    void Teleport()
    {
        if (mapController.dict_map_GOs[mapPointType] == null)
        {
            Debug.LogError("GameObject = null!!! Can't be teleported to that MapPoint :(\nFill the GO in the Inspector or ignore that Error.");
            return;
        }

        mapController.dict_map_GOs[mapPointType].transform.position = transform.position;
    }
}
