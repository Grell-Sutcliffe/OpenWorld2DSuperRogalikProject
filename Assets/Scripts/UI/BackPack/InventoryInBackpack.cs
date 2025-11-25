using UnityEngine;
using UnityEngine.UI;

public class InventoryInBackpack : MonoBehaviour
{
    public RectTransform area;      // область = размеру объекта
    BackPackController backpackController;

    public Item item;
    public Image image;
    private void Awake()
    {
        if (area == null)
            area = transform as RectTransform;
    }
    void Start()
    {
        backpackController = GameObject.Find("BackpackPanel").GetComponent<BackPackController>();
    }

    private void Update()
    {
        // Отпустили ЛКМ?
        if (Input.GetMouseButtonUp(0))
        {
            // Проверяем, находится ли курсор внутри области
            if (RectTransformUtility.RectangleContainsScreenPoint(
                    area,
                    Input.mousePosition,
                    null)) 
            {
                OnMouseUpInsideArea();
            }
        }
    }

    private void OnMouseUpInsideArea()
    {
        Debug.Log("Отпустили мышь над областью объекта!");
        // Здесь твоя логика
        if (backpackController.isStalking)
        {
            item = backpackController.itemStalker;
            image.sprite = item.sprite;

            backpackController.isStalking = false;
            backpackController.itemStalker = null;
            backpackController.imageStalker = backpackController.pustoe;
        }

    }

    public void Poo()
    {
        Debug.Log("ya tyt");
    }
}
