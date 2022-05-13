using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPowers : MonoBehaviour
{
    public LayerMask unityArrowLayer;
    public GameObject safe;
    [Range(1, 250)] public float PlatformSpeed = 50f;

    GameObject ArrowParent;
    Vector3 targetPosition;
    bool holdingArrows;
    Camera cam;
    RaycastHit hit;

    void Start()
    {
        cam = GetComponent<Camera>();
        ArrowParent = safe;
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit TempHit;

        if (Physics.Raycast(ray, out TempHit, 100, unityArrowLayer))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                hit = TempHit;
                holdingArrows = true;
                ArrowParent = hit.transform.parent.gameObject;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            holdingArrows = false;
        }

        if (holdingArrows)
        {
            targetPosition = ArrowParent.transform.position;

            switch (hit.transform.gameObject.tag)
            {
                case "UnityArrowX":
                    if (transform.position.z <= ArrowParent.transform.position.z)
                        targetPosition.x += Input.GetAxis("Mouse X") * PlatformSpeed / 250;
                    else
                        targetPosition.x -= Input.GetAxis("Mouse X") * PlatformSpeed / 250;

                    break;

                case "UnityArrowY":
                    targetPosition.y += Input.GetAxis("Mouse Y") * PlatformSpeed / 250;
                    break;

                case "UnityArrowZ":
                    if (transform.position.x <= ArrowParent.transform.position.x)
                        targetPosition.z -= Input.GetAxis("Mouse X") * PlatformSpeed / 250;
                    else
                        targetPosition.z += Input.GetAxis("Mouse X") * PlatformSpeed / 250;
                    break;
            }

                ArrowParent.transform.position = targetPosition;
        }
    }
}
