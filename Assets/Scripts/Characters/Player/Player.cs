using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static ShopPanelScript;

public class Player : Creature, IDamagable, IAttacker
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

    bool canHit = true;

    public Stats player_full_stats;
    public Stats current_stats;
    public Stats boost_stats;
    public Stats current_hit_stats;

    public Damage currentDmg => new Damage(
        RoundToMax(current_hit_stats.physical_dmg),
        RoundToMax(current_hit_stats.elemental_dmg),
        weapon.element_type,
        current_hit_stats.is_physical_crit,
        current_hit_stats.is_elemental_crit
    );

    [Header("Other stuff")]
    [SerializeField] float attackCooldown;

    [SerializeField] GameObject target;

    //public WeaponSO weaponSO;
    //public Weapon weapon;

    public List<WeaponSO> weaponSOs;
    public List<Weapon> weapons;

    public GameObject pivot;
    public GameObject pivotSecond;
    public GameObject pivotFirst;

    public float upgrade_percent = 1.05f;
    public Cost upgrate_cost;

    float offset = 0;
    float usedOffset = 0;
    Vector3 localPivotPosSaved;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        localPivotPosSaved = pivot.transform.localPosition;
        usedOffset = offset;
        GameObject mainControllerGO = GameObject.Find("MainController");
        if (mainControllerGO != null)
        {

            mainController = mainControllerGO.GetComponent<MainController>();
        }
        rb = GetComponent<Rigidbody2D>();

        upgrate_cost = new Cost(750, CostType.Gold);

        boost_stats = new Stats();

        weapons = new List<Weapon>();

        foreach (WeaponSO weaponSO in weaponSOs)
        {
            Item temp_item = mainController.GetItemByName(weaponSO.weapon_name);
            if (temp_item is Weapon temp_weapon)
            {   

                weapons.Add(temp_weapon);
                //mainController.SetCharacterWeapon(temp_weapon);
            }
        }
        weapon = weapons[0];
        Debug.Log(weapon.data);
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

        for (int index = 0; index < weaponSOs.Count; index++)
        {
            Item temp_item = mainController.GetItemByName(weaponSOs[index].weapon_name);
            if (temp_item is Weapon temp_weapon)
            {
                //weapons.Add(temp_weapon);
                mainController.SetCharacterWeapon(index, temp_weapon);
            }
        }

        GivePlayerNewWeapon(weapon);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void SwitchWeapon(int index)
    {
        canHit = true;
        Debug.Log("Switch to weapon " + index);
        if (index == 0){
            pivot = pivotFirst;
            pivotSecond.gameObject.SetActive(false);

        }
        else
        {
            pivot = pivotSecond;
            pivotFirst.gameObject.SetActive(false);
        }
        SpriteRenderer sr = pivot.GetComponentInChildren<SpriteRenderer>();
        weapon_sprite_renderer = sr;
        GivePlayerNewWeapon(weapons[index]);
        Debug.Log($"Gived {weapons[index].data}");
    }

    public void SetNewWeaponOnIndex(int index, Weapon new_weapon) //bebe
    {
        weapons[index] = new_weapon;

        GivePlayerNewWeapon(new_weapon);
    }

    public void GivePlayerNewWeapon(Weapon new_weapon)
    {
        weapon = new_weapon;

        weapon_sprite_renderer.sprite = new_weapon.sprite;

        current_stats = new Stats(player_full_stats, weapon.stats);
    }

    public void DealDamage()
    {
        /*
        current_hit_stats.attack = current_stats.attack + boost_stats.attack;
        current_hit_stats.crit_chance = current_stats.crit_chance + boost_stats.crit_chance;
        current_hit_stats.crit_dmg = current_stats.crit_chance + boost_stats.crit_dmg;
        */
        UpdateStats();
    }

    void UpdateStats()
    {
        current_stats = new Stats(player_full_stats, weapon.stats);
        current_hit_stats = new Stats(current_stats, boost_stats);

        current_hit_stats.physical_dmg = current_hit_stats.physical_attack;
        current_hit_stats.elemental_dmg = current_hit_stats.elemental_attack;

        current_hit_stats.is_physical_crit = false;
        current_hit_stats.is_elemental_crit = false;

        if (CheckCritChance())
        {
            int delta_physical_damage = RoundToMax(current_hit_stats.physical_attack * current_hit_stats.crit_dmg);
            current_hit_stats.physical_dmg += delta_physical_damage;
            current_hit_stats.is_physical_crit = true;

        }
        if (CheckCritChance())
        {
            int delta_elemental_damage = RoundToMax(current_hit_stats.elemental_attack * current_hit_stats.crit_dmg);
            current_hit_stats.elemental_dmg += delta_elemental_damage;
            current_hit_stats.is_elemental_crit = true;
        }
    }

    bool CheckCritChance()
    {
        return CheckCritChance(current_hit_stats);
    }

    bool CheckCritChance(Stats temp_stats)
    {
        System.Random rand = new System.Random();
        int chance = rand.Next(0, 101);

        bool is_crit = false;

        if (chance <= temp_stats.crit_chance * 100)
        {
            //delta_damage += RoundToMax(current_hit_stats.attack * current_hit_stats.crit_dmg);
            is_crit = true;
        }

        return is_crit;
    }

    public void CharacterUpgrade()
    {
        this.player_full_stats.health = RoundToMax(this.player_full_stats.health * upgrade_percent);
        this.player_full_stats.physical_attack = RoundToMax(this.player_full_stats.physical_attack * upgrade_percent);
        this.player_full_stats.crit_chance *= upgrade_percent;
        player_full_stats.crit_dmg *= upgrade_percent;
        //this.defence *= upgrade_percent;

        current_stats = new Stats(player_full_stats, weapon.stats);

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
                boost_stats.physical_attack = (current_stats.physical_attack * useEffect.use_percent_from_0_to_100 / 100f);
                break;
            case UseType.CritChance:
                boost_stats.crit_chance = useEffect.use_percent_from_0_to_100 / 100f;
                break;
            case UseType.CritDMG:
                boost_stats.crit_dmg = useEffect.use_percent_from_0_to_100 / 100f;
                break;
            case UseType.ElementalMastery:
                boost_stats.elemental_mastery = useEffect.use_percent_from_0_to_100 / 100f;
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
                boost_stats.physical_attack = 0;
                break;
            case UseType.CritChance:
                boost_stats.crit_chance = 0;
                break;
            case UseType.CritDMG:
                boost_stats.crit_dmg = 0;
                break;
            case UseType.ElementalMastery:
                boost_stats.elemental_mastery = 0;
                break;
        }
    }

    public void UnActivePivot()
    {
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
        player_full_stats.physical_attack = Data.damage;

        moveSpeed = Data.moveSpeed;

        //attackRange = Data.attackRange;
        attackCooldown = Data.attackCooldown;
    }

    bool isRun = false;
    bool isHit = false;
    bool overMenu = false;

    bool isFasingRight = true;
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            overMenu = true;
        }
        else
        {
            overMenu = false;
        }
        //Debug.Log(mainController.is_keyboard_active);
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
        //Debug.Log($"{canHit} {Input.GetMouseButtonDown(0)} {overMenu}");
        if (canHit && Input.GetMouseButtonDown(0) && !overMenu) // ЛКМ
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mouseWorldPos.x > transform.position.x)
            {
                FlipPivotRight();
            }
            else
            {
                FlipPivotLeft();
            }
            RotatePivot(mouseWorldPos, usedOffset);
            Hit();
        }
    }
    private void FlipPivotRight()
    {
        usedOffset = offset;
        pivot.transform.localScale = Vector3.one;
        pivot.transform.localPosition = localPivotPosSaved;


    }
    private void FlipPivotLeft()
    {
        usedOffset = offset + 180;
        pivot.transform.localScale = new Vector3(-1, 1, 0);
        pivot.transform.localPosition = new Vector3(-localPivotPosSaved.x, localPivotPosSaved.y, localPivotPosSaved.z);

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
        current_stats.health -= (dmg.physical_dmg + dmg.elemental_dmg);

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
        //Debug.Log(pivot);
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
