using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPowers : MonoBehaviour
{
    public LayerMask unityArrowLayer;
    public GameObject safe;
    [Range(1, 250)] public float PlatformSpeed = 50f;
    [Range(0.0001f, 10)] public float SmallestScale = 0.02f;

    //baseCollisionScript Collisions;
    //bool CollisionsAreOn;
    BoxCollider boxCollider;
    GameObject ArrowParent;
    Transform Border;
    Vector3 targetPosition;
    Vector3 targetScale;
    bool holdingArrows;
    Camera cam;
    RaycastHit hit;
    AudioSource aSource;

    void Start()
    {
        //Collisions = FindObjectOfType<baseCollisionScript>();
        //CollisionsAreOn = Collisions != null;
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
            switch(hit.transform.parent.tag)
            {
                case "UnityArrowBase":
                    TransformMethod(hit.transform.gameObject.tag);
                    break;

                case "UnityScaleBase":
                    ScaleMethod(hit.transform.gameObject.tag);
                    break;
            }
        }
    }

    private void ScaleMethod(string tagg)
    {
        targetScale = ArrowParent.transform.localScale;

        switch (tagg)
        {
            case "UnityScaleArrowX":

                targetScale.x += Input.GetAxis("Mouse X") * PlatformSpeed / 250;

                break;

            case "UnityScaleArrowY":

                targetScale.y += Input.GetAxis("Mouse Y") * PlatformSpeed / 250;

                break;

            case "UnityScaleArrowZ":

                    targetScale.z += Input.GetAxis("Mouse X") * PlatformSpeed / 250;

                break;
        }

        boxCollider = ArrowParent.GetComponent<BoxCollider>();
        bool collided = false;
        Collider[] collidedObjects2 = Physics.OverlapBox(ArrowParent.transform.position, targetScale / 2, ArrowParent.transform.rotation);
        foreach(Collider collidedThings in collidedObjects2)
        {
            if (collidedThings == boxCollider || collidedThings == null)
            { }
            else if (collidedThings.gameObject.CompareTag(Constants.UnityScaleBaseTag))
            {
                collided = true;
                break;
            }
        }

        if (!collided && targetScale.x > SmallestScale && targetScale.y > SmallestScale && targetScale.z > SmallestScale)
        {
            ArrowParent.transform.localScale = targetScale;
        }

        if (SmallestScale >= transform.localScale.x && SmallestScale >= transform.localScale.y && SmallestScale >= transform.localScale.z)
            Debug.Log("Smallest scale is bigger than or equal to objects scale.");
    }

    private void TransformMethod(string tagg)
    {
        targetPosition = ArrowParent.transform.position;

        switch (tagg)
        {
            case "UnityArrowX":

                if (transform.position.z <= ArrowParent.transform.position.z)//platformun z olarak arkasýndaysam
                {
                    targetPosition.x += Input.GetAxis("Mouse X") * PlatformSpeed / 250;
                }
                else//platformun z olarak önündeysem
                {
                    targetPosition.x -= Input.GetAxis("Mouse X") * PlatformSpeed / 250;
                }
                break;

            case "UnityArrowY":
                targetPosition.y += Input.GetAxis("Mouse Y") * PlatformSpeed / 250;//yukarý aþaðý
                break;

            case "UnityArrowZ":
                if (transform.position.x <= ArrowParent.transform.position.x)//platformun x olarak solundaysam
                    targetPosition.z -= Input.GetAxis("Mouse X") * PlatformSpeed / 250; //saða çekersem geri
                else//platformun x olarak saðýndaysam
                    targetPosition.z += Input.GetAxis("Mouse X") * PlatformSpeed / 250; //saða çekersem ileri
                break;
        }

        if (targetPosition.x > Border.position.x + Border.localScale.x / 2)
            targetPosition.x = Border.position.x + Border.localScale.x / 2;
        else if (targetPosition.x < Border.position.x - Border.localScale.x / 2)
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

        Collider[] collidedObjects = new Collider[1000];
        Collider[] collidedObjects2 = new Collider[0];

        boxCollider = ArrowParent.GetComponent<BoxCollider>();
        Vector3 halfExtents = new Vector3(boxCollider.size.x * ArrowParent.transform.localScale.x * ArrowParent.transform.parent.transform.localScale.x / 2, boxCollider.size.y * ArrowParent.transform.localScale.y * ArrowParent.transform.parent.transform.localScale.y / 2, boxCollider.size.z * ArrowParent.transform.localScale.z * ArrowParent.transform.parent.transform.localScale.z / 2);
        Physics.OverlapBoxNonAlloc(targetPosition, halfExtents, collidedObjects);

        for (int i = 0; i < collidedObjects.Length; i++)
        {
            if (collidedObjects[i] == null)
            {
                collidedObjects2 = new Collider[i];

                for (int j = 0; j < i; j++)
                {
                    collidedObjects2[j] = collidedObjects[j];
                }
                break;
            }
        }

        bool collided = false;
        foreach (Collider collisionsxd in collidedObjects2)
        {
            if (collisionsxd == boxCollider || collisionsxd == null)
            { }
            else if (collisionsxd.tag == "UnityArrowBase")
            {
                collided = true;
                break;
            }
        }

        if (!collided)
        {
            ArrowParent.transform.position = targetPosition;
        }
    }
}
