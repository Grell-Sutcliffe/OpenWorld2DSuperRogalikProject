using UnityEngine;

public class EmenyYSorting : MonoBehaviour
{
    public int baseOrder = 0;
    public int mul = 100;

    Renderer r;
    public Renderer effectController;

    void Awake() => r = GetComponent<Renderer>();

    void LateUpdate()
    {
        r.sortingOrder = baseOrder - Mathf.RoundToInt(transform.position.y * mul);
        effectController.sortingOrder = r.sortingOrder + 1;
    }
}
