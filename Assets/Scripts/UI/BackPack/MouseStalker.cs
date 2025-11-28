using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseStalker : MonoBehaviour
{
    public Sprite default_image_mouse;

    public Image image_mouse;
    // Update is called once per frame

    private void Start()
    {
        image_mouse.sprite = default_image_mouse;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ChangeImage(Sprite new_sprite)
    {
        //gameObject.GetComponent<Image>().sprite = new_sprite;
        image_mouse.sprite = new_sprite;
    }
    public void MakeDefault()
    {
        //gameObject.GetComponent<Image>().sprite = new_sprite;
        image_mouse.sprite = default_image_mouse;
    }
}
