using System.Collections;
using UnityEngine;

public class GardenController : InteractionController
{
    public SpriteRenderer spriteRenderer;

    public ConsumableItemSO consumableItemSO;

    private int time_to_grow;

    Coroutine coroutine;

    private void Awake()
    {
        base.Awake();

        //spriteRenderer = GetComponent<SpriteRenderer>();

        SetImageEmpty();
    }

    private void Start()
    {
        base.Start();

        SetTimeToGrow();

        StartNewGrowthCoroutine();
    }

    private void SetTimeToGrow()
    {
        time_to_grow = consumableItemSO.time_to_grow + Random.Range(-30, 31);
    }

    private void SetImageEmpty()
    {
        spriteRenderer.sprite = backpackController.empty_sprite;
    }
    

    private void SetImage()
    {
        spriteRenderer.sprite = consumableItemSO.sprite;
    }

    protected override void Interact()
    {
        if (time_to_grow <= 0)
        {
            backpackController.IncreaceItemByName(consumableItemSO.item_name);

            SetImageEmpty();

            SetTimeToGrow();

            StartNewGrowthCoroutine();
        }
    }

    private void StartNewGrowthCoroutine()
    {
        SetImageEmpty();

        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = null;

        coroutine = StartCoroutine(GrowthCoroutine());
    }

    private IEnumerator GrowthCoroutine()
    {
        while (time_to_grow > 0)
        {
            time_to_grow--;
            yield return new WaitForSeconds(1f);
        }

        SetImage();
    }
}
