using Unity.VisualScripting;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    MapController mapController;

    public bool is_center = false;
    public float width;
    public float height;

    MapScript map_north;
    MapScript map_south;
    MapScript map_west; // zapad
    MapScript map_east; // wostok
    MapScript map_north_east;
    MapScript map_north_west;
    MapScript map_south_east;
    MapScript map_south_west;

    void Start()
    {
        mapController = GameObject.Find("MapController").GetComponent<MapController>();

        //SpawnMaps();
        width = gameObject.transform.localScale.x * 2;
        height = gameObject.transform.localScale.y * 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if (mapController.bebebe < 50) // remove !!!
            //{
            SpawnMaps();
            DeleteMaps();
            is_center = true;
            //}
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnMaps();
            DeleteMaps();
            is_center = true;
        }
    }
    */

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /*
            is_center = false;
            Invoke("DeleteMaps", 0.1f);
            */
            Invoke("CheckMaps", 0.1f);
        }
    }

    void CheckMaps()
    {
        // pass;
    }

    void SpawnMaps()
    {
        Vector3 position;

        //mapController.bebebe++;

        // north
        if (map_north == null)
        {
            position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            map_north = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // south
        if (map_south == null)
        {
            position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
            map_south = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // east
        if (map_east == null)
        {
            position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
            map_east = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // west
        if (map_west == null)
        {
            position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
            map_west = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // north east
        if (map_north_east == null)
        {
            position = new Vector3(transform.position.x + width, transform.position.y + height, transform.position.z);
            map_north_east = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // north west
        if (map_north_west == null)
        {
            position = new Vector3(transform.position.x - width, transform.position.y + height, transform.position.z);
            map_north_west = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // south east
        if (map_south_east == null)
        {
            position = new Vector3(transform.position.x + width, transform.position.y - height, transform.position.z);
            map_south_east = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        // south west
        if (map_south_west == null)
        {
            position = new Vector3(transform.position.x - width, transform.position.y - height, transform.position.z);
            map_south_west = Instantiate(mapController.mapPrefab, position, transform.rotation).GetComponent<MapScript>();
        }

        map_north.map_south = gameObject.GetComponent<MapScript>();
        map_north.map_south_east = map_east;
        map_north.map_south_west = map_west;
        map_north.map_east = map_north_east;
        map_north.map_west = map_north_west;

        map_south.map_north = gameObject.GetComponent<MapScript>();
        map_south.map_north_east = map_east;
        map_south.map_north_west = map_west;
        map_south.map_east = map_south_east;
        map_south.map_west = map_south_west;

        map_east.map_west = gameObject.GetComponent<MapScript>();
        map_east.map_south_west = map_south;
        map_east.map_north_west = map_north;
        map_east.map_south = map_south_east;
        map_east.map_north = map_north_east;

        map_west.map_east = gameObject.GetComponent<MapScript>();
        map_west.map_south_east = map_south;
        map_west.map_north_east = map_north;
        map_west.map_south = map_south_west;
        map_west.map_north = map_north_west;

        map_north_west.map_south_east = gameObject.GetComponent<MapScript>();
        map_north_west.map_south = map_west;
        map_north_west.map_east = map_north;

        map_north_east.map_south_west = gameObject.GetComponent<MapScript>();
        map_north_east.map_south = map_east;
        map_north_east.map_west = map_north;

        map_south_west.map_north_east = gameObject.GetComponent<MapScript>();
        map_south_west.map_north = map_west;
        map_south_west.map_east = map_south;

        map_south_east.map_north_west = gameObject.GetComponent<MapScript>();
        map_south_east.map_north = map_east;
        map_south_east.map_west = map_south;
    }

    void DeleteMaps()
    {
        if (map_north.is_center)
        {
            map_north.map_north?.DestroySelf();
            map_north.map_north_east?.DestroySelf();
            map_north.map_north_west?.DestroySelf();

            map_north.is_center = false;
            /*
            map_south?.DestroySelf();
            map_south_east?.DestroySelf();
            map_south_west?.DestroySelf();
            */
        }
        if (map_south.is_center)
        {
            map_south.map_south?.DestroySelf();
            map_south.map_south_east?.DestroySelf();
            map_south.map_south_west?.DestroySelf();

            map_south.is_center = false;
            /*
            map_north?.DestroySelf();
            map_north_east?.DestroySelf();
            map_north_west?.DestroySelf();
            */
        }
        if (map_east.is_center)
        {
            map_east.map_east?.DestroySelf();
            map_east.map_south_east?.DestroySelf();
            map_east.map_north_east?.DestroySelf();

            map_east.is_center = false;
            /*
            map_west?.DestroySelf();
            map_south_west?.DestroySelf();
            map_north_west?.DestroySelf();
            */
        }
        if (map_west.is_center)
        {
            map_west.map_west?.DestroySelf();
            map_west.map_south_west?.DestroySelf();
            map_west.map_north_west?.DestroySelf();

            map_west.is_center = false;
            /*
            map_east?.DestroySelf();
            map_south_east?.DestroySelf();
            map_north_east?.DestroySelf();
            */
        }
        if (map_north_west.is_center)
        {
            map_north_west.map_north?.DestroySelf();
            map_north_west.map_west?.DestroySelf();
            map_north_west.map_north_east?.DestroySelf();
            map_north_west.map_north_west?.DestroySelf();
            map_north_west.map_south_west?.DestroySelf();

            map_north_west.is_center = false;
            /*
            map_south?.DestroySelf();
            map_east?.DestroySelf();
            map_north_east?.DestroySelf();
            map_south_east?.DestroySelf();
            map_south_west?.DestroySelf();
            */
        }
        if (map_north_east.is_center)
        {
            map_north_east.map_north?.DestroySelf();
            map_north_east.map_east?.DestroySelf();
            map_north_east.map_north_east?.DestroySelf();
            map_north_east.map_south_east?.DestroySelf();
            map_north_east.map_north_west?.DestroySelf();

            map_north_east.is_center = false;
            /*
            map_south?.DestroySelf();
            map_west?.DestroySelf();
            map_north_west?.DestroySelf();
            map_south_east?.DestroySelf();
            map_south_west?.DestroySelf();
            */
        }
        if (map_south_west.is_center)
        {
            map_south_west.map_south?.DestroySelf();
            map_south_west.map_west?.DestroySelf();
            map_south_west.map_north_west?.DestroySelf();
            map_south_west.map_south_east?.DestroySelf();
            map_south_west.map_south_west?.DestroySelf();

            map_south_west.is_center = false;
            /*
            map_north?.DestroySelf();
            map_east?.DestroySelf();
            map_north_east?.DestroySelf();
            map_south_east?.DestroySelf();
            map_north_west?.DestroySelf();
            */
        }
        if (map_south_east.is_center)
        {
            map_south_east.map_south?.DestroySelf();
            map_south_east.map_east?.DestroySelf();
            map_south_east.map_north_east?.DestroySelf();
            map_south_east.map_south_east?.DestroySelf();
            map_south_east.map_south_west?.DestroySelf();

            map_south_east.is_center = false;
            /*
            map_north?.DestroySelf();
            map_west?.DestroySelf();
            map_north_east?.DestroySelf();
            map_north_west?.DestroySelf();
            map_south_west?.DestroySelf();
            */
        }
    }

    public void DestroySelf()
    {
        /*
        if (gameObject.IsDestroyed()) return;
        Debug.Log($"destroy {gameObject.name}");
        */
            Destroy(gameObject);
    }
}
