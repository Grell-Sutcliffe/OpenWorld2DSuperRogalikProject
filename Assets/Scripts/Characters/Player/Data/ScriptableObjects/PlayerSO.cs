using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerMovementData MovementData { get; private set; }
}
