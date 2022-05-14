using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    Animator animator;
    float targetTime = float.MaxValue;
    float waitBeforeStarting = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetTime = Time.time + waitBeforeStarting;
    }
    void Update()
    {
        if (targetTime <= Time.time)
            animator.SetTrigger("start");
    }
}
