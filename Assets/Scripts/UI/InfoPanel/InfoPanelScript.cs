using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelScript : MonoBehaviour
{
    BackPackController backpackController;

    public GameObject info_GO;
    public GameObject weapon_info_GO;
    public GameObject content_GO;
    public GameObject infoPrefab;

    public Image image;
    public Sprite empty_sprite;
    public TextMeshProUGUI title_TMP;
    public TextMeshProUGUI description_TMP;
    public TextMeshProUGUI star_TMP;
    public Image elementImage;

    RectTransform content_rect_transform;

    public List<InfoSO> list_of_infoSOs = new List<InfoSO>();

    public Dictionary<InfoType, List<InfoSO>> dict_infoType_to_list_of_infoSO;

    public int item_height = 810;
    public int space_between_items = 50;
    public int item_in_row = 2;

    //private bool was_loaded = false;

    void Awake()
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        MakeDictionary();

        content_rect_transform = content_GO.GetComponent<RectTransform>();
    }

    void MakeDictionary()
    {
        dict_infoType_to_list_of_infoSO = new Dictionary<InfoType, List<InfoSO>>();

        dict_infoType_to_list_of_infoSO[InfoType.Enemy] = new List<InfoSO>();
        dict_infoType_to_list_of_infoSO[InfoType.Weapon] = new List<InfoSO>();
        dict_infoType_to_list_of_infoSO[InfoType.Note] = new List<InfoSO>();

        foreach (InfoSO infoSO in list_of_infoSOs)
        {
            dict_infoType_to_list_of_infoSO[infoSO.infoType].Add(infoSO);
        }

        //was_loaded = true;
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);

        /*
        if (!was_loaded)
        {
            MakeDictionary();
        }*/

        SpawnContent(InfoType.Enemy);

        SelectInfo();
    }

    public void SelectInfo(InfoSO infoSO = null)
    {
        if (infoSO == null)
        {
            info_GO.SetActive(false);

            /*
            image.sprite = empty_sprite;
            title_TMP.text = "";
            description_TMP.text = "";
            */

            return;
        }

        info_GO.SetActive(true);

        if (infoSO is WeaponInfoSO weaponInfoSO)
        {
            image.sprite = weaponInfoSO.weaponSO.sprite;
            title_TMP.text = weaponInfoSO.weaponSO.weapon_name;
            description_TMP.text = weaponInfoSO.weaponSO.description;

            weapon_info_GO.SetActive(true);

            star_TMP.text = weaponInfoSO.weaponSO.stars.ToString();
            elementImage.sprite = backpackController.GetElementByElementType(weaponInfoSO.weaponSO.element_type).sprite;
        }
        else
        {
            weapon_info_GO.SetActive(false);

            image.sprite = infoSO.sprite;
            title_TMP.text = infoSO.title;
            description_TMP.text = infoSO.description;
        }
    }

    void SpawnContent(InfoType infoType)
    {
        ClearContent();
        ChangePanelHeight(infoType);
    }

    void ClearContent()
    {
        foreach (Transform child in content_GO.transform)
        {
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void ChangePanelHeight(InfoType infoType)
    {
        int item_counter = dict_infoType_to_list_of_infoSO[infoType].Count;

        int row_amount = item_counter / item_in_row + (item_counter % item_in_row == 0 ? 0 : 1);
        int new_height = row_amount * item_height + (row_amount + 1) * space_between_items;

        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (InfoSO infoSO in dict_infoType_to_list_of_infoSO[infoType])
        {
            SpawnPrefab(infoSO);
        }
    }

    void SpawnPrefab(InfoSO infoSO)
    {
        GameObject new_prefab = Instantiate(infoPrefab, content_GO.transform);
        InfoSelectableIconScript new_prefab_script = new_prefab.GetComponent<InfoSelectableIconScript>();

        new_prefab_script.UpdateInfo(infoSO);
    }

    public void SpawnEnemyContent()
    {
        SpawnContent(InfoType.Enemy);
    }

    public void SpawnWeaponContent()
    {
        SpawnContent(InfoType.Weapon);
    }

    public void SpawnNoteContent()
    {
        SpawnContent(InfoType.Note);
    }
}

public enum InfoType
{
    Enemy = 0,
    Weapon = 1,
    Note = 2,
}
