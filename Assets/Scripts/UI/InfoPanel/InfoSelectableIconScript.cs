using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSelectableIconScript : MonoBehaviour
{
    InfoPanelScript infoPanelScript;

    InfoSO infoSO;

    public Image image;
    public TextMeshProUGUI title_TMP;

    public void UpdateInfo(InfoSO infoSO)
    {
        if (infoSO is WeaponInfoSO weaponInfoSO)
        {
            this.infoSO = weaponInfoSO;

            image.sprite = weaponInfoSO.weaponSO.sprite;
            title_TMP.text = weaponInfoSO.weaponSO.weapon_name;
        }
        else
        {
            this.infoSO = infoSO;

            image.sprite = infoSO.sprite;
            title_TMP.text = infoSO.title;
        }
    }

    public void OnClick()
    {
        if (infoPanelScript == null) infoPanelScript = GameObject.Find("InfoPanel").GetComponent<InfoPanelScript>();

        infoPanelScript.SelectInfo(infoSO);
    }
}
