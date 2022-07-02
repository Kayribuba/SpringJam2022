using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OutroManager : MonoBehaviour//I hardcoded sorry:(
{
    [SerializeField] VideoPlayer vp;
    [SerializeField] GameObject subCan;

    private void Update()
    {
        if (vp.frame > 70)
            subCan.SetActive(true);
        if (vp.frame > (float)vp.frameCount - 5)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
