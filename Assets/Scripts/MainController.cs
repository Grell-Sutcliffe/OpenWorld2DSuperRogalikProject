using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DialogController;
using static DialogPanelScript;

public class MainController : MonoBehaviour
{
    QuestsController questsController;
    Player playerScript;

    public HealthBarScript healthBarScript;

    public GameObject playerPanel;
    public GameObject dialogPanel;
    public GameObject questPanel;
    public GameObject wishPanel;
    public GameObject shopPanel;
    public GameObject characterPanel;
    public GameObject switchWeaponPanel;
    public GameObject backpackPanel;
    public GameObject buttlePanel;
    public GameObject enterDangeonPanel;
    public GameObject multiplayerPanel;
    public GameObject rewardPanel;

    //public GameObject taskShower;

    public GameObject Dedus;
    public GameObject GrandsonEugene;
    public GameObject Dangeon;
    public GameObject Doggy;
    public GameObject Book;

    public ScrollInteractionScript scrollInteractionScript;
    BackPackController backpackController;
    WishPanelScript wishPanelScript;
    ShopPanelScript shopPanelScript;
    CharacterPanelScript characterPanelScript;
    RewardPanelScript rewardPanelScript;
    ButtlePanelScript buttlePanelScript;

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;

    DedusController dedusController;
    GrandsonEugeneController grandsonEugeneController;

    DangeonInteractionScript dangeonInteractionScript;

    public bool is_keyboard_active = true;

    SpriteRenderer current_interaction_SR;

    public List<SpriteRenderer> list_of_interactable_SR = new List<SpriteRenderer>();
    public List<string> list_of_interactable_objects_names = new List<string>();

    bool dedus_F;
    bool grandsonEugene_F;
    bool dangeon_F;
    bool doggy_F;
    bool book_F;

    private void Awake()
    {
        StuffSetActiveTrue();

        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();
        backpackController = backpackPanel.GetComponent<BackPackController>();
        wishPanelScript = wishPanel.GetComponent<WishPanelScript>();
        shopPanelScript = shopPanel.GetComponent<ShopPanelScript>();
        characterPanelScript = characterPanel.GetComponent<CharacterPanelScript>();
        buttlePanelScript = buttlePanel.GetComponent<ButtlePanelScript>();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();
        rewardPanelScript = rewardPanel.GetComponent<RewardPanelScript>();
    }

    void Start()
    {
        //backpackController.MakeDictionary();

        SetDangeonScripts();

        is_keyboard_active = true;

        StuffSetActiveFalse();
    }

    public bool UseWish(bool is_pink, int number)
    {
        string wish_name;
        if (is_pink)
        {
            wish_name = shopPanelScript.dict_costType_to_Item[CostType.PinkWish].item_name;
        }
        else
        {
            wish_name = shopPanelScript.dict_costType_to_Item[CostType.BlueWish].item_name;
        }
        return backpackController.DecreaceItemByName(wish_name, number);
    }

    public void GetReward()
    {
        System.Random rand = new System.Random();
        int new_reward_amount = rand.Next(1, 10);

        rewardPanelScript.SetRewardAmount(new_reward_amount);
    }

    public void ClaimReward(int amount)
    {
        string reward_name = shopPanelScript.dict_costType_to_Item[CostType.BlueWish].item_name;

        backpackController.IncreaceItemByName(reward_name, amount);
    }

    public void SetCharacterWeapon(int index, Weapon weapon)
    {
        backpackController.IncreaceItemByName(weapon.item_name, 1);
        characterPanelScript.SetNewWeapon(index, weapon);
    }

    public void UpdateButtlePanel()
    {
        buttlePanelScript.SetNewWeapons(playerScript.weapons[0], playerScript.weapons[1]);
    }

    public bool WeaponUpgrade(int weapon_id)
    {
        if (backpackController.dict_id_to_item[weapon_id].amount > 1)
        {
            if (backpackController.dict_id_to_item[weapon_id] is Weapon weapon)
            {
                if (weapon.current_level < weapon.max_level)
                {
                    weapon.WeaponUpgrade();
                    return true;
                }
            }
        }
        return false;
    }

    public void PickUpItemByName(string name)
    {
        backpackController.IncreaceItemByName(name, 1);
    }

