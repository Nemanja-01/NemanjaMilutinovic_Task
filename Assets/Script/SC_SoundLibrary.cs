using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SoundLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;
    // Start is called before the first frame update
    private void Awake()
    {
        InitializeDictionary();
    }

    public void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            soundDictionary[soundEffectGroup.name] = soundEffectGroup.clip;
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if (audioClips.Count > 0)
            {
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }
        }
        return null;

    }
    [System.Serializable]
    public struct SoundEffectGroup
    {
        public string name;
        public List<AudioClip> clip;
    }
}
