using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F;

    protected bool isPlayerInRange = false;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
