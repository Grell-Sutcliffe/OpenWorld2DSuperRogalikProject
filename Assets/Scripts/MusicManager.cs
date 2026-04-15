using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource musicSource;
    int currentMusicIndex = 0;
    [SerializeField] private List<AudioClip> tracksPhantom = new();
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> tracks = new();
    private Coroutine musicCoroutine;
    /*
     * 0 - poof (0) 
     * 1 - playerdmg
     * 2-4 - dmg enemies
     * 5 - click
     * 6 - sand (enemy die)
     * 
     * 
     */

    [SerializeField] private float volume = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        volume = PlayerPrefs.GetFloat("volume", 1f);

        musicSource.volume = volume;
        source.volume = volume;
    }

    public void OnSliderChanged(float value)
    {
        MusicManager.Instance.SetVolume(value);
    }

    public void SetVolume(float value)
    {
        volume = value;

        musicSource.volume = volume;
        source.volume = volume;

        PlayerPrefs.SetFloat("volume", volume);
    }

    public void PlayPhantomMusicByIndex(int index)
    {
        if (index < 0 || index >= tracks.Count)
        {
            Debug.LogWarning($"Music phantom index {index} out of range");
            return;
        }
        currentMusicIndex = index;

        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);

        musicCoroutine = StartCoroutine(MusicPlaylistRoutine());
        
    }

    private IEnumerator MusicPlaylistRoutine()
    {
        while (true)
        {
            AudioClip clip = tracksPhantom[currentMusicIndex];

            musicSource.clip = clip;
            musicSource.loop = false;
            musicSource.Play();

            yield return new WaitForSeconds(clip.length);

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

        //if (source.clip == tracks[index] && source.isPlaying)
        //    return;
        Debug.LogWarning("PPOOOOOF");
        //source.clip = tracks[index];
        //source.loop = isLoop;
        source.PlayOneShot(tracks[index]);
    }

    public void Stop()
    {
        source.Stop();
    }
}
