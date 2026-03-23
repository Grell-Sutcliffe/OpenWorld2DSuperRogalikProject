using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardIconScript : MonoBehaviour
{
    BackPackController backpackController;

    public Image rewardImage;
    public TextMeshProUGUI rewardAmountTMP;

    Reward reward;

    public void SetReward(Reward reward)
    {
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        this.reward = reward;

        rewardImage.sprite = backpackController.dict_id_to_item[backpackController.dict_item_name_to_id[reward.item_name]].sprite;
        rewardAmountTMP.text = reward.amount.ToString();
    }
}
