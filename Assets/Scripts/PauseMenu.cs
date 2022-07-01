using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused { get; private set; }
    [SerializeField]Canvas PauseMenuCanvas;
    [SerializeField] GameObject player;

    AudioPlayer AP;
    DialogueMangerScript DMS;

    void Start()
    {
        AP = FindObjectOfType<AudioPlayer>();
        DMS = FindObjectOfType<DialogueMangerScript>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else if (!GameIsPaused)
                Pause();
        }
    }

    public void Pause()
    {
        if(!GameIsPaused)
        {
            PauseMenuCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameIsPaused = true;
            Time.timeScale = 0;
            player.GetComponent<PlayerMovementScript>().enabled = false;
            player.GetComponent<Respawn>().enabled = false;
            player.GetComponentInChildren<MouseLook>().enabled = false;
            player.GetComponentInChildren<UnityPowers>().enabled = false;

            if(AP != null)
            {
                if(AP.GetComponent<AudioSource>().isPlaying)
                {
                    AP.GetComponent<AudioSource>().Pause();
                }
            }

            if(DMS != null)
            {
                if(DMS.GetComponent<AudioSource>().isPlaying)
                {
                    DMS.enabled = false;
                    DMS.GetComponent<AudioSource>().Pause();
                }
            }
        }
    }
    public void Resume()
    {
        if (GameIsPaused)
        {
            PauseMenuCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameIsPaused = false;
            Time.timeScale = 1;
            player.GetComponent<PlayerMovementScript>().enabled = true;
            player.GetComponent<Respawn>().enabled = true;
            player.GetComponentInChildren<MouseLook>().enabled = true;
            player.GetComponentInChildren<UnityPowers>().enabled = true;

            if (AP != null)
            {
                    AP.GetComponent<AudioSource>().UnPause();
            }

            if (DMS != null)
            {
                    DMS.GetComponent<AudioSource>().UnPause();
                    DMS.enabled = true;
            }
        }
    }
    public void MainMenu()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Options()
    {

    }
}
