using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioPlayer : MonoBehaviour
{
    [System.Serializable]
    struct LevelSong
    {
        public int[] levelIndexesToPlay;
        public AudioClip songToPlay;
        public bool loopSong;
    }

    public static AudioPlayer instance;
    [SerializeField] LevelSong[] levelSongs;
    AudioSource audioSource;

    int oldIndex = -1;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            CheckSong(0);
        }

    }
    void OnLevelWasLoaded(int level)
    {
        CheckSong(level);
    }

    void CheckSong(int level)
    {
        foreach (LevelSong LS in levelSongs)
        {
            foreach (int indx in LS.levelIndexesToPlay)
            {
                if (indx == level)
                {
                    if (level != oldIndex)
                    {
                        if (audioSource.clip != null && LS.songToPlay != audioSource.clip)
                        {
                            audioSource.Stop();
                            audioSource.clip = LS.songToPlay;
                            audioSource.loop = LS.loopSong;
                            audioSource.Play();
                            oldIndex = level;
                            break;
                        }
                    }
                }
            }

        }
    }
}