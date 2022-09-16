using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShortcuts : MonoBehaviour
{
    public Transform playerpos;
    public Transform pos1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangePos();
        }
    }

    void ChangePos()
    {
        playerpos.transform.position = pos1.position;
        Debug.Log("f1 pressed" + pos1.position);
    }
}
