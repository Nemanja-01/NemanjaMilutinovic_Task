using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_SoundManager : MonoBehaviour
{
    private static SC_SoundManager Instance;
    private static AudioSource _soundSource;
    private static AudioSource randomPitchAudio;
    private static AudioSource npcAudioSource;
    private static SC_SoundLibrary soundLibrary;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AudioSource[] audioSources = GetComponents<AudioSource>();
            _soundSource = audioSources[0];
            randomPitchAudio = audioSources[1];
            npcAudioSource = audioSources[2];
            soundLibrary = GetComponent<SC_SoundLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string name, bool randomPitch = false)
    {
        AudioClip clip = soundLibrary.GetRandomClip(name);
        if (clip != null)
        {
            if (randomPitch)
            {
                randomPitchAudio.pitch = Random.Range(1f, 1.5f);
                randomPitchAudio.PlayOneShot(clip);
            }
            else
            {
                _soundSource.PlayOneShot(clip);
            }
        }
    }
    public static void PlayVoice(AudioClip audioClip, float pitch = 1f)
    {
        npcAudioSource.pitch = pitch;
        npcAudioSource.PlayOneShot(audioClip);
    }
    private void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        _soundSource.volume = volume;
        randomPitchAudio.volume = volume;
        npcAudioSource.volume = volume;
    }
    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }

}
