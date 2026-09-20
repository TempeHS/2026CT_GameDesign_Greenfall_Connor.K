using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    public AudioClip bgMusic;
    [SerializeField] private Slider musicSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SetVolume(musicSlider.value);
    }
    void Start()
    {
        if (bgMusic != null) 
        {
            PlayBGM(false, bgMusic);
        }
        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
    }

    public static void SetVolume(float volume)
    {
        instance.audioSource.volume = volume;
    }
    public static void PlayBGM(bool resetSong, AudioClip audioclip = null)
    {
        if (audioclip != null)
        {
            instance.audioSource.clip = audioclip;
        }
        if (instance.audioSource.clip != null) 
        {
            if (resetSong) {
                instance.audioSource.Stop();
            }
            instance.audioSource.Play();
        }
    }
    public static void PauseBGM()
    {
        instance.audioSource.Pause();
    }
}
