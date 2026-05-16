using UnityEngine;

[CreateAssetMenu(fileName = "UsableItem", menuName = "Item/UsableItem")]
public class UsableItemSO : ConsumableItemSO
{
    public UseType useType;
    public int use_percent_from_0_to_100;
    public int time_duration_seconds;
    public int time_for_close;
}
