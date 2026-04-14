using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CanvasScript Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
