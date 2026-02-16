using UnityEngine;
using UnityEngine.UI;

public class ButtlePanelScript : MonoBehaviour
{
    public Image weaponImage1;
    public Image weaponImage2;

    Weapon weapon1;
    Weapon weapon2;

    void Start()
    {
        
    }

    public void SetNewWeapons(Weapon weapon1, Weapon weapon2)
    {
        this.weapon1 = weapon1;
        this.weapon2 = weapon2;

        weaponImage1.sprite = weapon1.sprite;
        weaponImage2.sprite = weapon2.sprite;
    }
}
