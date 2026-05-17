using TMPro;
using UnityEngine;
using UnityEngine.Windows;

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

    float setFontSize(float d)
    {
        float result = 0; 
        switch (d)
        {
            case < 15:
                result = Mathf.Clamp(d / 15 * 2 + 1, 1, 2);
                break;
            case < 30:
                result = Mathf.Clamp(d / 15 * 2, 2, 3);
                break;
            case < 60:
                result = Mathf.Clamp(d / 15 * 2, 3, 4);
                break;
            case < 90:
                //float t = (d - 60f) / 140f;
                //result = Mathf.Lerp(6f, 10f, t);
                result = Mathf.Clamp(d / 15 * 2, 4, 5);
                break;
            case >= 90:
                //float t = (d - 60f) / 140f;
                //result = Mathf.Lerp(6f, 10f, t);
                result = Mathf.Clamp(d / 15 * 2, 5, 6);
                break;
        }

        return result;
    }

    public void InitPhysical(Damage dmg)
    {
        text.text = Mathf.RoundToInt(dmg.physical_dmg).ToString();
        text.color = dmg.isPhysicalCrit ? Color.red : Color.white;

        startPos = transform.position;
        text.fontSize = setFontSize(dmg.physical_dmg); 

        endPos = startPos + new Vector3(Random.Range(-0.5f, 0f), Random.Range(0.5f, 1f), 0) * 1.0f;
        t = 0f;
    }

    public void InitElemental(Damage dmg)
    {
        text.text = Mathf.RoundToInt(dmg.elemental_dmg).ToString();
        //text.color = dmg.isElementalCrit ? Color.red : Color.white;
        switch (dmg.element_type)
        {
            case ElementType.Cryo:
                text.color = Color.cyan;
                break;
            case ElementType.Pyro:
                text.color = Color.yellow;
                break;
            case ElementType.Energo:
                text.color = Color.magenta;
                break;
            case ElementType.Floro:
                text.color = Color.green;
                break;
        }

        startPos = transform.position;

        text.fontSize = setFontSize(dmg.elemental_dmg);

        endPos = startPos + new Vector3(Random.Range(0f, 0.5f), Random.Range(0.5f, 1f), 0) * 1.0f;
        t = 0f;
    }

    void Update()
    {
        t += Time.deltaTime / lifeTime;
        float k = moveCurve.Evaluate(t);
        //Debug.Log(moveCurve.Evaluate(t));
        transform.position = Vector3.Lerp(startPos, endPos, k);

        if (t >= 1f) Destroy(gameObject);
    }
}
