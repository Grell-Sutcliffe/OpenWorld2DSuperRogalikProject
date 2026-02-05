using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WishPanelScript : MonoBehaviour
{
    MainController mainController;
    public BackPackController backpackController;
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

    public GameObject pinkWishInteractPanel;
    public GameObject blueWishInteractPanel;

    public GameObject wishAnimationImage;

    /*
    private Animator pink_stars_animator;
    private Animator blue_stars_animator;

    private Animator current_animator;
    */

    private Animator wish_animator;

    bool is_pink = true;

    public class WishParameters
    {
        public float chance_to_get_5_star = 0.01f;
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
    }

    WishParameters pink_wish_parameters;
    WishParameters blue_wish_parameters;

    Dictionary<int, List<int>> dict_star_to_list_of_reward_id = new Dictionary<int, List<int>>();

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

    List<WishReward> rewards = new List<WishReward>();

    int current_reward_index = 0;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();

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

        MakeDictionary();
    }

    void MakeDictionary()
    {
        for (int i = 2; i <= 5; i++)
        {
            dict_star_to_list_of_reward_id[i] = new List<int>();
        }

        foreach (int id in backpackController.dict_id_to_item.Keys)
        {
            if (backpackController.dict_id_to_item[id].item_type == ItemType.Weapon)
            {
                if (backpackController.dict_id_to_item[id] is Weapon weapon) 
                {
                    dict_star_to_list_of_reward_id[weapon.stars].Add(id);
                }
            }
            else if (backpackController.dict_id_to_item[id].item_type == ItemType.Materials)
            {
                if (backpackController.dict_id_to_item[id] is ItemForSale item)
                {
                    dict_star_to_list_of_reward_id[2].Add(id);
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
        return mainController.UseWish(is_pink, number);
    }

    void ComputeRewards(int number)
    {
        CloseWishInteractPanel();

        wishAnimationImage.SetActive(true);

        System.Random rand = new System.Random();

        for (int i = 0; i < number; i++)
        {
            int chance = rand.Next(0, 101);
            if (!is_pink)
            {
                blue_wish_parameters.current_wish_made_amount++;

                if (blue_wish_parameters.current_wish_made_amount >= blue_wish_parameters.next_time_get_5_star_wish_amount)
                {
                    ObtainXStarReward(5);
                    blue_wish_parameters.next_time_get_5_star_wish_amount += blue_wish_parameters.get_5_star_wish_amount;
                    continue;
                }
                if (chance < blue_wish_parameters.chance_to_get_5_star * 100)
                {
                    ObtainXStarReward(5);
                    continue;
                }

                if (blue_wish_parameters.current_wish_made_amount >= blue_wish_parameters.next_time_get_4_star_wish_amount)
                {
                    ObtainXStarReward(4);
                    blue_wish_parameters.next_time_get_4_star_wish_amount += blue_wish_parameters.get_4_star_wish_amount;
                    continue;
                }
                if (chance < blue_wish_parameters.chance_to_get_4_star * 100)
                {
                    ObtainXStarReward(4);
                    continue;
                }

                if (blue_wish_parameters.current_wish_made_amount >= blue_wish_parameters.next_time_get_3_star_wish_amount)
                {
                    ObtainXStarReward(3);
                    blue_wish_parameters.next_time_get_3_star_wish_amount += blue_wish_parameters.get_3_star_wish_amount;
                    continue;
                }
                if (chance < blue_wish_parameters.chance_to_get_3_star * 100)
                {
                    ObtainXStarReward(3);
                    continue;
                }

                ObtainXStarReward(2);
            }
        }

        wish_animator.SetBool("is_wishing", true);
        wish_animator.SetBool("is_wish_pink", is_pink);

        //rewards.Reverse();
    }

    void ObtainXStarReward(int star)
    {
        if (star == 5) wish_animator.SetBool("is_gold", true);
        if (star == 4) wish_animator.SetBool("is_purple", true);
        if (star <= 3) wish_animator.SetBool("is_blue", true);

        Debug.Log($"new reward. star = {star}");

        System.Random rand = new System.Random();

        Debug.Log($"GET {star}* REWARD");
        int reward_index = rand.Next(0, dict_star_to_list_of_reward_id[star].Count);
        int reward_id = dict_star_to_list_of_reward_id[star][reward_index];

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
            blueWishMadeRewardWeaponElementImage.sprite = characterPanelScript.dict_element_type_to_element[weapon.elementalDamage.element_type].sprite;
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
            OpenBlueWishMade();
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
