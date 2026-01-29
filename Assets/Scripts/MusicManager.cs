using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> tracks = new();
    /*
     * 0 - poof (0) 
     * 1 - playerdmg
     * 2-4 - dmg enemies
     * 5 - click
     * 6 - sand (enemy die)
     * 
     * 
     */
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        source = GetComponent<AudioSource>();
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