    public void UpdateWishPanelInfo()
    {
        wishPanelScript.UpdatePinkWishInfo(backpackController.GetItemCounterByName(shopPanelScript.dict_costType_to_Item[CostType.PinkWish].item_name));
        wishPanelScript.UpdateBlueWishInfo(backpackController.GetItemCounterByName(shopPanelScript.dict_costType_to_Item[CostType.BlueWish].item_name));
    }

    public int GetItemCounterByName(string name)
    {
        return backpackController.GetItemCounterByName(name);
    }

    public Item GetItemByName(string name)
    {
        //Debug.LogError($"backpackController == null ? {backpackController == null}");
        return backpackController.GetItemByName(name);
    }

    public void ShowInteraction()
    {
        //Debug.Log("SHOW INTERACTION");

        foreach (string c in list_of_interactable_objects_names)
        {
            //Debug.Log($"{c}");
        }

        current_interaction_SR = null;

        if (scrollInteractionScript == null) scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();

        if (scrollInteractionScript.current_index >= list_of_interactable_objects_names.Count)
        {
            scrollInteractionScript.current_index = 0;
        }
        scrollInteractionScript.ApplyAllColors();
    }

    public void UpdateHealthBar(int amount)
    {
        healthBarScript.UpdateHealthBar(amount);
    }

    public void UpdateHealthBar(float amount)
    {
        healthBarScript.UpdateHealthBar(amount);
    }

    public void InteractDangeon()
    {
        ShowEnterDangeonPanel();
    }

    public void StartDialog(Dialog dialog)
    {
        if (dialog == null) return;
        dialogPanelScript.StartDialog(dialog);
    }

    void StuffSetActiveTrue()
    {
        characterPanel.SetActive(true);
        dialogPanel.SetActive(true);
        questPanel.SetActive(true);
        backpackPanel.SetActive(true);
        shopPanel.SetActive(true);
        wishPanel.SetActive(true);
        switchWeaponPanel.SetActive(true);
        enterDangeonPanel.SetActive(true);
        //multiplayerPanel.SetActive(true);
    }

    void StuffSetActiveFalse()
    {
        dialogPanel.SetActive(false);
        questPanel.SetActive(false);
        wishPanel.SetActive(false);
        shopPanel.SetActive(false);
        backpackPanel.SetActive(false);
        characterPanel.SetActive(false);
        switchWeaponPanel.SetActive(false);
        enterDangeonPanel.SetActive(false);
        multiplayerPanel.SetActive(false);

        rewardPanel.SetActive(false);

        dedus_F = false;
        grandsonEugene_F = false;
        dangeon_F = false;
    }

    public void EnterDangeon()
    {
        Debug.Log("ENTER DANGEON");
        dangeonInteractionScript.EnterDangeon();
    }

    public void ShowPlayerPanel()
    {
        playerPanel.SetActive(true);
    }

    public void HidePlayerPanel()
    {
        playerPanel.SetActive(false);
    }

    public void ShowEnterDangeonPanel()
    {
        enterDangeonPanel.SetActive(true);
        TurnOffKeyboard();
    }

    public void HideEnterDangeonPanel()
    {
        enterDangeonPanel.SetActive(false);
        TurnOnKeyboard();
    }

    void SetDangeonScripts()
    {
        if (Dangeon == null) Dangeon = GameObject.Find("Dangeon");
        if (Dangeon != null)
        {
            dangeonInteractionScript = Dangeon.GetComponent<DangeonInteractionScript>();
        }
    }

    public void OpenQuestPanel()
    {
        questPanel.SetActive(true);
        questsController.OpenQuestPanel();
        TurnOffKeyboard();
    }

    public void CloseQuestPanel()
    {
        questPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void OpenWishPanel()
    {
        wishPanelScript.OpenWishPanel();
        TurnOffKeyboard();
    }

    public void CloseWishPanel()
    {
        wishPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void OpenCharacterPanel()
    {
        characterPanelScript.OpenCharacterPanel();
        TurnOffKeyboard();
    }

    public void CloseCharacterPanel()
    {
        characterPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void OpenBackpackPanel()
    {
        backpackController.OpenBackpackPanel();
        TurnOffKeyboard();
    }

    public void CloseBackpackPanel()
    {
        backpackPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void OpenShopPanel()
    {
        shopPanelScript.OpenShopPanel();
        TurnOffKeyboard();
    }

    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void TurnOnKeyboard()
    {
        is_keyboard_active = true;
    }

    public void TurnOffKeyboard()
    {
        is_keyboard_active = false;
    }
}
