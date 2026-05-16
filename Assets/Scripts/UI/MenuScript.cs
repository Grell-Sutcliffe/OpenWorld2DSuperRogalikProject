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

    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        Time.timeScale = 1f;

        MusicManager.Instance.SetSlider(volumeSlider);
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

        yield return new WaitForSecondsRealtime(0.3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float visualProgress = 0f;

        while (visualProgress < 0.5f)
        {
            visualProgress += Time.unscaledDeltaTime * 0.35f;

            if (visualProgress > 0.5f)
                visualProgress = 0.5f;

            progressBar.value = visualProgress;
            loadingText.text = (visualProgress * 100f).ToString("F0") + "%";

            yield return null;
        }

        progressBar.value = 0.5f;
        loadingText.text = "50%";

        yield return new WaitForSecondsRealtime(0.5f);

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        while (visualProgress < 1f)
        {
            visualProgress += Time.unscaledDeltaTime * 0.5f;

            if (visualProgress > 1f)
                visualProgress = 1f;

            progressBar.value = visualProgress;
            loadingText.text = (visualProgress * 100f).ToString("F0") + "%";

            yield return null;
        }

        progressBar.value = 1f;
        loadingText.text = "100%";

        yield return new WaitForSecondsRealtime(0.5f);

        operation.allowSceneActivation = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
