using System.Collections;
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

    bool isDelay;
    Damage damage;
    public void Init(Damage dmg, float forse, float del, float radius, float PS, float freq, float time)
    {
        circleCollider = GetComponent<CircleCollider2D>();
        pullForce = forse;
        damage = dmg;
        delay = del;
        circleCollider.radius = radius;
        particleSpeed = PS;
        frequency = freq;
        timeLife = time;
    }
    private void Start()
    {
        if (circleCollider == null) circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Spawn());
        Destroy(gameObject, timeLife);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        Rigidbody2D targetRb = collision.GetComponent<Rigidbody2D>();
        if (targetRb != null)
        {
            Vector2 direction = (Vector2)transform.position - targetRb.position;
            float distance = direction.magnitude;

            float force = pullForce / Mathf.Max(distance, 0.5f);

            targetRb.AddForce(direction.normalized * force);
        }

        var damageable = collision.GetComponent<IDamagable>();
        if (damageable == null)
            damageable = collision.GetComponentInParent<IDamagable>();

        if (damageable != null && !isDelay)
        {   if (damage == null)
            {
                damage = new Damage(1, 1, ElementType.Physical);
            }
            damageable.TakeDamage(damage);
            StartCoroutine(Delay());
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
        fp.Init(transform, particleSpeed, destroyDist);
    }
    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delay);
        isDelay = false;

    }
}
