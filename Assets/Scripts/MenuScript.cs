using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
