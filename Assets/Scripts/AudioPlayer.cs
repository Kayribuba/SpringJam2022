using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;
    public AudioClip[] clips;
    public AudioSource source;

    int oldLevel = -1;

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case 0:
                if (oldLevel != level)
                {
                    source.clip = clips[0];
                    oldLevel = level;
                    source.Play();
                }
                break;
            case 1:
                if (oldLevel != level)
                {
                    source.clip = clips[6];
                    oldLevel = level;
                    source.Play();
                }
                break;
            case 2:
                if (oldLevel != level)
                {
                    source.clip = clips[2];
                    oldLevel = level;
                    source.Play();
                }
                break;
            case 9:
                if (oldLevel != level)
                {
                    source.clip = clips[6];
                    oldLevel = level;
                    source.PlayOneShot(source.clip);
                }
                break;
            case 10:
                if (oldLevel != level)
                {
                    source.clip = clips[5];
                    oldLevel = level;
                    source.PlayOneShot(source.clip);
                }
                break;
        }
    }
}
