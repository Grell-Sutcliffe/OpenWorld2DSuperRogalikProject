using UnityEngine;

public class PlayerSorting : MonoBehaviour
{
    Player playerScript;

    public int baseOrder = 0;
    public int mul = 100;

    public GameObject player_GO;

    public GameObject pivot_1_GO;
    public GameObject pivot_2_GO;

    Renderer r;

    Renderer pivot_1;
    Renderer pivot_2;

    void Awake()
    {
        playerScript = GetComponent<Player>();

        r = player_GO.GetComponent<Renderer>();

        pivot_1 = pivot_1_GO.GetComponent<Renderer>();
        pivot_2 = pivot_2_GO.GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        r.sortingOrder = baseOrder - Mathf.RoundToInt(transform.position.y * mul);

        if (playerScript.direction == 3 || playerScript.direction == 4)
        {
            pivot_1.sortingOrder = r.sortingOrder - 1;
            pivot_2.sortingOrder = r.sortingOrder - 1;
        }
        else
        {
            pivot_1.sortingOrder = r.sortingOrder + 1;
            pivot_2.sortingOrder = r.sortingOrder + 1;
        }
    }
}
