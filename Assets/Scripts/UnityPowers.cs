using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPowers : MonoBehaviour
{
    public LayerMask unityArrowLayer;
    public GameObject safe;
    [Range(1, 250)] public float PlatformSpeed = 50f;

    GameObject ArrowParent;
    Transform Border;
    Vector3 targetPosition;
    bool holdingArrows;
    Camera cam;
    RaycastHit hit;
    AudioSource aSource;

    void Start()
    {
        cam = GetComponent<Camera>();
        ArrowParent = safe;
        aSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit TempHit;

        if (Physics.Raycast(ray, out TempHit, 100, unityArrowLayer))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(0))
            {
                aSource.PlayOneShot(aSource.clip);
                hit = TempHit;
                holdingArrows = true;
                ArrowParent = hit.transform.parent.gameObject;
                Border = ArrowParent.transform.parent;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetMouseButtonUp(0))
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

            if (targetPosition.x > Border.position.x + Border.localScale.x / 2)
                targetPosition.x = Border.position.x + Border.localScale.x / 2;
            else if(targetPosition.x < Border.position.x - Border.localScale.x / 2)
                targetPosition.x = Border.position.x - Border.localScale.x / 2;

            if (targetPosition.y > Border.position.y + Border.localScale.y / 2)
                targetPosition.y = Border.position.y + Border.localScale.y / 2;
            else if (targetPosition.y < Border.position.y - Border.localScale.y / 2)
                targetPosition.y = Border.position.y - Border.localScale.y / 2;

            if (targetPosition.z > Border.position.z + Border.localScale.z / 2)
                targetPosition.z = Border.position.z + Border.localScale.z / 2;
            else if (targetPosition.z < Border.position.z - Border.localScale.z / 2)
                targetPosition.z = Border.position.z - Border.localScale.z / 2;


            Mathf.Clamp(targetPosition.x, Border.position.x - Border.localScale.x / 2, Border.position.x + Border.localScale.x / 2);
            Mathf.Clamp(targetPosition.y, Border.position.y - Border.localScale.y / 2, Border.position.y + Border.localScale.y / 2);
            Mathf.Clamp(targetPosition.z, Border.position.z - Border.localScale.z / 2, Border.position.z + Border.localScale.z / 2);

            ArrowParent.transform.position = targetPosition;
        }
    }
}
