using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    public float objectDuration = .5f;

    void Start()
    {
        Destroy(gameObject, objectDuration);
    }
}
