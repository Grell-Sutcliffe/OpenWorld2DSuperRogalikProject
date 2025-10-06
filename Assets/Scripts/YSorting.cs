using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class YSorter : MonoBehaviour
{
    public int baseOrder = 0;
    public int mul = 100;

    Renderer r;

    void Awake() => r = GetComponent<Renderer>();

    void LateUpdate()
    {
        r.sortingOrder = baseOrder - Mathf.RoundToInt(transform.position.y * mul);
    }
}
