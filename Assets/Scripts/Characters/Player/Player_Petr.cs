using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable, IAttacker
{
    MainController mainController;
    public GameObject owner => gameObject;
    protected float current_dmg;
    public Damage currentDmg => new Damage(current_dmg);


    // Player's Data
    [Header("References")]
    [field: SerializeField] public PlayerSO Data;


    [Header("Animations")]


    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRender;


    private Vector2 moveVector;

    float dir = 0;
    string playerName;
    [SerializeField] float maxHealth;
    float armour;
    float damage;
    float moveSpeed;

    float attackRange;
    float attackCooldown;

    float currentHealth;

    float crit_chance = 0.7f;
    float crit_dmg = 2.5f;

    [SerializeField] GameObject target;

    public void DealDamage()
    {
        int delta_damage = 0;

        System.Random rand = new System.Random();
        int chance = rand.Next(0, 101);

        if (chance <= crit_chance * 100)
        {
            delta_damage += RoundToMax(damage * crit_dmg);
        }

        this.current_dmg = damage + delta_damage;
    }

    public void UnActivePivot(){}
    public void StartDelay(){}
    public Transform GetTarget()
    {
        return target.transform;
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //spriteRender = GetComponent<SpriteRenderer>();
    }
        /*
        // §ª§ß§Ú§è§Ú§Ñ§Ý§Ú§Ù§Ñ§è§Ú§ñ §Ü§à§Þ§á§à§ß§Ö§ß§ä§à§Ó
        Input = GetComponent<PlayerInput>();
        movementStateMachine = new PlayerMovementStateMachine(this);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        AnimationData.Initialize();
    }
    public void ModifySpeed(float multiplier, float duration)
    {
        StartCoroutine(SpeedCoroutine(multiplier, duration));
    }
    
    private IEnumerator SpeedCoroutine(float multiplier, float duration) // what if someone handle two effect on the player???
    {
        var data = movementStateMachine;
        //float old = data.EffectSpeedModifier;

        data.EffectSpeedModifier *= multiplier;
        Debug.Log(data.EffectSpeedModifier);

        yield return new WaitForSeconds(duration);

        data.EffectSpeedModifier /= multiplier;
        Debug.Log(data.EffectSpeedModifier);
    }*/
    private void ApplyConfig()
    {
        if (Data == null)
        {
            Debug.LogWarning($"{name} has no PlayerConfig!");
            return;
        }

        playerName = Data.playerName;
        maxHealth = Data.maxHealth;
        currentHealth = maxHealth;
        armour = Data.armour;
        damage = Data.damage;
        moveSpeed = Data.moveSpeed;

        attackRange = Data.attackRange;
        attackCooldown = Data.attackCooldown;
    }
    private void Start()
    {
        ApplyConfig();
    }
    bool isRun = false;

    private void Update()
    {
        if (mainController.is_keyboard_active)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 input = new Vector2(x, y);

            moveVector = input.sqrMagnitude > 1f ? input.normalized : input;

            if (input.sqrMagnitude > 0.01f)
            {
                isRun = true;
                anim.SetBool("isRun", true);
                float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
                Debug.Log($"angle = {angle} {((angle + 90) % 360) / 45}");
                

                if (angle > 90 || angle < -90)
                {
                    angle = (angle + 360) % 360;
                    spriteRender.flipX = true;
                    dir = 4f - ((angle - 90f) / 45f);

                }
                else
                {
                    dir = ((angle + 90) % 360) / 45;
                    spriteRender.flipX = false;

                }

            }
            else
            {
                anim.SetBool("isRun", false);
            }
            anim.SetFloat("dir", dir);
        }
        
    }

    private void FixedUpdate()
    {
        if (mainController.is_keyboard_active)
        {
            rb.linearVelocity = moveVector * moveSpeed;

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    protected virtual void LoggerName(string s = null)
    {
        Debug.Log($"{name} massage: {s}");
    }
    public void TakeDamage(Damage dmg)
    {
        //LoggerName($"took dmg = {dmg.damage}");
        currentHealth -= dmg.damage;

        mainController.UpdateHealthBar(currentHealth / maxHealth);

        Debug.LogWarning($"Player have taken a dmg and now he has {currentHealth} health was {currentHealth + dmg.damage}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    int RoundToMax(float number)
    {
        return ((number * 10 % 10 > 0) ? ((int)number + 1) : ((int)number));
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
