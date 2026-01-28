using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponPanelScript : MonoBehaviour
{
    //public GameObject switchWeaponPanel;
    CharacterPanelScript characterPanelScript;

    public SwitchWeaponPanelScript switchWeaponPanelScript;

    public Weapon weapon;

    [Header("Спрайт")]
    public Image weaponImage; 

    [Header("Характеристики")]
    public TextMeshProUGUI rarity_TMP;
    public TextMeshProUGUI element_TMP;
    public Image element_Image;
    public TextMeshProUGUI attack_TMP;
    public TextMeshProUGUI crit_chance_TMP;
    public TextMeshProUGUI crit_dmg_TMP;
    public TextMeshProUGUI elemental_mastery_TMP;

    void Start()
    {
        characterPanelScript = GameObject.Find("CharacterPanel").GetComponent<CharacterPanelScript>();
    }

    void UpdatePanel()
    {
        if (characterPanelScript == null) characterPanelScript = GameObject.Find("CharacterPanel").GetComponent<CharacterPanelScript>();

        if (weapon == null) return;

        weaponImage.sprite = weapon.sprite;
        rarity_TMP.text = weapon.stars.ToString();
        element_TMP.text = characterPanelScript.dict_element_type_to_element[weapon.element].name;
        element_Image.sprite = characterPanelScript.dict_element_type_to_element[weapon.element].sprite;
        attack_TMP.text = "+" + weapon.damage.ToString();
        crit_chance_TMP.text = "+" + GetPercent(weapon.crit_chance).ToString();
        crit_dmg_TMP.text = "+" + GetPercent(weapon.crit_dmg).ToString();
        elemental_mastery_TMP.text = "+" + GetPercent(weapon.elemental_mastery).ToString();
    }

    float GetPercent(float number)
    {
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

    public void SetNewWeapon(Weapon new_weapon)
    {
        weapon = new_weapon;

        UpdatePanel();
    }
}
