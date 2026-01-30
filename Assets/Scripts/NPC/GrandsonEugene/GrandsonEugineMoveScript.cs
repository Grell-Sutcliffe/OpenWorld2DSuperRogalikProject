using UnityEngine;
using UnityEngine.Rendering;

public class GrandsonEugineMoveScript : MonoBehaviour
{
    public Transform target;

    public GameObject root;

    public float speed = 1f;
    public float stop_distance = 2f;
    public bool rotate_towards = false;

    public bool need_to_move = false;

    private Animator animator;

    Rigidbody2D rb;

    string is_F = "is_F";
    string is_RF = "is_RF";
    string is_R = "is_R";
    string is_RB = "is_RB";
    string is_B = "is_B";

    string current_direction;

    bool is_right;

    SpriteRenderer sprite;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {
        animator = root.GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = root.GetComponent<SpriteRenderer>();

        current_direction = is_F;
    }

    void FixedUpdate()
    {
        if (!target || !need_to_move) return;

        Vector2 toTarget = (Vector2)target.position - rb.position;
        float dist = toTarget.magnitude;
        if (dist <= stop_distance)
        {
            animator.SetBool("is_walking", false);
            return;
        }

        animator.SetBool("is_walking", true);

        Vector2 step = toTarget.normalized * speed * Time.fixedDeltaTime;

        SetDirection(step);

        rb.MovePosition(rb.position + step);

        //if (rotate_towards && step.sqrMagnitude > 0f) rb.rotation = Mathf.Atan2(step.y, step.x) * Mathf.Rad2Deg;
    }

    public void SetMoveToPlayer(bool new_bool)
    {
        need_to_move = new_bool;
    }

    public void StartMoveToPlayer()
    {
        need_to_move = true;
    }

    public void StopMoveToPlayer()
    {
        need_to_move = false;
    }

    void SetDirection(Vector2 vector)
    {
        SetDirection(GetAngle(vector));
    }

    void SetDirection(float angle)
    {
        if (angle <= 180)
        {
            if (is_right)
            {
                Flip();
                is_right = false;
            }
        }
        else
        {
            if (!is_right)
            {
                Flip();
                is_right = true;
            }

            angle = 360 - angle;
        }

        if (angle < 22.5)
        {
            if (current_direction != is_B)
            {
                current_direction = is_B;
                SetDirection(is_B);
            }
        }
        else if (angle < 67.5)
        {
            if (current_direction != is_RB)
            {
                current_direction = is_RB;
                SetDirection(is_RB);
            }
        }
        else if (angle < 112.5)
        {
            if (current_direction != is_R)
            {
                current_direction = is_R;
                SetDirection(is_R);
            }
        }
        else if (angle < 157.5)
        {
            if (current_direction != is_RF)
            {
                current_direction = is_RF;
                SetDirection(is_RF);
            }
        }
        else
        {
            if (current_direction != is_F)
            {
                current_direction = is_F;
                SetDirection(is_F);
            }
        }
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        //sprite.flipY = !sprite.flipY;
    }

    float GetAngle(Vector2 vector)
    {
        float angleRad = Mathf.Atan2(vector.y, vector.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        float angle = (90f - angleDeg + 360f) % 360f;

        return angle;
    }

    void SetDirection(string direction)
    {
        SetAllDirectionsFalse();
        animator.SetBool(direction, true);
    }

    void SetAllDirectionsFalse()
    {
        animator.SetBool("is_F", false);
        animator.SetBool("is_RF", false);
        animator.SetBool("is_R", false);
        animator.SetBool("is_RB", false);
        animator.SetBool("is_B", false);
    }
}
