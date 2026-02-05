using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WishPanelScript : MonoBehaviour
{
    MainController mainController;
    public BackPackController backpackController;

    public TextMeshProUGUI pink_wish_counter_text;
    public TextMeshProUGUI blue_wish_counter_text;

    public GameObject interactPanel;

    public GameObject pinkBackgroundPanel;
    public GameObject blueBackgroundPanel;

    public GameObject starsGO;
    public GameObject blueStarsGO;

    public GameObject pinkWishMadePanel;
    public GameObject blueWishMadePanel;

    public Image blueWishMadeRewardImage;
    public TextMeshProUGUI blueWishMadeRewardTMP;

    public GameObject pinkWishInteractPanel;
    public GameObject blueWishInteractPanel;

    private Animator pink_stars_animator;
    private Animator blue_stars_animator;

    private Animator current_animator;

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

    List<Item> rewards = new List<Item>();

    int current_reward_index = 0;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        pink_stars_animator = starsGO.GetComponent<Animator>();
        blue_stars_animator = blueStarsGO.GetComponent<Animator>();

        pink_wish_parameters = new WishParameters();
        blue_wish_parameters = new WishParameters();
    }

    private void Start()
    {
        blueWishMadePanel.SetActive(false);
        pinkWishMadePanel.SetActive(false);

        blueWishInteractPanel.SetActive(false);
        pinkWishInteractPanel.SetActive(true);

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
        rewards = new List<Item>();
        current_reward_index = 0;

        bool success = UseWish(10);

        if (success)
        {
            CloseWishInteractPanel();

            current_animator.SetBool("is_wishing", true);

            ComputeRewards(10);

            Invoke("StopWish", 0.3f);
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
        rewards = new List<Item>();
        current_reward_index = 0;

        bool success = UseWish(1);

        if (success)
        {
            CloseWishInteractPanel();

            current_animator.SetBool("is_wishing", true);

            ComputeRewards(1);

            Invoke("StopWish", 0.3f);
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

        //rewards.Reverse();
    }

    void ObtainXStarReward(int star)
    {
        System.Random rand = new System.Random();

        Debug.Log($"GET {star}* REWARD");
        int reward_index = rand.Next(0, dict_star_to_list_of_reward_id[star].Count);
        int reward_id = dict_star_to_list_of_reward_id[star][reward_index];

        Item new_item = backpackController.dict_id_to_item[reward_id];

        //new_weapon.amount++;
        rewards.Add(new_item);

        ObtainItem(reward_id);
    }

    void ObtainItem(int id)
    {
        backpackController.IncreaceItemByName(backpackController.dict_id_to_item[id].item_name, 1);
    }

    void StopWish()
    {
        current_animator.SetBool("is_wishing", false);
    }

    public void CompleteWish()
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

        Item new_item = rewards[current_reward_index];
        //Item new_item = rewards[rewards.Count - 1];
        //rewards.RemoveAt(rewards.Count - 1);
        current_reward_index++;

        blueWishMadeRewardImage.sprite = new_item.sprite;
        blueWishMadeRewardTMP.text = new_item.item_name;
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
            current_animator = pink_stars_animator;
        }
        else
        {
            is_pink = false;
            current_animator = blue_stars_animator;
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
