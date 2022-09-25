using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneVolume : MonoBehaviour
{

    public AudioSource audioSource;
    private static readonly string musicPref = "musicPref";
    private float musicFloat;

    void Awake()
    {
        AudioSettings();
    }

    private void AudioSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(musicPref);

        audioSource.volume = musicFloat;
    }
}
