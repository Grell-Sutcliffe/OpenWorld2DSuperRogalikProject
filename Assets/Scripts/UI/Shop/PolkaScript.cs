using UnityEngine;

public class PolkaScript : MonoBehaviour
{
    public SpecieBuyScript specieBuyScript;

    public void OnClick()
    {
        specieBuyScript.AddOne();
    }
}
