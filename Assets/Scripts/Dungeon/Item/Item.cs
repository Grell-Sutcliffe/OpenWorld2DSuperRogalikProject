using UnityEngine;
using System;

public abstract class Item : MonoBehaviour
{
    /*
    оружка + кнопка "надеть"
    еда - нкапливаемое + кнопку "использовать"
    материалы (камни, минералы, ресурсы) - накапливаемое
    квестовые штуки
    артеакты
    звезды (? наследуемый)
    ++ использование
    
    */

    [Header("Базовые свойства предмета")]
    public string itemName;
    public Sprite icon;
    public Guid uniqueID;
    public float radius = 0.5f;
    // add UI 

    [Header("Флаги состояния")]
    public bool playerInRange = false;

    protected virtual void Start()
    {
        uniqueID = Guid.NewGuid();

        var collider = gameObject.GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<CircleCollider2D>();
        }
        collider.isTrigger = true;
        collider.radius = radius; 
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public abstract void OnPickup(GameObject player);
}
