using TMPro;
using UnityEngine;

public class QuestInfoScript : MonoBehaviour
{
    QuestsController questsController;

    public TextMeshProUGUI quest_title_TMP;
    public TextMeshProUGUI quest_subtitle_TMP;
    public TextMeshProUGUI quest_description_TMP;

    public GameObject rewardsGO;
    public GameObject questInfoGO;
    public GameObject claimRewardsButton;

    public GameObject trackButton;
    public GameObject dontTrackButton;

    public GameObject rewardPrefab;

    Quest quest;

    void Start()
    {
        questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();
    }

    public void DontTrackButton()
    {
        questsController.SetTrackTask();
    }

    public void TrackButton()
    {
        questsController.SetTrackTask(quest.title);
        Track(true);
    }

    public void Track(bool need_to_track)
    {
        trackButton.SetActive(!need_to_track);
        dontTrackButton.SetActive(need_to_track);
    }

    public void SetQuest(string quest_title)
    {
        if (questsController == null) questsController = GameObject.Find("QuestsController").GetComponent<QuestsController>();

        quest = questsController.dict_quest_name_to_quest[quest_title];

        quest_title_TMP.text = quest.title;

        if (quest.current_task != null)
        {
            quest_subtitle_TMP.text = quest.current_task.subtitle;
            quest_description_TMP.text = quest.current_task.description;
        }

        UpdateQuestIcon();
        SpawnRewards();
    }

    void UpdateQuestIcon()
    {
        if (quest.current_task == null)
        {
            questInfoGO.SetActive(false);
            claimRewardsButton.SetActive(true);

            trackButton.SetActive(false);
            dontTrackButton.SetActive(false);
        }
        else
        {
            questInfoGO.SetActive(true);
            claimRewardsButton.SetActive(false);
        }
    }

    void SpawnRewards()
    {
        foreach (Transform child in rewardsGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Reward reward in quest.rewards)
        {
            GameObject new_prefab = Instantiate(rewardPrefab, rewardsGO.transform);
            RewardIconScript new_prefab_script = new_prefab.GetComponent<RewardIconScript>();

            new_prefab_script.SetReward(reward);
        }
    }

    public void ClaimRewards()
    {
        questsController.ClaimRewardsOnQuest(quest.title);
        
        claimRewardsButton.SetActive(false);
    }
}
