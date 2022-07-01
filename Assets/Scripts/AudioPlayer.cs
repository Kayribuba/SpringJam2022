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
                    source.clip = clips[2];
                    oldLevel = 1;
                    source.Play();
                }
                break;
            case 7:
                if (oldLevel != 7)
                {
                    source.clip = clips[5];
                    oldLevel = 7;
                    source.Play();
                }
                break;
        }
    }
}
