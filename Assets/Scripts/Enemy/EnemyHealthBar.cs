using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarFilling;

    void Start()
    {
        
    }

    public void ChangeHealthBarFillingScale(float scale)
    {
        Vector3 localScale = healthBarFilling.transform.localScale;
        localScale.x = scale;
        healthBarFilling.transform.localScale = localScale;
    }
}
