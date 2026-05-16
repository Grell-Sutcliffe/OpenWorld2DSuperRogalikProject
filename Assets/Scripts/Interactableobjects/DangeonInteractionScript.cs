using UnityEngine;
using UnityEngine.SceneManagement;

public class DangeonInteractionScript : InteractionController
{
    public void EnterDangeon(GameObject prefDung, GameObject GodFather)
    {

        /*
        mainController.TurnOnKeyboard();
        SceneManager.LoadScene(1);
        mainController.playerScript.transform.position = Vector3.zero;
        */
        GodFather.SetActive(false);
        GameObject arrowGO = Instantiate(
            prefDung,       
            MainController.Instance.playerScript.transform.position,
            Quaternion.Euler(0, 0, 0)
        );

    }

    protected override void Interact()
    {
        mainController.InteractDangeon(this);
    }
}
