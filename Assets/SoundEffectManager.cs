using System.Collections.Specialized;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;

    [SerializeField] private SliderJoint2D sfxSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public static void Play(string soundName)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
