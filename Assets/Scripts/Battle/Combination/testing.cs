using System.Collections;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] GameObject pref;
    void Start()
    {
        StartCoroutine(SpawnBoomWithDelay(3f));
       
    }

    private IEnumerator SpawnBoomWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject boomGO = Instantiate(
            pref,
            transform.position,
            Quaternion.identity
        );

        var boom = boomGO.GetComponent<BOOM>();
        boom.Init(new Damage(23, 0, ElementType.Physical), 3);
    }
}
