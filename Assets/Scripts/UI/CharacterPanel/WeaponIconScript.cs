using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconScript : MonoBehaviour
{
    SwitchWeaponPanelScript switchWeaponPanelScript;
    CharacterPanelScript characterPanelScript;

    Weapon weapon;

    public Image weaponImage;
    public Image elementImage;
    public TextMeshProUGUI itemNameTMP;
    public TextMeshProUGUI itemStarTMP;

    void Start()
    {
        switchWeaponPanelScript = GameObject.Find("SwitchWeaponPanel").GetComponent<SwitchWeaponPanelScript>();
        characterPanelScript = GameObject.Find("CharacterPanel").GetComponent<CharacterPanelScript>();
    }

    public void OnClick()
    {
        switchWeaponPanelScript.SelectNewWeapon(weapon);
    }

    public void CreateWeaponItem(Weapon weapon)
    {
        if (switchWeaponPanelScript == null) switchWeaponPanelScript = GameObject.Find("SwitchWeaponPanel").GetComponent<SwitchWeaponPanelScript>();
        if (characterPanelScript == null) characterPanelScript = GameObject.Find("CharacterPanel").GetComponent<CharacterPanelScript>();

        this.weapon = weapon;

        weaponImage.sprite = weapon.sprite;
        elementImage.sprite = characterPanelScript.dict_element_type_to_element[weapon.elementalDamage.element_type].sprite;
        itemNameTMP.text = weapon.item_name;
        itemStarTMP.text = weapon.stars.ToString();
    }
}
