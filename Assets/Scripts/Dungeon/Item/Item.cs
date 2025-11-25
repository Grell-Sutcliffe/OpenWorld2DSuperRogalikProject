using UnityEngine;
using System;
using System.Threading;
using UnityEngine.U2D;
using UnityEngine.UI;


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
    public string name;
    public Sprite sprite;
    public string description;
    public Guid uniqueID;
    public float radius = 0.5f;
    public int count;


    public Image image;
    public int id;
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

    public void OnPickup(GameObject player)
    {

    }



    public Item()
    {
        name = "";
        description = "";
        count = 0;
    }

    public Item(string name_)
    {
        name = name_;
        description = "";
        count = 0;
    }

    public Item(string name_, string description_, Sprite sprite_)
    {
        name = name_;
        description = description_;
        count = 0;
        sprite = sprite_;
    }
}
