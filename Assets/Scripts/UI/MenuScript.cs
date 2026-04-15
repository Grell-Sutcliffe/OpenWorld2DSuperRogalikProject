using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject loadingGO;
    public Slider progressBar;
    public TextMeshProUGUI loadingText;

    string sceneToLoad = "GameScene";

    private void Start()
    {
        MusicManager.Instance.PlayPhantomMusicByIndex(0);
    }

    public void PlayButton()
    {
        loadingGO.SetActive(true);

        StartCoroutine(LoadAsync());
        //SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            progressBar.value = progress;
            loadingText.text = (progress * 100).ToString("F0") + "%";

            if (operation.progress >= 0.4f)
            {
                // Можно добавить задержку или анимацию
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
