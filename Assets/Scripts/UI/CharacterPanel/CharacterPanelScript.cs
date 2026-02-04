using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelScript : MonoBehaviour
{
    public class Element
    {
        public ElementType type;
        public string name;
        public Sprite sprite;

        public Element (ElementType type, string name, Sprite sprite)
        {
            this.type = type;
            this.name = name;
            this.sprite = sprite;
        }
    }

    public Image goldImage;
    public TextMeshProUGUI goldTMP;
    
    public GameObject characterPanel;
    public GameObject weaponPanel;
    public GameObject characterUpgratePanel;

    public Image costImage;
    public TextMeshProUGUI costTMP;
    
    Player playerScript;
    CurrentWeaponPanelScript currentWeaponPanelScript;
    public BackPackController backpackController;
    public ShopPanelScript shopPanelScript;

    public Image characterImage;

    [Header("’арактеристики")]
    public TextMeshProUGUI health_TMP;
    public TextMeshProUGUI attack_TMP;
    public TextMeshProUGUI crit_chance_TMP;
    public TextMeshProUGUI crit_dmg_TMP;
    public TextMeshProUGUI elemental_mastery_TMP;
    public TextMeshProUGUI defence_TMP;

    [Header("”лучшить характеристики OLD")]
    public TextMeshProUGUI upgrate_old_level_TMP;
    public TextMeshProUGUI upgrate_old_health_TMP;
    public TextMeshProUGUI upgrate_old_attack_TMP;
    public TextMeshProUGUI upgrate_old_crit_chance_TMP;
    public TextMeshProUGUI upgrate_old_crit_dmg_TMP;
    public TextMeshProUGUI upgrate_old_elemental_mastery_TMP;
    public TextMeshProUGUI upgrate_old_defence_TMP;

    [Header("”лучшить характеристики NEW")]
    public TextMeshProUGUI upgrate_new_level_TMP;
    public TextMeshProUGUI upgrate_new_health_TMP;
    public TextMeshProUGUI upgrate_new_attack_TMP;
    public TextMeshProUGUI upgrate_new_crit_chance_TMP;
    public TextMeshProUGUI upgrate_new_crit_dmg_TMP;
    public TextMeshProUGUI upgrate_new_elemental_mastery_TMP;
    public TextMeshProUGUI upgrate_new_defence_TMP;

    [Header("Ёлементы")]
    public Dictionary<ElementType, Element> dict_element_type_to_element;

    public Sprite sprite_cryo;
    public Sprite sprite_pyro;
    public Sprite sprite_electro;
    public Sprite sprite_anemo;
    public Sprite sprite_physical;

    public string name_cryo = "cryo";
    public string name_pyro = "pyro";
    public string name_electro = "electro";
    public string name_anemo = "anemo";
    public string name_physical = "physical";

    void Awake()
    {
        currentWeaponPanelScript = weaponPanel.GetComponent<CurrentWeaponPanelScript>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
        //shopPanelScript = GameObject.Find("ShopPanel").GetComponent<ShopPanelScript>();

        characterUpgratePanel.SetActive(false);

        MakeDictionary();
    }

    void Start()
    {
        //GoToCharacterPanel();
    }

    public void UpdatePanel()
    {
        if (currentWeaponPanelScript == null) currentWeaponPanelScript = weaponPanel.GetComponent<CurrentWeaponPanelScript>();
        if (playerScript == null) playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Debug.LogError($"playerScript.weapon = {playerScript.weapon}");

        //Debug.LogError($"playerScript.weapon = {playerScript.weapon} name = {playerScript.weapon.item_name}");
        health_TMP.text = RoundToMax(playerScript.maxHealth).ToString();
        attack_TMP.text = RoundToMax(playerScript.damage + playerScript.weapon.damage).ToString();
        crit_chance_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.crit_chance + playerScript.weapon.crit_chance);
        crit_dmg_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.crit_dmg + playerScript.weapon.crit_dmg);
        elemental_mastery_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.weapon.elementalDamage.elemental_mastery);
        defence_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.defence);
    }

    public void OpenCharacterUpgradePanel()
    {
        UpdateGoldAmount();

        characterUpgratePanel.SetActive(true);

        costImage.sprite = shopPanelScript.dict_costType_to_Item[playerScript.upgrate_cost.cost_type].sprite;
        costTMP.text = playerScript.upgrate_cost.cost_amount.ToString();

        upgrate_old_level_TMP.text = playerScript.current_level.ToString();
        upgrate_new_level_TMP.text = (playerScript.current_level + 1).ToString();

        upgrate_old_health_TMP.text = playerScript.maxHealth.ToString();
        upgrate_new_health_TMP.text = (RoundToMax(playerScript.maxHealth * playerScript.upgrade_percent)).ToString();

        upgrate_old_attack_TMP.text = (playerScript.damage + playerScript.weapon.damage).ToString();
        upgrate_new_attack_TMP.text = (RoundToMax(playerScript.damage * playerScript.upgrade_percent + playerScript.weapon.damage)).ToString();

        upgrate_old_crit_chance_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.crit_chance + playerScript.weapon.crit_chance);
        upgrate_new_crit_chance_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.crit_chance * playerScript.upgrade_percent + playerScript.weapon.crit_chance);

        upgrate_old_crit_dmg_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.crit_dmg + playerScript.weapon.crit_dmg);
        upgrate_new_crit_dmg_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.crit_dmg * playerScript.upgrade_percent + playerScript.weapon.crit_dmg);

        upgrate_old_elemental_mastery_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.weapon.elementalDamage.elemental_mastery);
        upgrate_new_elemental_mastery_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.weapon.elementalDamage.elemental_mastery);
        
        upgrate_old_defence_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.defence);
        upgrate_new_defence_TMP.text = currentWeaponPanelScript.FloatToString(playerScript.defence);
    }

    public void CharacterUpgrade()
    {
        if (playerScript.upgrate_cost.cost_amount <= shopPanelScript.dict_costType_to_Item[playerScript.upgrate_cost.cost_type].amount)
        {
            backpackController.DecreaceItemByName(shopPanelScript.dict_costType_to_Item[playerScript.upgrate_cost.cost_type].item_name, playerScript.upgrate_cost.cost_amount);
            playerScript.CharacterUpgrade();
            OpenCharacterUpgradePanel();
            UpdatePanel();
        }
    }

    public void GivePlayerNewWeapon()
    {
        //playerScript.weapon = currentWeaponPanelScript.weapon;
        playerScript.GivePlayerNewWeapon(currentWeaponPanelScript.weapon);
    }

    void MakeDictionary()
    {
        dict_element_type_to_element = new Dictionary<ElementType, Element>();

        dict_element_type_to_element[ElementType.Cryo] = new Element(ElementType.Cryo, name_cryo, sprite_cryo);
        dict_element_type_to_element[ElementType.Pyro] = new Element(ElementType.Pyro, name_pyro, sprite_pyro);
        dict_element_type_to_element[ElementType.Electro] = new Element(ElementType.Electro, name_electro, sprite_electro);
        dict_element_type_to_element[ElementType.Anemo] = new Element(ElementType.Anemo, name_anemo, sprite_anemo);
        dict_element_type_to_element[ElementType.Physical] = new Element(ElementType.Physical, name_physical, sprite_physical);
    }

    public void SetNewWeapon(Weapon new_weapon)
    {
        currentWeaponPanelScript.SetNewWeapon(new_weapon);
        GivePlayerNewWeapon();
        UpdatePanel();
    }

    void UpdateGoldAmount()
    {
        goldImage.sprite = shopPanelScript.dict_costType_to_Item[CostType.Gold].sprite;
        goldTMP.text = shopPanelScript.dict_costType_to_Item[CostType.Gold].amount.ToString();
    }

    public void OpenCharacterPanel()
    {
        gameObject.SetActive(true);
        GoToCharacterPanel();
    }

    public void GoToCharacterPanel()
    {
        characterPanel.SetActive(true);
        weaponPanel.SetActive(false);
        UpdatePanel();
    }

    public void GoToWeaponPanel()
    {
        characterPanel.SetActive(false);
        //weaponPanel.SetActive(true);
        currentWeaponPanelScript.OpenPanel();
    }

    public int RoundToMax(float number)
    {
        return (int)(number * 10 % 10 > 0 ? number + 1 : number);
    }
}
