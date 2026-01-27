using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Color red = new Color(180, 45, 45);  // 180 45 45 (B42D2D)
    Color empty = Color.clear;

    [SerializeField]
    GameObject healthBarFilling;
    Sprite health_bar_filling_sprite;
    Texture2D health_bar_filling_texture;
    Rect health_bar_filling_texture_rect;

    Color[] health_bar_raw_pixels;
    Color[,] health_bar_pixels;
    int width;
    int height;

    [SerializeField]
    Sprite health_bar_filling_sprite_origin;
    Texture2D health_bar_filling_texture_origin;
    Rect health_bar_filling_texture_rect_origin;

    public float max_helth = 10f;
    public float current_helth = 10f;
    float current_health_percent = 1f;

    void Start()
    {
        current_helth = max_helth;
        current_health_percent = current_helth / max_helth;

        GetHealthBarFillingSpriteParameters();
        GetHealthBarFillingSpriteOriginParameters();

        UpdateHealthBar(1f);
    }

    public void UpdateHealthBar(int amount)
    {
        current_helth += amount;
        current_health_percent = current_helth / max_helth;

        SetHealthBarPercent(current_health_percent);
    }

    public void UpdateHealthBar(float amount)
    {
        //Debug.Log("Float amount HEALTH BAR");

        current_health_percent = amount;
        current_helth = max_helth / current_health_percent;

        SetHealthBarPercent(current_health_percent);
    }

    void SetHealthBarPercent(float percent)  // 0-1
    {
        SetHealthBarClear();

        int max_x = (int)(width * percent) + (width * percent * 10 % 10 > 0 ? 1 : 0);
        //int max_y = (int)(height * percent) + (height * percent * 10 % 10 > 0 ? 1 : 0);

        for (int x = 0; x < max_x; x++)
        {
            //for (int y = 0; y <= max_y; y++)
            for (int y = 0; y < height; y++)
            {
                if (health_bar_pixels[x, y] == Color.white)
                {
                    health_bar_filling_texture.SetPixel(x, y, red);
                }
            }
        }

        health_bar_filling_texture.Apply();

        health_bar_filling_sprite = Sprite.Create(
            health_bar_filling_texture,
            new Rect(0, 0, health_bar_filling_texture.width, health_bar_filling_texture.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    void SetHealthBarClear()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                health_bar_filling_texture.SetPixel(x, y, empty);
            }
        }

        health_bar_filling_texture.Apply();
    }

    void GetHealthBarFillingSpriteParameters()
    {
        health_bar_filling_sprite = healthBarFilling.GetComponent<Image>().sprite;
        health_bar_filling_texture = health_bar_filling_sprite.texture;
    }

    void GetHealthBarFillingSpriteOriginParameters()
    {
        health_bar_filling_texture_origin = health_bar_filling_sprite_origin.texture;

        width = (int)health_bar_filling_texture_origin.width;
        height = (int)health_bar_filling_texture_origin.height;

        health_bar_filling_texture_rect_origin = health_bar_filling_sprite_origin.textureRect;

        int x0 = (int)health_bar_filling_texture_rect_origin.x;
        int y0 = (int)health_bar_filling_texture_rect_origin.y;

        health_bar_raw_pixels = health_bar_filling_texture_origin.GetPixels(x0, y0, width, height);

        health_bar_pixels = new Color[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                health_bar_pixels[x, y] = health_bar_raw_pixels[y * width + x];
            }
        }
    }
}
