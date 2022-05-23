using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfo : MonoBehaviour
{
    public Vector3 BaseCenter { get; private set; }
    public Vector3 XLocal { get; private set; }
    public Vector3 YLocal { get; private set; }
    public Vector3 ZLocal { get; private set; }
    public float XLength { get; private set; }
    public float YLength { get; private set; }
    public float ZLength { get; private set; }

    void Awake()
    {
        BaseCenter = transform.position;

        XLength = Mathf.Abs(transform.localScale.x);
        YLength = Mathf.Abs(transform.localScale.y);
        ZLength = Mathf.Abs(transform.localScale.z);

        XLocal = transform.right;
        YLocal = transform.up;
        ZLocal = transform.forward;
    }
}
