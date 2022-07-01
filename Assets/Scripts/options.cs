using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider valuebar;
    public Slider musicValuebar;
    float valuee;

    AudioSource MusicAudioSource;

    public void SetVolume()
    {
        valuee = valuebar.value;
        audioMixer.SetFloat("volume", valuee);
    }
    public void SetMusicValue()
    {
        MusicAudioSource = FindObjectOfType<AudioSource>();
        if (MusicAudioSource != null)
        {
            valuee = musicValuebar.value;
            MusicAudioSource.volume = valuee;
        }
    }
    public void SetMusicValuebarValue()
    {
        MusicAudioSource = FindObjectOfType<AudioSource>();
        if (MusicAudioSource != null)
        {
            musicValuebar.value = MusicAudioSource.volume;
        }
    }
}
