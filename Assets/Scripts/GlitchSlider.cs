using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchSlider : MonoBehaviour
{
    [SerializeField] GameObject glitch;
    [SerializeField] float acc = 0.3f;
    [SerializeField] float speed = 5f;

    Vector3 x;
    float StartTime;
    float DTime;

    private void Start()
    {
        x = glitch.transform.position;
        StartTime = Time.time;
    }
    void Update()
    {
        DTime = Time.time - StartTime;
        speed += (acc/1000 * DTime);

        x.x -= speed * Time.deltaTime;
        glitch.transform.position = new Vector3(x.x, glitch.transform.position.y, glitch.transform.position.z);
    }
}
