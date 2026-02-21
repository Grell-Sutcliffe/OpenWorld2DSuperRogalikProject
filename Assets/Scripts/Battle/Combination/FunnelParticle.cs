using UnityEngine;

public class FunnelParticle : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float destroyDistance = 0.2f;

    public void Init(Transform target, float speed, float destDist)
    {
        this.target = target;
        this.speed = speed;
        this.destroyDistance = destDist;
        SpriteRenderer sr = GetComponent<SpriteRenderer>(); 
        sr.color = Color.red;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
