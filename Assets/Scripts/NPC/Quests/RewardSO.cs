using UnityEngine;

[CreateAssetMenu(fileName = "Reward", menuName = "Item/Reward")]
public class RewardSO : ScriptableObject
{
    public ItemSO itemSO;
    public int amount;
}
