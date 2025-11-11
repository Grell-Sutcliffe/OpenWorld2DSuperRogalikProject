using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackController : MonoBehaviour
{
    public Image book_img;

    public class Item
    {
        public string name;
        public string description;
        public int count;
    }

    public List<int> player_items_id;

    void Start()
    {
        
    }
}
