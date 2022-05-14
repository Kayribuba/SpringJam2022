using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMscripto : MonoBehaviour
{
    float targetTime = float.MaxValue;
    int sceneBuildIndex = 0;
    bool timeIsSet = false;

    void Update()
    {
        if (targetTime < Time.time)
            LoadScene(sceneBuildIndex);
    }
    public void LoadSceneSecondsAfter(int buildIndex, float time)
    {
        if(!timeIsSet)
        {
            Debug.Log("aman aman nereye geldik");
            targetTime = time + Time.time;
            sceneBuildIndex = buildIndex;

            timeIsSet = true;
        }
    }
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
