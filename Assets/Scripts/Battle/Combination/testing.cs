using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] GameObject pref;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject boomGO = Instantiate(
            pref,         // тут нада от оружия брать
            transform.position,
            Quaternion.Euler(0, 0, 0)
        );
        var boom = boomGO.GetComponent<BOOM>();
        boom.Init(new Damage(23, 0, ElementType.Physical), 3);
    }

    
}
