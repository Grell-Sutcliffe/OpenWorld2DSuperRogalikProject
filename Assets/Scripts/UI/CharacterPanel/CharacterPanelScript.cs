 using UnityEngine;

public class CharacterPanelScript : MonoBehaviour
{
    public GameObject characterPanel;
    public GameObject weaponPanel;

    void Start()
    {
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
        weaponPanel.SetActive(true);
    }
}
