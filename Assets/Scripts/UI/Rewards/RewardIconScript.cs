using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardIconScript : MonoBehaviour
{
    public Image rewardImage;
    public TextMeshProUGUI rewardAmountTMP;

    Reward reward;

    public void SetReward(Reward reward)
    {
        this.reward = reward;

        //rewardImage.sprite = reward.i
    }
}
