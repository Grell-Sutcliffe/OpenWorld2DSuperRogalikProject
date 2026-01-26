using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected Collider2D hitCollider;

    [SerializeField] protected TypeOfEffect effectType;
    [SerializeField] protected float powerOfEffect;

    [SerializeField] protected bool isDestroyAfterWork;
    [SerializeField] protected bool OnCoolDown;
    [SerializeField] protected float CoolDown;

    [SerializeField] protected TypeOfEnemy enemyType;
}


public enum TypeOfEffect
{
    DMG = 1,
    SPD = 2
}

public enum TypeOfEnemy
{
    PLAYER = 1,
    ENEMY = 2
}