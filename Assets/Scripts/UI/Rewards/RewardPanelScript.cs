using TMPro;
using UnityEngine;

public class RewardPanelScript : MonoBehaviour
{
    MainController mainController;

    int blue_reward_amount;
    int pink_reward_amount;
    public TextMeshProUGUI blue_reward_amountTMP;
    public TextMeshProUGUI pink_reward_amountTMP;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void SetRewardAmount(int blue_number, int pink_number)
    {
        gameObject.SetActive(true);

        blue_reward_amount = blue_number;
        blue_reward_amountTMP.text = blue_number.ToString();

        pink_reward_amount = blue_number;
        pink_reward_amountTMP.text = blue_number.ToString();
    }

    public void ClaimRewards()
    {
        mainController.ClaimReward(blue_reward_amount, pink_reward_amount);

        gameObject.SetActive(false);
    }
}
