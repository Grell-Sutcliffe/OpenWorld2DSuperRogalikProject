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
        progressBar.value = 0f;
        loadingText.text = "0%";

        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            progressBar.value = progress;
            loadingText.text = (progress * 100f).ToString("F0") + "%";

            if (progress < 0.75f)
            {
                yield return new WaitForSeconds(0.5f);
            }

            yield return null;
        }

        progressBar.value = 1f;
        loadingText.text = "100%";

        yield return new WaitForSeconds(0.5f);

        operation.allowSceneActivation = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
