using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] BoxCollider2D spawnZone;
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    [SerializeField] private List<int> nums = new List<int>();

    [SerializeField] private GameObject poof;
    Animator anim;
    bool wasTriggered = false;
    int enemyCount = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        for (int i = 0; i < nums.Count; i++)
        {
            enemyCount += nums[i];
        }
    }

    public void Died()
    {
        enemyCount -= 1;
        if (enemyCount == 0) {Debug.LogWarning("ALL DEAD");
            anim.SetTrigger("reward");
        }
    }

    public void GiveReward()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wasTriggered) return;
        if (collision.CompareTag("Player"))
        {
            wasTriggered = true;
            Bounds b = spawnZone.bounds;

            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = 0; j < nums[i]; j++)
                {
                    StartCoroutine(WaitAndDo(Random.Range(0f, 0.5f), b, i));
                    //Spawn(b, i);
                }

            }
           
        }
    }

    private void Spawn(Bounds b, int i)
    {
        var GO = Instantiate(
                            poof,
                            new Vector2(UnityEngine.Random.Range(b.min.x, b.max.x),
                                        UnityEngine.Random.Range(b.min.y, b.max.y)),
                                        Quaternion.identity);

        var poofGo = GO.GetComponent<Poof>();
        poofGo.walkZone = spawnZone;
        poofGo.whoParent = transform;
        poofGo.go = objects[i];
    }

    IEnumerator WaitAndDo(float t, Bounds b, int i)
    {
        yield return new WaitForSeconds(t); // ждать 2 секунды
        Spawn(b,i);
    }

}
