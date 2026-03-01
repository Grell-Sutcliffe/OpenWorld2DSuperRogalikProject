using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeliveryPanelScript : MonoBehaviour
{
    MainController mainController;
    BackPackController backpackController;

    public GameObject content_GO;

    public GameObject item_delivery_icon_prefab;

    RectTransform content_rect_transform;

    List<CollectableItem> deliverable_items;

    private void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();

        content_rect_transform = content_GO.GetComponent<RectTransform>();
    }

    public void DeliverItemsButton()
    {
        bool was_deliver_success = backpackController.DeliverItems(deliverable_items);

        if (was_deliver_success)
        {
            // успешно отдали
            gameObject.SetActive(false);
        }
        else
        {
            // не отдали, нет материалов
            mainController.OpenErrorPanel(ErrorType.NotEnoughMaterials);
        }
    }

    public void OpenPanel(List<CollectableItem> list)
    {
        Debug.Log("DeliveryPanel  :  Open DeliveryPanel");

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
        content_rect_transform.sizeDelta = new Vector2(content_rect_transform.sizeDelta.x, 0);
    }

    void SpawnIconPrefab(CollectableItem collectableItem)
    {
        GameObject new_prefab = Instantiate(item_delivery_icon_prefab, content_GO.transform);

        Icon_ItemDelivery_Script temp_script = new_prefab.GetComponent<Icon_ItemDelivery_Script>();

        temp_script.SetItem(collectableItem, backpackController.GetItemByName(collectableItem.item_name));
    }
}
