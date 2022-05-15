using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider valuebar;
    float valuee;

    public void SetVolume()
    {
        valuee = valuebar.value;
        audioMixer.SetFloat("volume", valuee);
    }
}
