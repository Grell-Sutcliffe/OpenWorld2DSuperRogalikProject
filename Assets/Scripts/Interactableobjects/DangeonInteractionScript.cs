using UnityEngine;
using UnityEngine.SceneManagement;

public class DangeonInteractionScript : InteractionController
{
    public void EnterDangeon()
    {
        SceneManager.LoadScene(2);
    }

    protected override void Interact()
    {
        mainController.InteractDangeon();
    }
}
