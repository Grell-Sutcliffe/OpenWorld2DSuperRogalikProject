using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponPanelScript : MonoBehaviour
{
    CurrentWeaponPanelScript currentWeaponPanelScript;
    BackPackController backpackController;

    Weapon current_selected_weapon;

    public GameObject content_GO;
    public GameObject weaponIconPrefab;

    RectTransform content_rect_transform;

    public int item_counter = 0;

    public int item_height = 896;
    public int space_between_items = 50;
    public int item_in_row = 3;

    List<Weapon> weapons;

    private void Awake()
    {
        //mainController = GameObject.Find("MainController").GetComponent<MainController>();
        //Debug.LogError($"GameObject.Find(\"BackpackPanel\") == null: {GameObject.Find("BackpackPanel") == null}");

        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    void Start()
    {
        currentWeaponPanelScript = GameObject.Find("CurrentWeaponPanel").GetComponent<CurrentWeaponPanelScript>();

        content_rect_transform = content_GO.GetComponent<RectTransform>();

        UpdateContent();
    }

    public void OpenPanel()
    {
        if (currentWeaponPanelScript == null) currentWeaponPanelScript = GameObject.Find("CurrentWeaponPanel").GetComponent<CurrentWeaponPanelScript>();

        gameObject.SetActive(true);
        current_selected_weapon = currentWeaponPanelScript.weapon;
        UpdateContent();
    }

    public void SelectNewWeapon(Weapon new_weapon)
    {
        current_selected_weapon = new_weapon;
    }

    public void Select()
    {
        currentWeaponPanelScript.SetNewWeapon(current_selected_weapon);
    }

    void UpdateContent()
    {
        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        ClearContent();
        SpawnNewItems();

        Debug.Log(item_counter);
    }

    void SpawnNewItems()
    {
        item_counter = 0;

        foreach (int id in backpackController.dict_id_to_item.Keys)
        {
            if (backpackController.dict_id_to_item[id].amount > 0 && backpackController.dict_id_to_item[id].item_type == ItemType.Weapon)
            {
                if (backpackController.dict_id_to_item[id] is Weapon weapon)
                {
                    if (weapon.weapon_type == WeaponType.Sword)
                    {
                        item_counter++;
                        SpawnIconPrefab(weapon);
                    }
                }
            }
        }

        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount + 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);
    }

    void ClearContent()
    {
        foreach (Transform child in content_GO.transform)
        {
            Debug.Log($"delete item");
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void SpawnIconPrefab(Weapon weapon)
    {
        GameObject new_prefab = Instantiate(weaponIconPrefab, content_GO.transform);
        WeaponIconScript new_prefab_script = new_prefab.GetComponent<WeaponIconScript>();

        new_prefab_script.CreateWeaponItem(weapon);
    }
}
