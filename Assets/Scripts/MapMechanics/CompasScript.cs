using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CompasScript : MonoBehaviour
{
    public ConsumableItemSO consumableItemSO;

    public Image arrowImage;

    public Transform playerTransform;

    Coroutine coroutine;

    public void Activate()
    {
        gameObject.SetActive(true);

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(RotateArrowCoroutine());
    }

    public void Deactivate()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = null;

        gameObject.SetActive(false);
    }

    IEnumerator RotateArrowCoroutine()
    {
        while (true) {
            MapFragment nearest = MapFragmentRegistry.GetNearest(playerTransform.position);

            if (nearest == null)
            {
                coroutine = null;

                gameObject.SetActive(false);

                yield break;
            }

            RotateArrow(nearest.transform);
            yield return null;
        }
    }

    void RotateArrow(Transform target)
    {
        Vector2 direction = target.position - playerTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
