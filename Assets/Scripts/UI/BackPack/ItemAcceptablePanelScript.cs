using System.Collections.Generic;
using UnityEngine;

public class ItemAcceptablePanelScript : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;

    public GameObject content_GO;

    public GameObject item_delivery_icon_prefab;

    // RectTransform content_rect_transform;

    List<CollectableItem> deliverable_items;

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        // content_rect_transform = content_GO.GetComponent<RectTransform>();
    }

    public void AcceptItemsButton()
    {
        foreach (CollectableItem collectableItem in deliverable_items)
        {
            backpackController.IncreaceItemByName(collectableItem.item_name, collectableItem.amount);
        }

        EventBus.Raise(new ItemAcceptedEvent(deliverable_items));

        gameObject.SetActive(false);
    }

    public void OpenPanel(List<CollectableItem> list)
    {
        //Debug.Log("DeliveryPanel  :  Open DeliveryPanel");

        deliverable_items = list;
        gameObject.SetActive(true);

        UpdatePanel();
    }

    public void UpdatePanel()
    {
        ClearPanel();

        foreach (CollectableItem collectableItem in deliverable_items)
        {
            SpawnIconPrefab(collectableItem);
        }
    }

    void ClearPanel()
    {
        foreach (Transform child in content_GO.transform)
        {
            Destroy(child.gameObject);
        }
        // content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void SpawnIconPrefab(CollectableItem collectableItem)
    {
        GameObject new_prefab = Instantiate(item_delivery_icon_prefab, content_GO.transform);

        Icon_ItemAccept_Script temp_script = new_prefab.GetComponent<Icon_ItemAccept_Script>();

        temp_script.SetItem(collectableItem, backpackController.GetItemByName(collectableItem.item_name));
    }
}
