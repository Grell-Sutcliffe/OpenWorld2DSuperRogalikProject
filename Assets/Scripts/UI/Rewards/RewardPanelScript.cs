using TMPro;
using UnityEngine;

public class RewardPanelScript : MonoBehaviour
{
    MainController mainController;

    int reward_amount;
    public TextMeshProUGUI reward_amountTMP;

    void Start()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
    }

    public void SetRewardAmount(int number)
    {
        gameObject.SetActive(true);

        reward_amount = number;
        reward_amountTMP.text = number.ToString();
    }

    public void ClaimRewards()
    {
        mainController.ClaimReward(reward_amount);

        gameObject.SetActive(false);
    }
}
