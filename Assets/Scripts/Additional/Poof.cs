using UnityEngine;

public class Poof : MonoBehaviour
{
    public GameObject go;
    public Transform whoParent;
    public BoxCollider2D walkZone;

    private void Start()
    {
        MusicManager.Instance.PlayByIndex(0);
    }

    public void Spawn()
    {
        var GO = Instantiate(
                    go,
                    new Vector2(transform.position.x,
                                transform.position.y),
                                Quaternion.identity,
                                whoParent);
        var enemy = GO.GetComponent<EnemyAbstract>();
        if (enemy != null) enemy.walkZone = walkZone;
    }
    


    public void KillMyself()
    {
        Destroy(gameObject);
    }
    
}
