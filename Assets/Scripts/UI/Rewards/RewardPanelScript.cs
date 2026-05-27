using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanelScript : MonoBehaviour
{
    //MainController mainController;
    BackPackController backpackController;

    public Image reward_1_image;
    public Image reward_2_image;

    public TextMeshProUGUI reward_1_amountTMP;
    public TextMeshProUGUI reward_2_amountTMP;

    private Reward reward_1;
    private Reward reward_2;

    private void Awake()
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();
    }

    void Start()
    {
        //mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();
    }

    public void SetRewards(Reward reward_1, Reward reward_2)
    {
        gameObject.SetActive(true);

        this.reward_1 = reward_1;
        this.reward_2 = reward_2;

        reward_1_image.sprite = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[reward_1.item_name]].sprite;
        reward_2_image.sprite = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[reward_2.item_name]].sprite;

        reward_1_amountTMP.text = reward_1.amount.ToString();
        reward_2_amountTMP.text = reward_2.amount.ToString();
    }

    public void ClaimRewards()
    {
        backpackController.IncreaceItemByName(reward_1.item_name, reward_1.amount);
        backpackController.IncreaceItemByName(reward_2.item_name, reward_2.amount);

        gameObject.SetActive(false);
    }
}
