using System.Collections;
using UnityEngine;

public class GardenController : InteractionController
{
    public SpriteRenderer spriteRenderer;

    public ConsumableItemSO consumableItemSO;

    private int time_to_grow;

    Coroutine coroutine;

    protected override void Awake()
    {
        base.Awake();

        //spriteRenderer = GetComponent<SpriteRenderer>();

        SetImageEmpty();
    }

    protected override void Start()
    {
        base.Start();

        StartNewGrowthCoroutine();
    }

    private void SetTimeToGrow()
    {
        int delta = consumableItemSO.time_to_grow / 2;
        time_to_grow = consumableItemSO.time_to_grow + Random.Range(-delta, delta);
    }

    private void SetImageEmpty()
    {
        is_interactable = false;

        if (backpackController == null) backpackController = GameObject.Find("BackpackController").GetComponent<BackPackController>();

        spriteRenderer.sprite = backpackController.empty_sprite;

        OffInteraction();
    }
    

    private void SetImage()
    {
        is_interactable = true;

        spriteRenderer.sprite = consumableItemSO.sprite;

        OnInteraction();
    }

    protected override void Interact()
    {
        if (time_to_grow <= 0)
        {
            //Debug.LogError("Item took");

            backpackController.IncreaceItemByName(consumableItemSO.item_name);

            SetImageEmpty();

            StartNewGrowthCoroutine();
        }
    }

    private void StartNewGrowthCoroutine()
    {
        SetTimeToGrow();

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
