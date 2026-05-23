using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingIconScript : MonoBehaviour
{
    CraftPanelScript craftPanelScript;

    public Image image;
    public TextMeshProUGUI textTMP;

    private string item_name;

    public void SetSlot(Item item)
    {
        craftPanelScript = GameObject.Find("CraftPanel").GetComponent<CraftPanelScript>();

        item_name = item.item_name;

        image.sprite = item.sprite;
        textTMP.text = item_name;
    }

    public void OnClick()
    {
        craftPanelScript.ChangeCraftingLeftPanel(item_name);
    }
}
