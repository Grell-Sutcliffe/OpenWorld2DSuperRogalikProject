using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    [SerializeField] private float pullForce = 2f;
    [SerializeField] private float damagePerDelay = 2f;
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float particleSpeed = 1.0f;
    [SerializeField] private float frequency = 0.1f;
    [SerializeField] private float timeLife = 5f;

    [SerializeField] private GameObject particlePrefab; 

    CircleCollider2D circleCollider;
    Color color;
    bool isDelay;
    private HashSet<IDamagable> damagedThisCycle = new HashSet<IDamagable>();

    Damage damage;
    public void Init(Damage dmg, float forse = 3, float del = 1f, float radius = 3, float PS = 5, float freq = 0.05f, float time = 5)
    {
        circleCollider = GetComponent<CircleCollider2D>();
        pullForce = forse;
        damage = dmg;
        delay = del;
        circleCollider.radius = radius;
        particleSpeed = PS;
        frequency = freq;
        timeLife = time;
        switch (dmg.element_type)
        {
            case ElementType.Cryo:
                color = Color.cyan;
                break;
            case ElementType.Pyro:
                color = Color.yellow;
                break;
            case ElementType.Energo:
                color = Color.magenta;
                break;
            case ElementType.Floro:
                color = Color.green;
                break;
        }
    }
    private void Start()
    {
        if (circleCollider == null) circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Spawn());
        Destroy(gameObject, timeLife);
    }
    private bool isCycleActive = false;

    private void OnTriggerStay2D(Collider2D collision)
    {

        var enemy = collision.GetComponent<EnemyAbstract>();
        Debug.Log(enemy);
        if (enemy != null)
        {
            Vector2 direction = (Vector2)transform.position - (Vector2)collision.transform.position;
            float distance = direction.magnitude;
            if (distance < 1) return;
            float force = pullForce / Mathf.Max(distance, 0.5f);

            enemy.externalForce += direction.normalized * force;
        }

        // Урон — каждый враг получает независимо
        var damageable = collision.GetComponent<IDamagable>();
        if (damageable == null)
            damageable = collision.GetComponentInParent<IDamagable>();

        if (damageable != null && !damagedThisCycle.Contains(damageable))
        {
            if (damage == null) damage = new Damage(1, 0, ElementType.Physical);

            damageable.TakeDamage(damage);
            damagedThisCycle.Add(damageable);

            if (!isCycleActive)
            {
                StartCoroutine(DamageCycle());
            }
        }
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            SpawnParticle();
            yield return new WaitForSeconds(frequency);
        }
    }
    private void SpawnParticle()
    {
        Debug.Log("spawn");
        Vector2 randomPoint = Random.insideUnitCircle * circleCollider.radius;
        Vector2 spawnPos = (Vector2)transform.position + randomPoint;

        float distToCenter = randomPoint.magnitude;
        float destroyDist = Random.Range(0.1f, Mathf.Max(distToCenter, 0.15f));

        Vector2 dir = ((Vector2)transform.position - spawnPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject particle = Instantiate(
            particlePrefab,
            spawnPos,
            Quaternion.Euler(0, 0, angle + 90f),
            transform
        );

        var fp = particle.GetComponent<FunnelParticle>();
        fp.Init(transform, particleSpeed, destroyDist, color);
    }
    private IEnumerator DamageCycle()
    {
        isCycleActive = true;
        yield return new WaitForSeconds(delay);
        damagedThisCycle.Clear();  // все могут получить урон снова
        isCycleActive = false;
    }
}
