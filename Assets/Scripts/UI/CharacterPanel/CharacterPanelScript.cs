using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelScript : MonoBehaviour
{
    MainController mainController;

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

    public Image weapon1Image;
    public Image weapon2Image;

    public Image costImage;
    public TextMeshProUGUI costTMP;
    
    Player playerScript;
    CurrentWeaponPanelScript currentWeaponPanelScript;
    public BackPackController backpackController;
    public ShopPanelScript shopPanelScript;

    public Image characterImage;

    [Header("’арактеристики")]
    public TextMeshProUGUI health_TMP;
    public TextMeshProUGUI physical_attack_TMP;
    public TextMeshProUGUI elemental_attack_TMP;
    public TextMeshProUGUI crit_chance_TMP;
    public TextMeshProUGUI crit_dmg_TMP;
    public TextMeshProUGUI elemental_mastery_TMP;

    [Header("”лучшить характеристики OLD")]
    public TextMeshProUGUI upgrate_old_level_TMP;
    public TextMeshProUGUI upgrate_old_health_TMP;
    public TextMeshProUGUI upgrate_old_physical_attack_TMP;
    public TextMeshProUGUI upgrate_old_elemental_attack_TMP;
    public TextMeshProUGUI upgrate_old_crit_chance_TMP;
    public TextMeshProUGUI upgrate_old_crit_dmg_TMP;
    public TextMeshProUGUI upgrate_old_elemental_mastery_TMP;

    [Header("”лучшить характеристики NEW")]
    public TextMeshProUGUI upgrate_new_level_TMP;
    public TextMeshProUGUI upgrate_new_health_TMP;
    public TextMeshProUGUI upgrate_new_physical_attack_TMP;
    public TextMeshProUGUI upgrate_new_elemental_attack_TMP;
    public TextMeshProUGUI upgrate_new_crit_chance_TMP;
    public TextMeshProUGUI upgrate_new_crit_dmg_TMP;
    public TextMeshProUGUI upgrate_new_elemental_mastery_TMP;

    [Header("BOOST ’арактеристики")]
    public TextMeshProUGUI boost_health_TMP;
    public TextMeshProUGUI boost_physical_attack_TMP;
    public TextMeshProUGUI boost_elemental_attack_TMP;
    public TextMeshProUGUI boost_crit_chance_TMP;
    public TextMeshProUGUI boost_crit_dmg_TMP;
    public TextMeshProUGUI boost_elemental_mastery_TMP;

    [Header("Ёлементы")]
    public Dictionary<ElementType, Element> dict_element_type_to_element;

    public Sprite sprite_cryo;
    public Sprite sprite_pyro;
    public Sprite sprite_electro;
    public Sprite sprite_anemo;
    public Sprite sprite_physical;

    public string name_cryo = "cryo";
    public string name_pyro = "pyro";
    public string name_electro = "energo";
    public string name_anemo = "floro";
    public string name_physical = "physical";

    public int current_weapon_index = 0;
    public WeaponType current_weaponType;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
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
        SwitchWeaponTo(0);
    }

    public void UpdatePanel()
    {
        if (currentWeaponPanelScript == null) currentWeaponPanelScript = weaponPanel.GetComponent<CurrentWeaponPanelScript>();
        if (playerScript == null) playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Debug.LogError($"playerScript.weapon = {playerScript.weapon}");

        Debug.Log($"weapon 1 = {playerScript.weapons[0].item_name}, weapon 2 = {playerScript.weapons[1].item_name}");
        weapon1Image.sprite = playerScript.weapons[0].sprite;
        weapon2Image.sprite = playerScript.weapons[1].sprite;

        //Debug.LogError($"playerScript.weapon = {playerScript.weapon} name = {playerScript.weapon.item_name}");
        /*
        health_TMP.text = RoundToMax(playerScript.player_full_stats.health).ToString();
        physical_attack_TMP.text = RoundToMax(playerScript.player_full_stats.physical_attack + playerScript.weapons[current_weapon_index].stats.physical_attack).ToString();
        elemental_attack_TMP.text = RoundToMax(playerScript.player_full_stats.elemental_attack + playerScript.weapons[current_weapon_index].stats.elemental_attack).ToString();
        crit_chance_TMP.text = FloatToString(GetPercent(playerScript.player_full_stats.crit_chance + playerScript.weapons[current_weapon_index].stats.crit_chance));
        crit_dmg_TMP.text = FloatToString(GetPercent(playerScript.player_full_stats.crit_dmg + playerScript.weapons[current_weapon_index].stats.crit_dmg));
        elemental_mastery_TMP.text = FloatToString(GetPercent(playerScript.weapons[current_weapon_index].stats.elemental_mastery));
        */
        /*
        health_TMP.text = RoundToMax(playerScript.current_stats.health).ToString();
        physical_attack_TMP.text = RoundToMax(playerScript.current_stats.physical_attack).ToString();
        elemental_attack_TMP.text = RoundToMax(playerScript.current_stats.elemental_attack).ToString();
        crit_chance_TMP.text = FloatToString(GetPercent(playerScript.current_stats.crit_chance));
        crit_dmg_TMP.text = FloatToString(GetPercent(playerScript.current_stats.crit_dmg));
        elemental_mastery_TMP.text = FloatToString(GetPercent(playerScript.current_stats.elemental_mastery));
        */

        health_TMP.text = RoundToMax(playerScript.current_stats.health).ToString();
        boost_health_TMP.text = "";
        if (playerScript.boost_stats.health > 0)
        {
            health_TMP.text += "+" + RoundToMax(playerScript.boost_stats.health).ToString();
            boost_health_TMP.text += "+" + RoundToMax(playerScript.boost_stats.health).ToString();
        }

        physical_attack_TMP.text = RoundToMax(playerScript.current_stats.physical_attack).ToString();
        boost_physical_attack_TMP.text = "";
        if (playerScript.boost_stats.physical_attack > 0)
        {
            physical_attack_TMP.text += "+" + RoundToMax(playerScript.boost_stats.physical_attack).ToString();
            boost_physical_attack_TMP.text += "+" + RoundToMax(playerScript.boost_stats.physical_attack).ToString();
        }

        elemental_attack_TMP.text = RoundToMax(playerScript.current_stats.elemental_attack).ToString();
        boost_elemental_attack_TMP.text = "";
        if (playerScript.boost_stats.elemental_attack > 0)
        {
            elemental_attack_TMP.text += "+" + RoundToMax(playerScript.boost_stats.elemental_attack).ToString();
            boost_elemental_attack_TMP.text += "+" + RoundToMax(playerScript.boost_stats.elemental_attack).ToString();
        }

        crit_chance_TMP.text = FloatToString(GetPercent(playerScript.current_stats.crit_chance));
        boost_crit_chance_TMP.text = "";
        if (playerScript.boost_stats.crit_chance > 0)
        {
            crit_chance_TMP.text += "+" + FloatToString(GetPercent(playerScript.boost_stats.crit_chance));
            boost_crit_chance_TMP.text += "+" + FloatToString(GetPercent(playerScript.boost_stats.crit_chance));
        }

        crit_dmg_TMP.text = FloatToString(GetPercent(playerScript.current_stats.crit_dmg));
        boost_crit_dmg_TMP.text = "";
        if (playerScript.boost_stats.crit_dmg > 0)
        {
            crit_dmg_TMP.text += "+" + FloatToString(GetPercent(playerScript.boost_stats.crit_dmg));
            boost_crit_dmg_TMP.text += "+" + FloatToString(GetPercent(playerScript.boost_stats.crit_dmg));
        }

        elemental_mastery_TMP.text = FloatToString(GetPercent(playerScript.current_stats.elemental_mastery));
        boost_elemental_mastery_TMP.text = "";
        if (playerScript.boost_stats.elemental_mastery > 0)
        {
            elemental_mastery_TMP.text += "+" + FloatToString(GetPercent(playerScript.boost_stats.elemental_mastery));
            boost_elemental_mastery_TMP.text += "+" + FloatToString(GetPercent(playerScript.boost_stats.elemental_mastery));
        }
    }

    public void OpenCharacterUpgradePanel()
    {
        UpdateGoldAmount();

        characterUpgratePanel.SetActive(true);

        costImage.sprite = shopPanelScript.dict_costType_to_Item[playerScript.upgrate_cost.cost_type].sprite;
        costTMP.text = playerScript.upgrate_cost.cost_amount.ToString();

        upgrate_old_level_TMP.text = playerScript.current_level.ToString();
        upgrate_new_level_TMP.text = (playerScript.current_level + 1).ToString();

        upgrate_old_health_TMP.text = playerScript.player_full_stats.health.ToString();
        upgrate_new_health_TMP.text = (RoundToMax(playerScript.player_full_stats.health * playerScript.upgrade_percent)).ToString();

        upgrate_old_physical_attack_TMP.text = (playerScript.player_full_stats.physical_attack + playerScript.weapons[current_weapon_index].stats.physical_attack).ToString();
        upgrate_new_physical_attack_TMP.text = (RoundToMax(playerScript.player_full_stats.physical_attack * playerScript.upgrade_percent + playerScript.weapons[current_weapon_index].stats.physical_attack)).ToString();

        upgrate_old_elemental_attack_TMP.text = (playerScript.player_full_stats.elemental_attack + playerScript.weapons[current_weapon_index].stats.elemental_attack).ToString();
        upgrate_new_elemental_attack_TMP.text = (RoundToMax(playerScript.player_full_stats.elemental_attack * playerScript.upgrade_percent + playerScript.weapons[current_weapon_index].stats.elemental_attack)).ToString();

        upgrate_old_crit_chance_TMP.text = FloatToString(GetPercent(playerScript.player_full_stats.crit_chance + playerScript.weapons[current_weapon_index].stats.crit_chance));
        upgrate_new_crit_chance_TMP.text = FloatToString(GetPercent(playerScript.player_full_stats.crit_chance * playerScript.upgrade_percent + playerScript.weapons[current_weapon_index].stats.crit_chance));

        upgrate_old_crit_dmg_TMP.text = FloatToString(GetPercent(playerScript.player_full_stats.crit_dmg + playerScript.weapons[current_weapon_index].stats.crit_dmg));
        upgrate_new_crit_dmg_TMP.text = FloatToString(GetPercent(playerScript.player_full_stats.crit_dmg * playerScript.upgrade_percent + playerScript.weapons[current_weapon_index].stats.crit_dmg));

        upgrate_old_elemental_mastery_TMP.text = FloatToString(GetPercent(playerScript.weapons[current_weapon_index].stats.elemental_mastery));
        upgrate_new_elemental_mastery_TMP.text = FloatToString(GetPercent(playerScript.weapons[current_weapon_index].stats.elemental_mastery));
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
        else
        {
            mainController.OpenErrorPanel(ErrorType.NotEnoughMaterials);
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
        dict_element_type_to_element[ElementType.Energo] = new Element(ElementType.Energo, name_electro, sprite_electro);
        dict_element_type_to_element[ElementType.Floro] = new Element(ElementType.Floro, name_anemo, sprite_anemo);
        dict_element_type_to_element[ElementType.Physical] = new Element(ElementType.Physical, name_physical, sprite_physical);
    }

    public void SetNewWeapon(int new_index, Weapon new_weapon)
    {
        current_weapon_index = new_index;
        currentWeaponPanelScript.SetNewWeapon(new_weapon);
        //GivePlayerNewWeapon();
        playerScript.SetNewWeaponOnIndex(current_weapon_index, new_weapon);
        UpdatePanel();
        mainController.UpdateButtlePanel();
    }

    void UpdateGoldAmount()
    {
        goldImage.sprite = shopPanelScript.dict_costType_to_Item[CostType.Gold].sprite;
        goldTMP.text = shopPanelScript.dict_costType_to_Item[CostType.Gold].amount.ToString();
    }

    public void SwitchWeaponTo1()
    {
        SwitchWeaponTo(0);
    }

    public void SwitchWeaponTo2()
    {
        SwitchWeaponTo(1);
    }

    void SwitchWeaponTo(int index)
    {
        current_weapon_index = index;
        SetNewWeapon(index, playerScript.weapons[index]);
        current_weaponType = currentWeaponPanelScript.weapon.weapon_type;
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

    public string FloatToString(float number)
    {
        if (float.IsNaN(number) || float.IsInfinity(number))
            return "0.0";

        int temp_int = (int)(number * 10);
        number = temp_int / 10f;

        return number.ToString();
    }

    public float GetPercent(float number)
    {
        if (float.IsNaN(number) || float.IsInfinity(number)) return 0f;
        return number * 100;
    }
}
