using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static ShopPanelScript;

public class Player : MonoBehaviour, IDamagable, IAttacker
{
    MainController mainController;
    BackPackController backPackController;

    private PlayerInputControls controls;

    public SpriteRenderer weapon_sprite_renderer;

    public GameObject owner => gameObject;

    // Player's Data
    [Header("References")]
    [field: SerializeField] public PlayerSO Data;

    [Header("Animations")]
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRender;

    private Vector2 moveVector;

    float direction = 0;
    float moveSpeed;
    //float attackRange;

    [Header("Stats")]
    public int max_level = 100;
    public int current_level = 1;

    [SerializeField] float player_start_health;
    [SerializeField] float player_start_attack = 0f;
    [SerializeField] float player_start_crit_chance = 0f;
    [SerializeField] float player_start_crit_dmg = 0f;
    [SerializeField] float player_start_defence = 0f;
    [SerializeField] float player_start_elementsl_mastery = 0f;

    protected float current_hit_damage;
    public bool wasCrit;
    public Damage currentDmg => new Damage(current_hit_damage, current_weapon.elementalDamage, wasCrit);
    bool canHit = true;

    public Stats player_full_stats;
    public Stats current_stats;
    public Stats boost_stats;
    public Stats current_hit_stats;

    [Header("Other stuff")]
    [SerializeField] float attackCooldown;

    [SerializeField] GameObject target;

    //public WeaponSO weaponSO;
    public Weapon current_weapon;

    public List<WeaponSO> weaponSOs;
    public List<Weapon> weapons;

    public GameObject pivot;

    public float upgrade_percent = 1.05f;
    public Cost upgrate_cost;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();

        upgrate_cost = new Cost(750, CostType.Gold);

        boost_stats = new Stats();

        controls = new PlayerInputControls();

