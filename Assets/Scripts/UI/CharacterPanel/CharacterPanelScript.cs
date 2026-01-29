using System.Collections.Generic;
using UnityEngine;

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
    
    public GameObject characterPanel;
    public GameObject weaponPanel;
    
    CurrentWeaponPanelScript currentWeaponPanelScript;

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
        MakeDictionary();
    }

    void Start()
    {
        currentWeaponPanelScript = weaponPanel.GetComponent<CurrentWeaponPanelScript>();

        GoToCharacterPanel();
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

    public void OpenCharacterPanel()
    {
        gameObject.SetActive(true);
        GoToCharacterPanel();
    }

    public void GoToCharacterPanel()
    {
        characterPanel.SetActive(true);
        weaponPanel.SetActive(false);
    }

    public void GoToWeaponPanel()
    {
        characterPanel.SetActive(false);
        //weaponPanel.SetActive(true);
        currentWeaponPanelScript.OpenPanel();
    }
}
