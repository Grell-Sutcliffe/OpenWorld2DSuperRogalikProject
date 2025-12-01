using UnityEngine;
using UnityEngine.SceneManagement;

public class DoggyInteractionScript : InteractionController
{
    protected override void Interact()
    {
        mainController.InteractDoggy();
    }
}
