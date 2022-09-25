using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string musicPref = "musicPref";
    private int firstPlayInt;
    public Slider musicSlider;
    private float musicFloat;
    public AudioSource audioSource;
   
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            musicFloat = .75f;
            musicSlider.value = musicFloat;
            PlayerPrefs.SetFloat(musicPref, musicFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(musicPref);
            musicSlider.value = musicFloat;
        }
    }

    public void saveSound()
    {
        PlayerPrefs.SetFloat(musicPref, musicSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            saveSound();
        }
       
    }

    public void UpdateSound()
    {
        audioSource.volume = musicSlider.value;
    }
}
