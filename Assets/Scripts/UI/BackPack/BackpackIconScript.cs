using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackpackIconScript : MonoBehaviour
{
    BackPackController backpackController;

    public Image item_image_TMP;
    public TextMeshProUGUI item_counter_TMP;

    int id;
    Sprite sprite;
    int count;

    Item item; // new
    void Start()
    {
        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    public void SetNewId(int new_id)
    {
        id = new_id;

        if (backpackController == null) backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();

        sprite = backpackController.dict_id_to_item[id].sprite;
        count = backpackController.dict_id_to_item[id].count;

        item_image_TMP.sprite = sprite;
        item_counter_TMP.text = count.ToString();


        item = backpackController.dict_id_to_item[id]; // new
    }

    public void ItemOnClick()
    {
        backpackController.UpdateShowerPanel(id);


        backpackController.itemStalker = item; // new
        backpackController.isStalking = true; // new  сейчас он сбросится только при отпускании на кнопке, что неверно
        backpackController.imageStalker.sprite = item.sprite; // new

    }
}
