using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOptionsOverride : MonoBehaviour
{
    public bool UseDefaultOptions;

    [Range(1, 250)] public float PlatformSpeed = 100f;
    [Range(1, 500)] public float ScaleSpeed = 300f;

    [Header("1e-10 means no snapping")]
    [Range(1e-10f, 10)] public float GridStep = 0.5f;

    [Header("!!! Smallest scale shouldn't be less than 0.3f !!!")]
    public Vector3 SmallestScale = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 BiggestScale = new Vector3(5f, 5f, 5f);
}
