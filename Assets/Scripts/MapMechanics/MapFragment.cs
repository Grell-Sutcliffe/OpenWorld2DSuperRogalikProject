using UnityEngine;

public class MapFragment : MonoBehaviour
{
    private void OnEnable()
    {
        MapFragmentRegistry.Register(this);
    }

    private void OnDisable()
    {
        MapFragmentRegistry.Deregister(this);
    }
}
