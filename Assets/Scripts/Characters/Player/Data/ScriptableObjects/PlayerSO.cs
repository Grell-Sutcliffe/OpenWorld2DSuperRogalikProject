using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerMovementData MovementData { get; private set; }

    [Header("BasicStatus")]
    [SerializeField] public string playerName;
    [SerializeField] public float maxHealth;
    [SerializeField] public float armour;
    [SerializeField] public float damage;
    [SerializeField] public float moveSpeed;

    [Header("RangeConfig")]
    [SerializeField] public float attackRange;

    [Header("AttackConfig")]
    [SerializeField] public float attackCooldown;
}
