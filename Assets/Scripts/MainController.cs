using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static DialogPanelScript;

public class MainController : MonoBehaviour
{
    QuestsController questsController;

    public HealthBarScript healthBarScript;

    public GameObject playerPanel;
    public GameObject dialogPanel;
    public GameObject questPanel;
    public GameObject wishPanel;
    public GameObject shopPanel;
    public GameObject characterPanel;
    public GameObject switchWeaponPanel;
    public GameObject backpackPanel;
    public GameObject enterDangeonPanel;

    public GameObject taskShower;

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

    InteractKeyListener keyListener;
    DialogPanelScript dialogPanelScript;

    public DedusDialogScript dedusDialogScript;
    public GrandsonEugeneDialogScript grandsonEugeneDialogScript;
    public DoggyDialogScript doggyDialogScript;

    DedusController dedusController;
    GrandsonEugeneController grandsonEugeneController;

    DoggyInteractionScript doggyInteractionScript;
    DangeonInteractionScript dangeonInteractionScript;
    BookInteractionScript bookInteractionScript;

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

        scrollInteractionScript = gameObject.GetComponent<ScrollInteractionScript>();
        backpackController = backpackPanel.GetComponent<BackPackController>();
        wishPanelScript = wishPanel.GetComponent<WishPanelScript>();
        shopPanelScript = shopPanel.GetComponent<ShopPanelScript>();
        characterPanelScript = characterPanel.GetComponent<CharacterPanelScript>();

        keyListener = gameObject.GetComponent<InteractKeyListener>();
        dialogPanelScript = dialogPanel.GetComponent<DialogPanelScript>();
    }

    void Start()
    {
        //backpackController.MakeDictionary();

        SetDedusScripts();
        SetGrandsonEugeneScripts();
        SetDangeonScripts();
        SetDoggyScripts();
        SetBookScripts();

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
        Debug.Log("SHOW INTERACTION");

        foreach (string c in list_of_interactable_objects_names)
        {
            Debug.Log($"{c}");
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

    public void PressF()
    {
        if (list_of_interactable_objects_names.Count == 0) return;

        //Debug.Log($"list.Count = {list_of_interactable_objects_names.Count}, dangeon_F = {dangeon_F}, dangeon.name = {Dangeon.name}, list_names[cur_ind] = {list_of_interactable_objects_names[scrollInteractionScript.current_index]}");

        /*
        if (dedus_F && Dedus.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (dedusDialogScript == null) SetDedusScripts();
            InteractDedus();
        }
        else if (grandsonEugene_F && GrandsonEugene.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (grandsonEugeneDialogScript == null) SetGrandsonEugeneScripts();
            InteractGrandsonEugene();
        }
        else if (dangeon_F && Dangeon.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (dangeonInteractionScript == null) SetDangeonScripts();
            InteractDangeon();
        }
        else if (doggy_F && Doggy.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (dangeonInteractionScript == null) SetDoggyScripts();
            InteractDoggy();
        }
        else if (book_F && Book.name == list_of_interactable_objects_names[scrollInteractionScript.current_index])
        {
            if (bookInteractionScript == null) SetBookScripts();
            TakeBook();
        }
        */
    }

    public void InteractDedus()
    {
        dedusDialogScript.StartDialog();
    }

    public void InteractGrandsonEugene()
    {
        grandsonEugeneDialogScript.StartDialog();
    }

    public void InteractBook()
    {
        TakeBook();
    }

    public void InteractDoggy()
    {
        doggyDialogScript.StartDialog();
    }

    public void InteractDangeon()
    {
        ShowEnterDangeonPanel();
    }

    public void StartDialog(string speaker_text, SpeachNode speach_node)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_node);
    }

    public void StartDialog(string speaker_text, SpeachTree speach_tree)
    {
        dialogPanelScript.StartDialog(speaker_text, speach_tree);
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

        dedus_F = false;
        grandsonEugene_F = false;
        dangeon_F = false;
    }

    public void TakeBook()
    {
        // Debug.Log("Take book!");
        // bookInteractionScript.TakeBook();
        //backpackController.TakeByName(backpackController.book_name);
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

    public void DedusOn()
    {
        dedus_F = true;
        ShowInteraction();
        Debug.Log("Dedus on");
    }

    public void DedusOff()
    {
        dedus_F = false;
        ShowInteraction();
        Debug.Log("Dedus off");
    }

    public void GrandsonEugeneOn()
    {
        grandsonEugene_F = true;
        ShowInteraction();
        Debug.Log("Grandson on");
    }

    public void GrandsonEugeneOff()
    {
        grandsonEugene_F = false;
        ShowInteraction();
        Debug.Log("Grandson off");
    }

    public void DangeonOn()
    {
        dangeon_F = true;
        ShowInteraction();
        Debug.Log("Dangeon on");
    }

    public void DangeonOff()
    {
        dangeon_F = false;
        ShowInteraction();
        Debug.Log("Dangeon off");
    }

    public void DoggyOn()
    {
        doggy_F = true;
        ShowInteraction();
        Debug.Log("Doggy on");
    }

    public void DoggyOff()
    {
        doggy_F = false;
        ShowInteraction();
        Debug.Log("Doggy off");
    }

    public void BookOn()
    {
        book_F = true;
        ShowInteraction();
        Debug.Log("Book on");
    }

    public void BookOff()
    {
        book_F = false;
        ShowInteraction();
        Debug.Log("Book off");
    }

    void SetDedusScripts()
    {
        if (Dedus == null) Dedus = GameObject.Find("Dedus");
        if (Dedus != null)
        {
            dedusController = Dedus.GetComponent<DedusController>();
            dedusDialogScript = Dedus.GetComponent<DedusDialogScript>();
        }
    }

    void SetGrandsonEugeneScripts()
    {
        if (GrandsonEugene == null) GrandsonEugene = GameObject.Find("GrandsonEugine");
        if (GrandsonEugene != null)
        {
            grandsonEugeneController = GrandsonEugene.GetComponent<GrandsonEugeneController>();
            grandsonEugeneDialogScript = GrandsonEugene.GetComponent<GrandsonEugeneDialogScript>();
        }
    }

    void SetDoggyScripts()
    {
        if (Doggy == null) Doggy = GameObject.Find("Doggy");
        if (Doggy != null)
        {
            doggyInteractionScript = Doggy.GetComponent<DoggyInteractionScript>();
            doggyDialogScript = Doggy.GetComponent<DoggyDialogScript>();
        }
    }

    void SetDangeonScripts()
    {
        if (Dangeon == null) Dangeon = GameObject.Find("Dangeon");
        if (Dangeon != null)
        {
            dangeonInteractionScript = Dangeon.GetComponent<DangeonInteractionScript>();
        }
    }

    void SetBookScripts()
    {
        if (Book == null) Book = GameObject.Find("Book");
        if (Book != null)
        {
            bookInteractionScript = Book.GetComponent<BookInteractionScript>();
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
