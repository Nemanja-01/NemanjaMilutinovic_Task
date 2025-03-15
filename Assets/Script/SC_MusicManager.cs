using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_MusicManager : MonoBehaviour
{
    private static SC_MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip bcgMusic;
    [SerializeField] private Slider musicSlider;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(bcgMusic!=null)
        {
            PlayBcgMusic(false, bcgMusic);
        }

        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }
    public static void PlayBcgMusic(bool restartSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            Instance.audioSource.clip = audioClip;
        }
        if (Instance.audioSource != null)
        {
            if (restartSong)
            {
                Instance.audioSource.Stop();
            }
            Instance.audioSource.Play();
        }
    }
    public static void PauseBackgroundMusic()
    {
        Instance.audioSource.Pause();
    }
}
