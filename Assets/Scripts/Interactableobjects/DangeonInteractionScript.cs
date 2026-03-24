using UnityEngine;
using UnityEngine.SceneManagement;

public class DangeonInteractionScript : InteractionController
{
    public void EnterDangeon()
    {
        mainController.TurnOnKeyboard();
        SceneManager.LoadScene(1);
        mainController.playerScript.transform.position = Vector3.zero;
    }

    protected override void Interact()
    {
        mainController.InteractDangeon();
    }
}
