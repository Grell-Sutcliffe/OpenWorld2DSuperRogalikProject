using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WishReward
{
    public Item item;
    public int star;

    public WishReward(Item item, int star)
    {
        this.item = item;
        this.star = star;
    }
}

public class WishPanelScript : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;
    public CharacterPanelScript characterPanelScript;

    public TextMeshProUGUI pink_wish_counter_text;
    public TextMeshProUGUI blue_wish_counter_text;

    public GameObject interactPanel;

    public GameObject pinkBackgroundPanel;
    public GameObject blueBackgroundPanel;

    public GameObject starsGO;
    public GameObject blueStarsGO;

    public GameObject pinkWishMadePanel;
    public GameObject pinkWishMadePanel_gold;
    public GameObject pinkWishMadePanel_purple;
    public GameObject pinkWishMadePanel_blue;
    public GameObject blueWishMadePanel;
    public GameObject blueWishMadePanel_gold;
    public GameObject blueWishMadePanel_purple;
    public GameObject blueWishMadePanel_blue;

    public Image blueWishMadeRewardImage;
    public TextMeshProUGUI blueWishMadeRewardTMP;

    public GameObject blueWishMadeRewardWeaponStatsGO;
    public Image blueWishMadeRewardWeaponElementImage;
    public TextMeshProUGUI blueWishMadeRewardStarsTMP;

    public Image pinkWishMadeRewardImage;
    public TextMeshProUGUI pinkWishMadeRewardTMP;

    public GameObject pinkWishMadeRewardWeaponStatsGO;
    public Image pinkWishMadeRewardWeaponElementImage;
    public TextMeshProUGUI pinkWishMadeRewardStarsTMP;

    public GameObject pinkWishInteractPanel;
    public GameObject blueWishInteractPanel;

    public GameObject wishAnimationImage;

    /*
    private Animator pink_stars_animator;
    private Animator blue_stars_animator;

    private Animator current_animator;
    */

    private Animator wish_animator;

    private const string WishSaveKey = "wish_parameters_save";

    bool is_pink = true;

    [System.Serializable]
    public class WishSaveData
    {
        public WishParameters pinkWishParameters;
        public WishParameters blueWishParameters;
    }

    public void SaveWishParameters()
    {
        WishSaveData saveData = new WishSaveData();

        saveData.pinkWishParameters = pink_wish_parameters;
        saveData.blueWishParameters = blue_wish_parameters;

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(WishSaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Wish parameters saved: " + json);
    }

    public void LoadWishParameters()
    {
        if (!PlayerPrefs.HasKey(WishSaveKey))
        {
            Debug.Log("No wish parameters save found");
            return;
        }

        string json = PlayerPrefs.GetString(WishSaveKey);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("Wish parameters save is empty");
            return;
        }

        WishSaveData saveData = JsonUtility.FromJson<WishSaveData>(json);

        if (saveData == null)
        {
            Debug.LogWarning("Wish parameters save is broken");
            return;
        }

        if (saveData.pinkWishParameters != null)
            pink_wish_parameters = new WishParameters(saveData.pinkWishParameters);

        if (saveData.blueWishParameters != null)
            blue_wish_parameters = new WishParameters(saveData.blueWishParameters);

        Debug.Log("Wish parameters loaded: " + json);
    }

    public void DeleteWishParameters()
    {
        PlayerPrefs.DeleteKey(WishSaveKey);
        PlayerPrefs.Save();

        pink_wish_parameters = new WishParameters();
        blue_wish_parameters = new WishParameters();

        Debug.Log("Wish parameters reset to default");
    }

    [System.Serializable]
    public class WishParameters
    {
        public float chance_to_get_5_star = 0.01f;  // 0,01
        public float chance_to_get_4_star = 0.05f;
        public float chance_to_get_3_star = 0.2f;
        public int get_5_star_wish_amount = 60;
        public int get_4_star_wish_amount = 10;
        public int get_3_star_wish_amount = 5;
        public int current_wish_made_amount = 0;
        public int next_time_get_5_star_wish_amount = 0;
        public int next_time_get_4_star_wish_amount = 0;
        public int next_time_get_3_star_wish_amount = 0;

        public WishParameters(float chance_to_get_5_star_ = 0.01f, float chance_to_get_4_star_ = 0.05f, float chance_to_get_3_star_ = 0.2f, int get_5_star_wish_amount_ = 60, int get_4_star_wish_amount_ = 10, int get_3_star_wish_amount_ = 5)
        {
            chance_to_get_5_star = chance_to_get_5_star_;
            chance_to_get_4_star = chance_to_get_4_star_;
            chance_to_get_3_star = chance_to_get_3_star_;
            get_5_star_wish_amount = get_5_star_wish_amount_;
            get_4_star_wish_amount = get_4_star_wish_amount_;
            get_3_star_wish_amount = get_3_star_wish_amount_;
            current_wish_made_amount = 0;
            next_time_get_5_star_wish_amount = get_5_star_wish_amount_;
            next_time_get_4_star_wish_amount = get_4_star_wish_amount_;
            next_time_get_3_star_wish_amount = get_3_star_wish_amount_;
        }

        public WishParameters(WishParameters wishParameters)
        {
            this.chance_to_get_5_star = wishParameters.chance_to_get_5_star;
            this.chance_to_get_4_star = wishParameters.chance_to_get_4_star;
            this.chance_to_get_3_star = wishParameters.chance_to_get_3_star;
            this.get_5_star_wish_amount = wishParameters.get_5_star_wish_amount;
            this.get_4_star_wish_amount = wishParameters.get_4_star_wish_amount;
            this.get_3_star_wish_amount = wishParameters.get_3_star_wish_amount;
            this.current_wish_made_amount = wishParameters.current_wish_made_amount;
            this.next_time_get_5_star_wish_amount = wishParameters.next_time_get_5_star_wish_amount;
            this.next_time_get_4_star_wish_amount = wishParameters.next_time_get_4_star_wish_amount;
            this.next_time_get_3_star_wish_amount = wishParameters.next_time_get_3_star_wish_amount;
        }
    }

    WishParameters pink_wish_parameters;
    WishParameters blue_wish_parameters;

    List<int> list_of_material_reward_id;
    Dictionary<int, List<int>> dict_star_to_list_of_sword_id = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> dict_star_to_list_of_stick_id = new Dictionary<int, List<int>>();

    List<WishReward> rewards = new List<WishReward>();

    int current_reward_index = 0;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        /*
        pink_stars_animator = starsGO.GetComponent<Animator>();
        blue_stars_animator = blueStarsGO.GetComponent<Animator>();
        */

        wish_animator = wishAnimationImage.GetComponent<Animator>();

        pink_wish_parameters = new WishParameters();
        blue_wish_parameters = new WishParameters();
    }

    private void Start()
    {
        blueWishMadePanel.SetActive(false);
        pinkWishMadePanel.SetActive(false);

        wishAnimationImage.SetActive(false);

        blueWishInteractPanel.SetActive(false);
        pinkWishInteractPanel.SetActive(true);

        if (wish_animator == null)
        {
            wish_animator = wishAnimationImage.GetComponent<Animator>();
        }
        if (wish_animator == null)
        {
            Debug.LogError("ERROR: Can't find wish_animator");
        }

        LoadWishParameters();

        MakeDictionary();
    }

    void MakeDictionary()
    {
        list_of_material_reward_id = new List<int>();

        for (int i = 3; i <= 5; i++)
        {
            dict_star_to_list_of_sword_id[i] = new List<int>();
            dict_star_to_list_of_stick_id[i] = new List<int>();
        }

        foreach (int id in backpackController.dict_id_to_item.Keys)
        {
            if (backpackController.dict_id_to_item[id].item_type == ItemType.Weapon)
            {
                if (backpackController.dict_id_to_item[id] is Weapon weapon)
                {
                    // dict_star_to_list_of_reward_id[weapon.stars].Add(id);
                    if (weapon.weapon_type == WeaponType.Sword)
                    {
                        dict_star_to_list_of_sword_id[weapon.stars].Add(id);
                    }
                    else if (weapon.weapon_type == WeaponType.Stick)
                    {
                        dict_star_to_list_of_stick_id[weapon.stars].Add(id);
                    }
                }
            }
            else if (backpackController.dict_id_to_item[id].item_type == ItemType.Materials)
            {
                if (backpackController.dict_id_to_item[id] is ItemForSale item)
                {
                    list_of_material_reward_id.Add(id);
                }
            }
        }

        //Debug.LogError($"backpackController.dict_id_to_item.Keys.Count = {backpackController.dict_id_to_item.Keys.Count}\ndict_star_to_list_of_reward_id[2].Count = {dict_star_to_list_of_reward_id[2].Count}");
    }

    public void OpenWishPanel()
    {
        gameObject.SetActive(true);
        mainController.UpdateWishPanelInfo();
        SwitchToPinkWish();
    }

    public void StartWish10()
    {
        rewards = new List<WishReward>();
        current_reward_index = 0;

        bool success = UseWish(10);

        if (success)
        {
            ComputeRewards(10);

            //current_animator.SetBool("is_wishing", true);

            //Invoke("StopWish", 0.3f);
        }
        else
        {
            Debug.Log("NOT ENOUGTH WISHES");
            return;
        }
        mainController.UpdateWishPanelInfo();
    }

    public void StartEmptyWish()
    {

    }

    public void StartWish()
    {
        rewards = new List<WishReward>();
        current_reward_index = 0;

        bool success = UseWish(1);

        if (success)
        {
            //current_animator.SetBool("is_wishing", true);

            ComputeRewards(1);

            //Invoke("StopWish", 0.3f);
        }
        else
        {
            Debug.Log("NOT ENOUGTH WISHES");
            return;
        }
        mainController.UpdateWishPanelInfo();
    }

    bool UseWish(int number)
    {
        bool success = mainController.UseWish(is_pink, number);
        if (!success)
        {
            mainController.OpenErrorPanel(ErrorType.NotEnoughMaterials);
        }
        return success;
    }

    void ComputeRewards(int number)
    {
        CloseWishInteractPanel();

        wishAnimationImage.SetActive(true);

        System.Random rand = new System.Random();

        WishParameters temp_wishParameters = new WishParameters(is_pink ? pink_wish_parameters : blue_wish_parameters);

        for (int i = 0; i < number; i++)
        {
            int chance = rand.Next(0, 101);

            temp_wishParameters.current_wish_made_amount++;

            if (temp_wishParameters.current_wish_made_amount >= temp_wishParameters.next_time_get_5_star_wish_amount)
            {
                ObtainXStarReward(5);
                temp_wishParameters.next_time_get_5_star_wish_amount += temp_wishParameters.get_5_star_wish_amount;
                continue;
            }
            if (chance < temp_wishParameters.chance_to_get_5_star * 100)
            {
                ObtainXStarReward(5);
                continue;
            }

            if (temp_wishParameters.current_wish_made_amount >= temp_wishParameters.next_time_get_4_star_wish_amount)
            {
                ObtainXStarReward(4);
                temp_wishParameters.next_time_get_4_star_wish_amount += temp_wishParameters.get_4_star_wish_amount;
                continue;
            }
            if (chance < temp_wishParameters.chance_to_get_4_star * 100)
            {
                ObtainXStarReward(4);
                continue;
            }

            if (temp_wishParameters.current_wish_made_amount >= temp_wishParameters.next_time_get_3_star_wish_amount)
            {
                ObtainXStarReward(3);
                temp_wishParameters.next_time_get_3_star_wish_amount += temp_wishParameters.get_3_star_wish_amount;
                continue;
            }
            if (chance < temp_wishParameters.chance_to_get_3_star * 100)
            {
                ObtainXStarReward(3);
                continue;
            }

            ObtainXStarReward(2);
        }

        if (is_pink)
        {
            pink_wish_parameters = new WishParameters(temp_wishParameters);
        }
        else
        {
            blue_wish_parameters = new WishParameters(temp_wishParameters);
        }

        SaveWishParameters();

        wish_animator.SetBool("is_wishing", true);
        wish_animator.SetBool("is_wish_pink", is_pink);

        //rewards.Reverse();

        EventBus.Raise(new WishMadeEvent(number, rewards));
    }

    void ObtainXStarReward(int star)
    {
        if (star == 5) wish_animator.SetBool("is_gold", true);
        if (star == 4) wish_animator.SetBool("is_purple", true);
        if (star <= 3) wish_animator.SetBool("is_blue", true);

        Debug.Log($"new reward. star = {star}");

        System.Random rand = new System.Random();

        Debug.Log($"GET {star}* REWARD");

        int reward_id = 0;

        if (star == 2)
        {
            int reward_index = rand.Next(0, list_of_material_reward_id.Count);
            reward_id = list_of_material_reward_id[reward_index];
        }
        else
        {
            if (is_pink)
            {
                int reward_index = rand.Next(0, dict_star_to_list_of_stick_id[star].Count);
                reward_id = dict_star_to_list_of_stick_id[star][reward_index];
            }
            else
            {
                int reward_index = rand.Next(0, dict_star_to_list_of_sword_id[star].Count);
                reward_id = dict_star_to_list_of_sword_id[star][reward_index];
            }
        }

        Item new_item = backpackController.dict_id_to_item[reward_id];

        //new_weapon.amount++;
        rewards.Add(new WishReward(new_item, star));

        ObtainItem(reward_id);
    }

    void ObtainItem(int id)
    {
        backpackController.IncreaceItemByName(backpackController.dict_id_to_item[id].item_name, 1);
    }

    public void StopWish()
    {
        Debug.Log("WISH STOPPED");

        //current_animator.SetBool("is_wishing", false);

        wish_animator.SetBool("is_wishing", false);
        wish_animator.SetBool("is_wish_pink", true);

        wish_animator.SetBool("is_gold", false);
        wish_animator.SetBool("is_purple", false);
        wish_animator.SetBool("is_blue", false);

        wishAnimationImage.SetActive(false);
        CompleteWish();
    }

    void CompleteWish()
    {
        Debug.Log("WISH MADE");

        if (is_pink)
        {
            OpenPinkWishInteractPanel();
            OpenPinkWishMade();
        }
        else
        {
            OpenBlueWishInteractPanel();
            OpenBlueWishMade();
        }
    }

    public void CompleteBlueWish()
    {
        Debug.Log("BLUE WISH MADE");

        OpenBlueWishInteractPanel();
        OpenBlueWishMade();
    }

    void OpenPinkWishInteractPanel()
    {
        interactPanel.SetActive(true);
        pinkWishInteractPanel.SetActive(true);
    }

    void OpenBlueWishInteractPanel()
    {
        interactPanel.SetActive(true);
        blueWishInteractPanel.SetActive(true);
    }

    void CloseWishInteractPanel()
    {
        interactPanel.SetActive(false);
        pinkWishInteractPanel.SetActive(false);
        blueWishInteractPanel.SetActive(false);
    }

    void OpenPinkWishMade()
    {
        pinkWishMadePanel.SetActive(true);

        pinkWishMadeRewardWeaponStatsGO.SetActive(false);

        pinkWishMadePanel_gold.SetActive(false);
        pinkWishMadePanel_purple.SetActive(false);
        pinkWishMadePanel_blue.SetActive(false);

        WishReward new_reward = rewards[current_reward_index];
        //Item new_item = rewards[rewards.Count - 1];
        //rewards.RemoveAt(rewards.Count - 1);
        current_reward_index++;

        if (new_reward.star == 5) pinkWishMadePanel_gold.SetActive(true);
        if (new_reward.star == 4) pinkWishMadePanel_purple.SetActive(true);
        if (new_reward.star <= 3) pinkWishMadePanel_blue.SetActive(true);

        if (new_reward.item is Weapon weapon)
        {
            pinkWishMadeRewardWeaponStatsGO.SetActive(true);
            pinkWishMadeRewardWeaponElementImage.sprite = characterPanelScript.dict_element_type_to_element[weapon.element_type].sprite;
            pinkWishMadeRewardStarsTMP.text = new_reward.star.ToString();
        }

        pinkWishMadeRewardImage.sprite = new_reward.item.sprite;
        pinkWishMadeRewardTMP.text = new_reward.item.item_name;
    }

    void OpenBlueWishMade()
    {
        blueWishMadePanel.SetActive(true);

        blueWishMadeRewardWeaponStatsGO.SetActive(false);

        blueWishMadePanel_gold.SetActive(false);
        blueWishMadePanel_purple.SetActive(false);
        blueWishMadePanel_blue.SetActive(false);

        WishReward new_reward = rewards[current_reward_index];
        //Item new_item = rewards[rewards.Count - 1];
        //rewards.RemoveAt(rewards.Count - 1);
        current_reward_index++;

        if (new_reward.star == 5) blueWishMadePanel_gold.SetActive(true);
        if (new_reward.star == 4) blueWishMadePanel_purple.SetActive(true);
        if (new_reward.star <= 3) blueWishMadePanel_blue.SetActive(true);

        if (new_reward.item is Weapon weapon)
        {
            blueWishMadeRewardWeaponStatsGO.SetActive(true);
            blueWishMadeRewardWeaponElementImage.sprite = characterPanelScript.dict_element_type_to_element[weapon.element_type].sprite;
            blueWishMadeRewardStarsTMP.text = new_reward.star.ToString();
        }

        blueWishMadeRewardImage.sprite = new_reward.item.sprite;
        blueWishMadeRewardTMP.text = new_reward.item.item_name;
    }

    public void CloseWishMade()
    {
        //if (rewards.Count > 0)
        if (current_reward_index < rewards.Count)
        {
            if (is_pink)
            {
                OpenPinkWishMade();
            }
            else
            {
                OpenBlueWishMade();
            }
        }
        else
        {
            pinkWishMadePanel.SetActive(false);
            blueWishMadePanel.SetActive(false);
        }
    }

    public void SwitchToPinkWish()
    {
        PinkPanelSetActive(true);
    }

    public void SwitchToBlueWish()
    {
        PinkPanelSetActive(false);
    }

    void PinkPanelSetActive(bool is_active)
    {
        pinkBackgroundPanel.SetActive(is_active);
        blueBackgroundPanel.SetActive(!is_active);

        pinkWishInteractPanel.SetActive(is_active);
        blueWishInteractPanel.SetActive(!is_active);

        if (is_active)
        {
            is_pink = true;
            //current_animator = pink_stars_animator;
        }
        else
        {
            is_pink = false;
            //current_animator = blue_stars_animator;
        }
    }

    public void UpdatePinkWishInfo(int new_counter)
    {
        pink_wish_counter_text.text = new_counter.ToString();
    }

    public void UpdateBlueWishInfo(int new_counter)
    {
        blue_wish_counter_text.text = new_counter.ToString();
    }
}
