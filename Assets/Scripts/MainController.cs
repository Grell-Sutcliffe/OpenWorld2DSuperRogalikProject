using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DialogController;
using static DialogPanelScript;

public class MainController : MonoBehaviour
{
    QuestsController questsController;
    AchievementController achievementController;

    public Player playerScript;

    public InventoryStalker inventoryStalker;
    public HealthBarScript healthBarScript;

    public GameObject playerPanel;
    public GameObject dialogPanel;
    public GameObject questPanel;
    public GameObject wishPanel;
    public GameObject shopPanel;
    public GameObject characterPanel;
    public GameObject switchWeaponPanel;
    public GameObject achievementPanel;
    public GameObject backpackPanel;
    public GameObject buttlePanel;
    public GameObject enterDangeonPanel;
    public GameObject multiplayerPanel;
    public GameObject rewardPanel;
    public GameObject itemDeliveryPanel;
    public GameObject errorPanel;

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
    ErrorPanelScript errorPanelScript;
    ItemDeliveryPanelScript itemDeliveryPanelScript;
    AchievementPanelScript achievementPanelScript;

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;

    DedusController dedusController;
    GrandsonEugeneController grandsonEugeneController;

    DangeonInteractionScript dangeonInteractionScript;

    public bool is_keyboard_active = true;

    SpriteRenderer current_interaction_SR;

    public List<SpriteRenderer> list_of_interactable_SR = new List<SpriteRenderer>();
    public List<string> list_of_interactable_objects_names = new List<string>();

    public Dictionary<UseType, int> dict_useType_to_seconds_left;

    public Dictionary<string, NPCController> dict_npc_name_to_npcController;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        StuffSetActiveTrue();

        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();
        achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();
        wishPanelScript = wishPanel.GetComponent<WishPanelScript>();
        shopPanelScript = shopPanel.GetComponent<ShopPanelScript>();
        characterPanelScript = characterPanel.GetComponent<CharacterPanelScript>();
        buttlePanelScript = buttlePanel.GetComponent<ButtlePanelScript>();
        errorPanelScript = errorPanel.GetComponent<ErrorPanelScript>();
        itemDeliveryPanelScript = itemDeliveryPanel.GetComponent<ItemDeliveryPanelScript>();
        achievementPanelScript = achievementPanel.GetComponent<AchievementPanelScript>();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();
        rewardPanelScript = rewardPanel.GetComponent<RewardPanelScript>();

        Make_dict_npc_name_to_npcController();

        SetIconThinkingNPC();
    }

    void Start()
    {
        //backpackController.MakeDictionary();

        SetDangeonScripts();

        is_keyboard_active = true;

        StuffSetActiveFalse();

        ClearDictionary_useType_to_seconds_left();

        MusicManager.Instance.PlayPhantomMusicByIndex(0);
    }

    void Make_dict_npc_name_to_npcController()
    {
        dict_npc_name_to_npcController = new Dictionary<string, NPCController>();

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in npcs)
        {
            NPCController npcController = npc.GetComponent<NPCController>();

            if (npcController != null)
            {
                dict_npc_name_to_npcController[npcController.data.npc_name] = npcController;
            }
        }
    }

    void SetIconThinkingNPC()
    {
        foreach (string npc_name in dict_npc_name_to_npcController.Keys)
        {
            dict_npc_name_to_npcController[npc_name].IconThinkingSetActiveTrue();
        }
    }

    public void StartCountdownCoroutine(UseType useType)
    {
        StartCoroutine(CountdownCoroutine(useType));
    }

    private IEnumerator CountdownCoroutine(UseType useType)
    {
        while (dict_useType_to_seconds_left[useType] > 0)
        {
            foreach (BackpackIconScript backpackIconScript in backpackController.dict_useType_to_list_of_BackpackIconScripts[useType])
            {
                backpackIconScript.CloseIconForTime(dict_useType_to_seconds_left[useType]);
            }
            foreach (MiniSlotScript miniSlotScript in inventoryStalker.slotScripts_playerPanel)
            {
                if (miniSlotScript.slot_item is UsableItem usable_item)
                {
                    if (usable_item.useEffect.useType == useType)
                    {
                        miniSlotScript.CloseSlotForTime(dict_useType_to_seconds_left[useType]);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            dict_useType_to_seconds_left[useType]--;
        }

        foreach (BackpackIconScript backpackIconScript in backpackController.dict_useType_to_list_of_BackpackIconScripts[useType])
        {
            backpackIconScript.CloseIconForTime(dict_useType_to_seconds_left[useType]);
        }
        foreach (MiniSlotScript miniSlotScript in inventoryStalker.slotScripts_playerPanel)
        {
            if (miniSlotScript.slot_item is UsableItem usable_item)
            {
                if (usable_item.useEffect.useType == useType)
                {
                    miniSlotScript.CloseSlotForTime(dict_useType_to_seconds_left[useType]);
                }
            }
        }
    }

    void ClearDictionary_useType_to_seconds_left()
    {
        dict_useType_to_seconds_left = new Dictionary<UseType, int>();

        foreach (UseType useType in backpackController.list_of_use_types)
        {
            dict_useType_to_seconds_left[useType] = 0;
        }
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

    public void OpenErrorPanel(ErrorType errorType)
    {
        errorPanelScript.ShowError(errorType);
    }

    public void OpenItemDeliveryPanel(List<CollectableItem> list)
    {
        //Debug.Log("MainController  :  Open DeliveryPanel");
        itemDeliveryPanelScript.OpenPanel(list);
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
        errorPanel.SetActive(true);
        itemDeliveryPanel.SetActive(true);
        achievementPanel.SetActive(true);
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
        errorPanel.SetActive(false);
        itemDeliveryPanel.SetActive(false);
        achievementPanel.SetActive(false);

        rewardPanel.SetActive(false);
    }

    public void EnterDangeon()
    {
        Debug.Log("ENTER DANGEON");
        dangeonInteractionScript.EnterDangeon();
    }

    public void ShowPlayerPanel()
    {
        is_keyboard_active = true;
        playerPanel.SetActive(true);
    }

    public void HidePlayerPanel()
    {
        is_keyboard_active = false;
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
        backpackPanel.SetActive(true);
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

    public void OpenAchievementPanel()
    {
        achievementController.OpenAchievementPanel();
        TurnOffKeyboard();
    }

    public void CloseAchievementPanel()
    {
        achievementPanel.SetActive(false);
        TurnOnKeyboard();
    }

    public void OpenMultiplayerPanel()
    {
        multiplayerPanel.SetActive(true);
        TurnOffKeyboard();
    }

    public void CloseMultiplayerPanel()
    {
        multiplayerPanel.SetActive(false);
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
