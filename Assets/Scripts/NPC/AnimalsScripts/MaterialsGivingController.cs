using System.Collections;
using UnityEngine;

public class MaterialsGivingController : InteractionController
{
    public GameObject readyIconGO;
    public SpriteRenderer readiIconSpriteRenderer;

    public ConsumableItemSO consumableItemSO;
    public int time_to_wait;

    private bool is_ready_to_give;

    private Coroutine coroutine;

    protected override void Start()
    {
        base.Start();

        readiIconSpriteRenderer.sprite = consumableItemSO.sprite;

        NotReadyToGive();
    }

    protected override void Interact()
    {
        if (is_ready_to_give)
        {
            backpackController.IncreaceItemByName(consumableItemSO.item_name);

            NotReadyToGive();
        }
    }

    private void ReadyToGive()
    {
        is_ready_to_give = true;

        readyIconGO.SetActive(true);
    }

    private void NotReadyToGive()
    {
        is_ready_to_give = false;

        readyIconGO.SetActive(false);

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = null;

        int delta = time_to_wait / 2;
        coroutine = StartCoroutine(WaitingCoroutine(time_to_wait + Random.Range(-delta, delta)));
    }

    private IEnumerator WaitingCoroutine(int time_left)
    {
        while (time_left > 0)
        {
            time_left--;
            yield return new WaitForSeconds(1f);
        }

        ReadyToGive();
    }
}
