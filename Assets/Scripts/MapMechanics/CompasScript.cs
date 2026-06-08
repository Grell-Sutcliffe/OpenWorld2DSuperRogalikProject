using UnityEngine;
using UnityEngine.UI;

public class CompasScript : MonoBehaviour
{
    public Image arrowImage;

    public ConsumableItemSO consumableItemSO_1;
    public ConsumableItemSO consumableItemSO_2;
    public ConsumableItemSO consumableItemSO_3;

    private void Update()
    {
        if (consumableItemSO_1 != null)
        {
            RotateArrow(consumableItemSO_1.item_name);
        }
    }

    void RotateArrow(string item_name)
    {

    }
}
