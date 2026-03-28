using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementPanelScript : MonoBehaviour
{
    AchievementController achievementController;

    public GameObject content_GO;

    RectTransform content_rect_transform;

    public GameObject achievementIconPrefab;

    int item_counter = 0;
    public int item_height = 300;
    public int space_between_items = 10;

    private void Start()
    {
        achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();

        content_rect_transform = content_GO.GetComponent<RectTransform>();
    }

    public void OpenPanel()
    {
        if (achievementController == null) achievementController = GameObject.Find("AchievementController").GetComponent<AchievementController>();

        if (content_rect_transform == null) content_rect_transform = content_GO.GetComponent<RectTransform>();

        gameObject.SetActive(true);
        
        UpdateContent();
    }

    public void UpdateContent(AchievementType achievementType = AchievementType.All)
    {
        CountItems(achievementType);
        ClearContent();
        ChangePanelHeight(achievementType);
    }

    void CountItems(AchievementType achievementType = AchievementType.All)
    {
        item_counter = 0;

        foreach (string achievement_title in achievementController.dict_achievementType_to_list_of_achievement_list[achievementType])
        {
            if (achievementController.dict_achievement_title_to_achievement[achievement_title].is_completed == false)
            {
                item_counter++;
            }
        }
    }

    void ClearContent()
    {
        foreach (Transform child in content_GO.transform)
        {
            //Debug.Log($"delete item");
            Destroy(child.gameObject);
        }
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void ChangePanelHeight(AchievementType achievementType = AchievementType.All)
    {
        int new_height = item_counter * item_height + (item_counter - 1) * space_between_items;
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, new_height);

        foreach (string achievement_title in achievementController.dict_achievementType_to_list_of_achievement_list[achievementType])
        {
            if (achievementController.dict_achievement_title_to_achievement[achievement_title].is_completed == false)
            {
                SpawnPrefab(achievement_title);
            }
        }
    }

    void SpawnPrefab(string achievement_title)
    {
        GameObject new_prefab = Instantiate(achievementIconPrefab, content_GO.transform);
        AchievementIconScript new_prefab_script = new_prefab.GetComponent<AchievementIconScript>();

        new_prefab_script.SetAcievement(achievement_title);
    }
}
