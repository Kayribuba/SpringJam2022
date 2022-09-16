using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorModeScript : MonoBehaviour
{
    [SerializeField] CursorLockMode cursorLockMode;

    void Awake()
    {
        if(cursorLockMode == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(cursorLockMode == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
