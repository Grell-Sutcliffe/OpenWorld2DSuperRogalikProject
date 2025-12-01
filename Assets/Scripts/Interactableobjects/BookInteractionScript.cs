using UnityEngine;
using UnityEngine.SceneManagement;

public class BookInteractionScript : InteractionController
{
    protected override void Interact()
    {
        mainController.InteractBook();
        TakeBook();
    }

    public void TakeBook()
    {
        Destroy(gameObject);
    }
}
