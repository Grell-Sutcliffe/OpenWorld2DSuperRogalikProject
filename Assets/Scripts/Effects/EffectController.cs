using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject BOOM_Pref;
    [SerializeField] GameObject Spread_Pref;
    [SerializeField] GameObject Funnel_Pref;
    public static EffectController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public void SpawnFunnel(Transform transform, Damage dmg, float radius = 5)
    {
        Debug.Log($"Спавн волонки с {dmg.element_type}");
        GameObject funnelGO = Instantiate(
            Funnel_Pref,
            new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z),
            Quaternion.Euler(0, 0, 0)
        );
        var funnel = funnelGO.GetComponent<Funnel>();
        funnel.Init(dmg,3,3, radius);
    }
    public void SpawnBOOM(Transform transform, Damage dmg, float radius = 3)
    {
        Debug.Log($"Спавн взрыва с {dmg.element_type}");
        GameObject boomGO = Instantiate(
            BOOM_Pref,
            new Vector3(transform.position.x, transform.position.y+0.3f, transform.position.z),
            Quaternion.Euler(0, 0, 0)
        );
        var boom = boomGO.GetComponent<BOOM>();
        boom.Init(dmg, radius);
    }
    public void SpawnSpread(Transform transform, Damage dmg, float radius = 5)
    {
        Debug.Log($"Спавн разлития с {dmg.element_type}");
        GameObject spreadGO = Instantiate(
            Spread_Pref,
            new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z),
            Quaternion.Euler(0, 0, 0)
        );
        var spread = spreadGO.GetComponent<Spreading>();
        spread.Init(dmg, radius);
    }
}
