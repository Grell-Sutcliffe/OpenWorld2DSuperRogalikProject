using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("BasicStatus")]
    [SerializeField] public string enemyName;
    [SerializeField] public float maxHealth;
    [SerializeField] public float armour;
    [SerializeField] public float damage;
    [SerializeField] public float moveSpeed;
    [SerializeField] public EnemyType enemyType;

    [Header("RangeConfig")]
    [SerializeField] public float detectionRange;
    [SerializeField] public float attackRange;
    [SerializeField] public float keepDistance;

    [Header("AttackConfig")]
    [SerializeField] public float attackCooldown;
    //[SerializeField] public float attackWindup;

}
