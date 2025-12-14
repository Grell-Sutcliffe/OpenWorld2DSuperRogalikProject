using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("弹丸设置")]
    [SerializeField] private float speed = 10f;  // 弹丸速度
    [SerializeField] private float lifetime = 2f;  // 弹丸存活时间（秒）

    private Vector2 direction;  // 弹丸方向
    private float timer = 0f;   // 存活计时器

    void Start()
    {
        // 设置自动销毁，避免弹丸一直存在
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 最简单的移动方式：每帧向前移动
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // 备用计时器（以防自动销毁失效）
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    // 初始化方法（从RogueKnight调用）
    public void Initialize(Vector2 dir, float spd, float dmg)
    {
        direction = dir.normalized;  // 确保方向是单位向量
        speed = spd;

        // 让弹丸朝向移动方向
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}