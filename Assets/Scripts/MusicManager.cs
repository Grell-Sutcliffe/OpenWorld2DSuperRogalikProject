using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource source;

    [Header("Tracks")]
    [SerializeField] private List<AudioClip> tracksPhantom = new();
    [SerializeField] private List<AudioClip> tracks = new();

    [Header("Volume UI")]
    [SerializeField] private Slider volumeSlider;

    [Header("Volume")]
    [SerializeField] private float volume = 1f;

    private const string VolumeKey = "volume";

    private int currentMusicIndex = 0;
    private Coroutine musicCoroutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolume();
    }

    private void Start()
    {
        SetupSlider();
    }

    private void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        if (musicSource != null)
            musicSource.volume = volume;

        if (source != null)
            source.volume = volume;
    }

    private void SetupSlider()
    {
        if (volumeSlider == null)
            return;

        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.wholeNumbers = false;

        volumeSlider.onValueChanged.RemoveListener(SetVolume);

        volumeSlider.SetValueWithoutNotify(volume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        volume = Mathf.Clamp01(value);

        ApplyVolume();

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void OnSliderChanged(float value)
    {
        SetVolume(value);
    }

    public void PlayPhantomMusicByIndex(int index)
    {
        if (index < 0 || index >= tracksPhantom.Count)
        {
            Debug.LogWarning($"Music phantom index {index} out of range");
            return;
        }

        currentMusicIndex = index;

        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);

        musicCoroutine = StartCoroutine(MusicPlaylistRoutine());
    }

    public void SetSlider(Slider slider)
    {
        volumeSlider = slider;
        SetupSlider();
    }

    private IEnumerator MusicPlaylistRoutine()
    {
        while (true)
        {
            AudioClip clip = tracksPhantom[currentMusicIndex];

            musicSource.clip = clip;
            musicSource.loop = false;
            musicSource.Play();

            yield return new WaitForSecondsRealtime(clip.length);

            currentMusicIndex++;

            if (currentMusicIndex >= tracksPhantom.Count)
                currentMusicIndex = 0;
        }
    }

    public void PlayByIndex(int index, bool isLoop = false)
    {
        if (index < 0 || index >= tracks.Count)
        {
            Debug.LogWarning($"Music index {index} out of range");
            return;
        }

        source.PlayOneShot(tracks[index]);
    }

    public void Stop()
    {
        source.Stop();
    }
}
