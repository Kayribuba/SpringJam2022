using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class skipAfterVideo : MonoBehaviour
{
    VideoPlayer Vp;

    private void Start()
    {
        Vp = GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        if(Vp.frame >= (float)Vp.frameCount - 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
