using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] float lifeTime = 0.8f;
    [SerializeField] AnimationCurve moveCurve = AnimationCurve.Linear(0, 0, 1, 1);
    float t;
    Vector3 startPos;
    Vector3 endPos;
    

    TextMeshPro text;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        Destroy(gameObject, lifeTime);
    }

    public void Init(Damage dmg)
    {
        text.text = Mathf.RoundToInt(dmg.damage).ToString();
        text.color = dmg.isCrit ? Color.red : Color.white;

        startPos = transform.position;
        text.fontSize = dmg.isCrit ? 4f : 3f; // ← ВОТ ЭТО

        endPos = startPos + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.5f, 1f), 0) * 1.0f; // высота
        t = 0f;

    }

    void Update()
    {
        t += Time.deltaTime / lifeTime;
        float k = moveCurve.Evaluate(t);
        Debug.Log(moveCurve.Evaluate(t));
        transform.position = Vector3.Lerp(startPos, endPos, k);

        if (t >= 1f) Destroy(gameObject);
    }
}
