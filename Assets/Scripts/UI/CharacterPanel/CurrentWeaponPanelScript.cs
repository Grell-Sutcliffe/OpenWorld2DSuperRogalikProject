using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CurrentWeaponPanelScript : MonoBehaviour
{
    MainController mainController;

    //public GameObject switchWeaponPanel;
    public CharacterPanelScript characterPanelScript;

    public SwitchWeaponPanelScript switchWeaponPanelScript;
    public GameObject upgrateWeaponPanel;

    public Image weaponImage_1;
    public Image weaponImage_2;

    public TextMeshProUGUI weaponAmount;

    public Weapon weapon;

    [Header("Спрайт")]
    public Image weaponImage; 

    [Header("Характеристики")]
    public TextMeshProUGUI rarity_TMP;
    public TextMeshProUGUI element_TMP;
    public Image element_Image;
    public TextMeshProUGUI physical_attack_TMP;
    public TextMeshProUGUI elemental_attack_TMP;
    public TextMeshProUGUI crit_chance_TMP;
    public TextMeshProUGUI crit_dmg_TMP;
    public TextMeshProUGUI elemental_mastery_TMP;

    [Header("Улучшить характеристики OLD")]
    public TextMeshProUGUI upgrate_old_level_TMP;
    public TextMeshProUGUI upgrate_old_physical_attack_TMP;
    public TextMeshProUGUI upgrate_old_elemental_attack_TMP;
    public TextMeshProUGUI upgrate_old_crit_chance_TMP;
    public TextMeshProUGUI upgrate_old_crit_dmg_TMP;
    public TextMeshProUGUI upgrate_old_elemental_mastery_TMP;

    [Header("Улучшить характеристики NEW")]
    public TextMeshProUGUI upgrate_new_level_TMP;
    public TextMeshProUGUI upgrate_new_physical_attack_TMP;
    public TextMeshProUGUI upgrate_new_elemental_attack_TMP;
    public TextMeshProUGUI upgrate_new_crit_chance_TMP;
    public TextMeshProUGUI upgrate_new_crit_dmg_TMP;
    public TextMeshProUGUI upgrate_new_elemental_mastery_TMP;

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

        upgrateWeaponPanel.SetActive(false);
    }

    void Start()
    {
        // characterPanelScript = GameObject.Find("CharacterPanel").GetComponent<CharacterPanelScript>();
    }

    public void OpenUpgrateWeaponPanel()
    {
        // UpdateGoldAmount();
        
        upgrateWeaponPanel.SetActive(true);

        weaponImage_1.sprite = weapon.sprite;
        weaponImage_2.sprite = weapon.sprite;
        weaponAmount.text = (weapon.amount - 1).ToString();

        upgrate_old_level_TMP.text = weapon.current_level.ToString();
        upgrate_new_level_TMP.text = (weapon.current_level + 1).ToString();

        upgrate_old_physical_attack_TMP.text = weapon.stats.physical_attack .ToString();
        upgrate_new_physical_attack_TMP.text = (characterPanelScript.RoundToMax(weapon.stats.physical_attack * weapon.upgrade_percent)).ToString();

        upgrate_old_elemental_attack_TMP.text = weapon.stats.elemental_attack .ToString();
        upgrate_new_elemental_attack_TMP.text = (characterPanelScript.RoundToMax(weapon.stats.elemental_attack * weapon.upgrade_percent)).ToString();

        upgrate_old_crit_chance_TMP.text = FloatToString(weapon.stats.crit_chance);
        upgrate_new_crit_chance_TMP.text = FloatToString(weapon.stats.crit_chance * weapon.upgrade_percent);

        upgrate_old_crit_dmg_TMP.text = FloatToString(weapon.stats.crit_dmg);
        upgrate_new_crit_dmg_TMP.text = FloatToString(weapon.stats.crit_dmg * weapon.upgrade_percent);

        upgrate_old_elemental_mastery_TMP.text = FloatToString(weapon.stats.elemental_mastery);
        upgrate_new_elemental_mastery_TMP.text = FloatToString(weapon.stats.elemental_mastery * weapon.upgrade_percent);
    }

    public void UpgrateWeapon()
    {
        if (mainController.WeaponUpgrade(weapon.id))
        {
            weapon.amount--;
            OpenUpgrateWeaponPanel();
            UpdatePanel();
        }
        else
        {
            mainController.OpenErrorPanel(ErrorType.NotEnoughMaterials);
        }
    }

    void UpdatePanel()
    {
        // if (characterPanelScript == null) characterPanelScript = GameObject.Find("CharacterPanel").GetComponent<CharacterPanelScript>();

        if (weapon == null) return;

        weaponImage.sprite = weapon.sprite;
        rarity_TMP.text = weapon.stars.ToString();
        element_TMP.text = characterPanelScript.dict_element_type_to_element[weapon.element_type].element_name;
        element_Image.sprite = characterPanelScript.dict_element_type_to_element[weapon.element_type].sprite;
        physical_attack_TMP.text = "+" + weapon.stats.physical_attack.ToString();
        elemental_attack_TMP.text = "+" + weapon.stats.elemental_attack.ToString();
        crit_chance_TMP.text = "+" + FloatToString(GetPercent(weapon.stats.crit_chance));
        crit_dmg_TMP.text = "+" + FloatToString(GetPercent(weapon.stats.crit_dmg));
        elemental_mastery_TMP.text = "+" + FloatToString(GetPercent(weapon.stats.elemental_mastery));
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

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        UpdatePanel();
    }

    public void OpenSwitchWeaponPanel()
    {
        switchWeaponPanelScript.OpenPanel();
    }

    public void SetNewWeaponInCharacterPanel(Weapon new_weapon)
    {
        characterPanelScript.SetNewWeapon(characterPanelScript.current_weapon_index, new_weapon);
    }

    public void SetNewWeapon(Weapon new_weapon)
    {
        weapon = new_weapon;

        UpdatePanel();

        //characterPanelScript.SetNewWeapon(weapon);
    }
}
