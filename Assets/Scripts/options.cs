using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource MusicAudioSource;
    public Slider valuebar;
    public Slider musicValuebar;
    float valuee;

    private void Start()
    {
        musicValuebar.value = MusicAudioSource.volume;
    }
    public void SetVolume()
    {
        valuee = valuebar.value;
        audioMixer.SetFloat("volume", valuee);
    }
    public void SetMusicValue()
    {
        valuee = musicValuebar.value;
        MusicAudioSource.volume = valuee;
    }
}
