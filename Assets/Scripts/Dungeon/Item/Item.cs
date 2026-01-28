using UnityEngine;
using System;
using System.Threading;
using UnityEngine.U2D;

public abstract class Item
{
    public Sprite sprite;
    public string item_name;
    public string description;
    //public Guid uniqueID;
    //public float radius = 0.5f;

    public int amount;

    public ItemType item_type = ItemType.Everything;

    public int id;

    //public bool playerInRange = false;

    /*
    protected virtual void Start()
    {
        //uniqueID = Guid.NewGuid();
        var collider = gameObject.GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<CircleCollider2D>();
        }
        collider.isTrigger = true;
        //collider.radius = radius; 
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //playerInRange = false;
        }
    }

    public void OnPickup(GameObject player)
    {

    }
    */
}

public enum ItemType
{
    Everything = 0,
    Weapon = 1,
    Food = 2,
    Drink = 3,
    Materials = 4,
    Quest = 5,
}
