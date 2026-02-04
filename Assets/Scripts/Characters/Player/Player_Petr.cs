using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static ShopPanelScript;

public class Player : MonoBehaviour, IDamagable, IAttacker
{
    MainController mainController;
    BackPackController backPackController;

    public SpriteRenderer weapon_sprite_renderer;

    public GameObject owner => gameObject;
    protected float current_dmg;
    public bool wasCrit;

    public Damage currentDmg => new Damage(current_dmg, weapon.elementalDamage, wasCrit);
    bool canHit = true;

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
    public float maxHealth;
    float armour;
    public float damage;
    float moveSpeed;

    public int max_level = 100;
    public int current_level = 1;

    float attackRange;
    [SerializeField]  float attackCooldown;

    float currentHealth;

    public float crit_chance = 0.7f;
    public float crit_dmg = 1.5f;


    public float defence = 0.15f;

    [SerializeField] GameObject target;

    public WeaponSO weaponSO;
    public Weapon weapon;

    public GameObject pivot;

    public float upgrade_percent = 1.05f;

    public Cost upgrate_cost;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();

        upgrate_cost = new Cost(750, CostType.Gold);
    }

    void Start()
    {
        ApplyConfig();

        //weapon = new Weapon(weaponSO);
        weapon = null;
        Item temp_item = mainController.GetItemByName(weaponSO.weapon_name);
        if (temp_item is Weapon temp_weapon)
        {
            //Debug.LogError("TEMP_WEAPON --- OK");
            weapon = temp_weapon;
        }
        //Debug.LogError($"weapon == null : {weapon == null}");

        mainController.SetCharacterWeapon(weapon);
        GivePlayerNewWeapon(weapon);
    }

    public void GivePlayerNewWeapon(Weapon new_weapon)
    {
        weapon = new_weapon;
        weapon_sprite_renderer.sprite = new_weapon.sprite;
    }

    public void DealDamage()
    {
        float current_damage = damage + weapon.damage;
        float current_crit_chance = crit_chance + weapon.crit_chance;
        float current_crit_dmg = crit_dmg + weapon.crit_dmg;

        int delta_damage = 0;

        System.Random rand = new System.Random();
        int chance = rand.Next(0, 101);
        wasCrit = false;
        if (chance <= current_crit_chance * 100)
        {
            delta_damage += RoundToMax(current_damage * crit_dmg);
            wasCrit = true;
        }

        this.current_dmg = current_damage + delta_damage;

        LoggerName($"now have {current_dmg} damage");
    }

    public void CharacterUpgrade()
    {
        this.maxHealth = RoundToMax(this.maxHealth * upgrade_percent);
        this.damage = RoundToMax(this.damage * upgrade_percent);
        this.crit_chance *= upgrade_percent;
        this.crit_dmg *= upgrade_percent;
        //this.defence *= upgrade_percent;

        this.current_level++;
    }

    public void UnActivePivot(){
        pivot.gameObject.SetActive(false);
    }

    protected virtual IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        canHit = true;
    }
    public void StartDelay()
    {

        isHit = false;

        StartCoroutine(Delay(attackCooldown));
    }

    public Transform GetTarget()
    {
        return target.transform;
    }
    public float GetHealth()
    {
        return currentHealth;
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

    bool isRun = false;
    bool isHit = false;
    float offset = 0;
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
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
                //Debug.Log($"angle = {angle} {((angle + 90) % 360) / 45}");
                

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
            anim.SetFloat("dir", dir);  // вынести в отделную функцию
            anim.SetBool("isHit", isHit);
        }

        if (canHit && Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RotatePivot(mouseWorldPos, offset);
            Hit();
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
        MusicManager.Instance.PlayByIndex(1);

        Debug.LogWarning($"Player have taken a dmg and now he has {currentHealth} health was {currentHealth + dmg.damage}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Hit()
    {   
        DealDamage();
        isHit = true;
        canHit = false;
        pivot.gameObject.SetActive(true);
    }
    public int RoundToMax(float number)
    {
        return ((number * 10 % 10 > 0) ? ((int)number + 1) : ((int)number));
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void RotatePivot(Vector2 mousePos, float offs = 0f)
    {
        Vector2 dir = ((Vector2)mousePos - (Vector2)pivot.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2) поворачиваем pivot меча
        pivot.transform.rotation = Quaternion.Euler(0, 0, angle - offs); // оффсет под спрайт
    }
}
