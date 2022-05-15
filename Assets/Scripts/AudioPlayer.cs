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
                if (oldLevel != 0)
                {
                    source.clip = clips[0];
                    oldLevel = 0;
                    source.Play();
                }
                break;
            case 1:
                if (oldLevel != 1)
                {
                    source.clip = clips[1];
                    oldLevel = 1;
                    source.Play();
                }
                break;
            case 2:
                if (oldLevel != 2)
                {
                    source.clip = clips[2];
                    oldLevel = 2;
                    source.Play();
                }
                break;
            case 3:
                if (oldLevel != 3)
                {
                    source.clip = clips[3];
                    oldLevel = 3;
                    source.Play();
                }
                break;
            case 4:
                if (oldLevel != 4)
                {
                    source.clip = clips[4];
                    oldLevel = 4;
                    source.Play();
                }
                break;
            case 5:
                if (oldLevel != 5)
                {
                    source.clip = clips[5];
                    oldLevel = 5;
                    source.PlayOneShot(clips[5]);
                }
                break;
        }
    }
}