        controls.PlayerKeyboardInput.SwitchWeapon_1.performed += _ => SwitchWeapon(0);
        controls.PlayerKeyboardInput.SwitchWeapon_2.performed += _ => SwitchWeapon(1);
    }

    void Start()
    {
        //player_full_stats = new Stats(player_start_health, player_start_attack, player_start_crit_chance, player_start_crit_dmg, player_start_defence, player_start_elementsl_mastery);
        player_full_stats = new Stats();

        ApplyConfig();

        current_stats = new Stats(player_full_stats);
        current_hit_stats = new Stats(player_full_stats);

        //current_weapon = null;
        weapons = new List<Weapon>();

        foreach (WeaponSO weaponSO in weaponSOs)
        {
            Item temp_item = mainController.GetItemByName(weaponSO.weapon_name);
            if (temp_item is Weapon temp_weapon)
            {
                weapons.Add(temp_weapon);
                mainController.SetCharacterWeapon(temp_weapon);
            }
        }
        current_weapon = weapons[0];
        GivePlayerNewWeapon(current_weapon);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void SwitchWeapon(int index)
    {
        Debug.Log("Switch to weapon " + index);
        GivePlayerNewWeapon(weapons[index]);
    }

    public void GivePlayerNewWeapon(Weapon new_weapon)
    {
        current_weapon = new_weapon;
        weapon_sprite_renderer.sprite = new_weapon.sprite;

        current_stats = new Stats(current_stats, current_weapon.stats);
    }

    public void DealDamage()
    {
        current_hit_stats.attack = current_stats.attack + boost_stats.attack;
        current_hit_stats.crit_chance = current_stats.crit_chance + boost_stats.crit_chance;
        current_hit_stats.crit_dmg = current_stats.crit_chance + boost_stats.crit_dmg;

        int delta_damage = 0;

        System.Random rand = new System.Random();
        int chance = rand.Next(0, 101);
        wasCrit = false;
        if (chance <= current_hit_stats.crit_chance * 100)
        {
            delta_damage += RoundToMax(current_hit_stats.attack * current_hit_stats.crit_dmg);
            wasCrit = true;
        }

        this.current_hit_damage = current_hit_stats.attack + delta_damage;

        LoggerName($"now have {this.current_hit_damage} damage");
    }

    public void CharacterUpgrade()
    {
        this.player_full_stats.health = RoundToMax(this.player_full_stats.health * upgrade_percent);
        this.player_full_stats.attack = RoundToMax(this.player_full_stats.attack * upgrade_percent);
        this.player_full_stats.crit_chance *= upgrade_percent;
        player_full_stats.crit_dmg *= upgrade_percent;
        //this.defence *= upgrade_percent;

        current_stats = new Stats(player_full_stats, current_weapon.stats);

        this.current_level++;
    }

    public void BoostCharacter(UseEffect useEffect)
    {
        Debug.Log($"BOOST CHARACTER {useEffect.useType}, +{useEffect.use_percent_from_0_to_100}%");
        switch (useEffect.useType)
        {
            case UseType.Health:
                current_stats.health += (player_full_stats.health * useEffect.use_percent_from_0_to_100 / 100f);
                if (current_stats.health > player_full_stats.health)
                {
                    current_stats.health = player_full_stats.health;
                }
                UpdateHealthBar();
                break;
            case UseType.Attack:
                boost_stats.attack = (current_stats.attack * useEffect.use_percent_from_0_to_100 / 100f);
                break;
            case UseType.CritChance:
                boost_stats.crit_chance = useEffect.use_percent_from_0_to_100 / 100f;
                break;
            case UseType.CritDMG:
                boost_stats.crit_dmg = useEffect.use_percent_from_0_to_100 / 100f;
                break;
            case UseType.ElementalMastery:
                boost_stats.elementsl_mastery = useEffect.use_percent_from_0_to_100 / 100f;
                break;
        }
        StartCoroutine(RemoveBoostAfterSeconds(useEffect));
    }

    private IEnumerator RemoveBoostAfterSeconds(UseEffect useEffect)
    {
        yield return new WaitForSeconds(useEffect.time_duration_seconds);

        switch (useEffect.useType)
        {
            case UseType.Attack:
                boost_stats.attack = 0;
                break;
            case UseType.CritChance:
                boost_stats.crit_chance = 0;
                break;
            case UseType.CritDMG:
                boost_stats.crit_dmg = 0;
                break;
            case UseType.ElementalMastery:
                boost_stats.elementsl_mastery = 0;
                break;
        }
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
        return current_stats.health;
    }

    private void ApplyConfig()
    {
        if (Data == null)
        {
            Debug.LogWarning($"{name} has no PlayerConfig!");
            return;
        }

        /*
        player_max_health = Data.maxHealth;
        player_current_health = player_max_health;
        player_current_attack = Data.damage;
        */

        player_full_stats.health = Data.maxHealth;
        player_full_stats.attack = Data.damage;

        moveSpeed = Data.moveSpeed;

        //attackRange = Data.attackRange;
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
                    direction = 4f - ((angle - 90f) / 45f);
                }
                else
                {
                    direction = ((angle + 90) % 360) / 45;
                    spriteRender.flipX = false;
                }
            }
            else
            {
                anim.SetBool("isRun", false);
            }
            anim.SetFloat("dir", direction);  // вынести в отделную функцию
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
        //Debug.Log($"{name} massage: {s}");
    }

    public void TakeDamage(Damage dmg)
    {
        //LoggerName($"took dmg = {dmg.damage}");
        current_stats.health -= dmg.damage;

        UpdateHealthBar();
        MusicManager.Instance.PlayByIndex(1);

        //Debug.LogWarning($"Player have taken a dmg and now he has {current_stats.health} health was {current_stats.health + dmg.damage}");
        if (current_stats.health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        mainController.UpdateHealthBar(current_stats.health / player_full_stats.health);
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

public class Stats
{
    public float health = 0f;
    public float attack = 0f;
    public float crit_chance = 0f;
    public float crit_dmg = 0f;
    public float defence = 0f;
    public float elementsl_mastery = 0f;

    public Stats(Stats stats)
    {
        this.health = stats.health;
        this.attack = stats.attack;
        this.crit_chance = stats.crit_chance;
        this.crit_dmg = stats.crit_dmg;
        this.defence = stats.defence;
        this.elementsl_mastery = stats.elementsl_mastery;
    }

    public Stats(Stats stats1, Stats stats2)
    {
        this.health = stats1.health + stats2.health;
        this.attack = stats1.attack + stats2.attack;
        this.crit_chance = stats1.crit_chance + stats2.crit_chance;
        this.crit_dmg = stats1.crit_dmg + stats2.crit_dmg;
        this.defence = stats1.defence + stats2.defence;
        this.elementsl_mastery = stats1.elementsl_mastery + stats2.elementsl_mastery;
    }

    public Stats(float health = 0, float attack = 0, float crit_chance = 0, float crit_dmg = 0, float defence = 0, float elementsl_mastery = 0)
    {
        this.health = health;
        this.attack = attack;
        this.crit_chance = crit_chance;
        this.crit_dmg = crit_dmg;
        this.defence = defence;
        this.elementsl_mastery = elementsl_mastery;
    }
}
