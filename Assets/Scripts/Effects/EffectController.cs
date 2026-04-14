using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject BOOM_Pref;
    [SerializeField] GameObject Spread_Pref;
    public static EffectController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnBOOM(Transform transform, Damage dmg, float radius = 3)
    {
        Debug.Log($"Спавн взрыва с {dmg.element_type}");
        GameObject boomGO = Instantiate(
            BOOM_Pref,
            transform.position,
            Quaternion.Euler(0, 0, 0)
        );
        var boom = boomGO.GetComponent<BOOM>();
        boom.Init(dmg, radius);
    }

}
