using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    MainController mainController;

    // Player's Data
    [Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    // Component for getting player input
    public PlayerInput Input { get; private set; }

    [Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }


    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator PlayerAnimator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    // §¡§Ó§ä§à§Þ§Ñ§ä §ã§à§ã§ä§à§ñ§ß§Ú§Û §Õ§Ý§ñ §å§á§â§Ñ§Ó§Ý§Ö§ß§Ú§ñ §Õ§Ó§Ú§Ø§Ö§ß§Ú§ñ §Ú§Ô§â§à§Ü§Ñ
    private PlayerMovementStateMachine movementStateMachine;


    string playerName;
    float maxHealth;
    float armour;
    float damage;
    float moveSpeed;

    float attackRange;
    float attackCooldown;

    float currentHealth;

    public float GetHealth()
    {
        return currentHealth;
    }
    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

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
    }
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
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        ApplyConfig();
    }

    private void Update()
    {
        if (mainController.is_keyboard_active)
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update();
        }
    }

    private void FixedUpdate()
    {
        if (mainController.is_keyboard_active)
        {
            movementStateMachine.PhysicsUpdate();
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        mainController.UpdateHealthBar(currentHealth / maxHealth);

        Debug.Log($"Player have taken a dmg and now he has {currentHealth} health was {currentHealth + dmg}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
