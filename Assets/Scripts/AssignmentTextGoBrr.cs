using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AssignmentTextGoBrr : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float growSpeed = 1f;
    bool changed;
    bool changed2;
    float targetTime = float.MaxValue;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (text.fontSize <= 125)
            text.fontSize += growSpeed;
        else if(!changed)
        {
            changed = true;
            targetTime = Time.time + .5f;
        }

        if(Time.time >= targetTime && !changed2)
        {
            changed2 = true;
            text.text = "GO";
            targetTime = Time.time + .1f;
        }
        if(Time.time >= targetTime)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
