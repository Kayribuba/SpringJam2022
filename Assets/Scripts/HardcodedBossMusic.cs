using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardcodedBossMusic : MonoBehaviour
{
    [SerializeField] PauseMenu PauseMenu;
    [SerializeField] AudioSource ASource;

    bool gameIsPaused;

    void Update()
    {
        if (PauseMenu.GameIsPaused && !gameIsPaused)
        {
            gameIsPaused = true;
            ASource.Pause();
        }
        else if(!PauseMenu.GameIsPaused && gameIsPaused)
        {
            gameIsPaused = false;
            ASource.UnPause();
        }
    }
}
