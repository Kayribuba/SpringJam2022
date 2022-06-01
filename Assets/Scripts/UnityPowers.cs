using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPowers : MonoBehaviour
{
    public LayerMask unityArrowLayer;
    public GameObject safe;
    [Range(1, 250)] public float PlatformSpeed = 100f;
    [Range(1, 500)] public float ScaleSpeed = 300f;

    [Header("!!! Smallest scale shouldn't be less than 0.3f for cosmetic reasons !!!")]
    public Vector3 SmallestScale = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 BiggestScale = new Vector3(5f, 5f, 5f);

    [Header("Write down the tags for objects to collide")]
    public string[] TRANSFORMObjectTagsToCollide;
    public string[] SCALEObjectTagsToCollide;

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
                if (transform.position.z <= ArrowParent.transform.position.z)
                    targetScale.x += Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                else
                    targetScale.x -= Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                break;

            case "UnityScaleArrowY":
                targetScale.y += Input.GetAxis("Mouse Y") * ScaleSpeed / 250;
                break;

            case "UnityScaleArrowZ":
                if (transform.position.x <= ArrowParent.transform.position.x)
                    targetScale.z -= Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                else
                    targetScale.z += Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                break;

            case "UnityScaleArrowXMinus":
                if (transform.position.z <= ArrowParent.transform.position.z)
                    targetScale.x -= Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                else
                    targetScale.x += Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                break;

            case "UnityScaleArrowYMinus":
                targetScale.y -= Input.GetAxis("Mouse Y") * ScaleSpeed / 250;
                break;

            case "UnityScaleArrowZMinus":
                if (transform.position.x <= ArrowParent.transform.position.x)
                    targetScale.z += Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                else
                    targetScale.z -= Input.GetAxis("Mouse X") * ScaleSpeed / 250;
                break;
        }

        boxCollider = ArrowParent.GetComponent<BoxCollider>();
        bool collided = false;
        Collider[] collidedObjects2 = Physics.OverlapBox(ArrowParent.transform.position, targetScale / 2, ArrowParent.transform.rotation);
        foreach(Collider collidedThings in collidedObjects2)
        {
            foreach(string tags in SCALEObjectTagsToCollide)
            {
                if (collidedThings == boxCollider || collidedThings == null)
                { }
                else if (collidedThings.gameObject.CompareTag(tags))
                {
                    collided = true;
                    break;
                }
            }

            if (collided)
                break;
        }

        if (!collided && targetScale.x > SmallestScale.x && targetScale.y > SmallestScale.y && targetScale.z > SmallestScale.z && targetScale.x < BiggestScale.x && targetScale.y < BiggestScale.y && targetScale.z < BiggestScale.z)
        {
            ArrowParent.transform.localScale = targetScale;

            ArrowParent.transform.Find("X+").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.z, 1 / ArrowParent.transform.localScale.x, 0.3f / ArrowParent.transform.localScale.y);
            ArrowParent.transform.Find("X-").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.z, 1 / ArrowParent.transform.localScale.x, 0.3f / ArrowParent.transform.localScale.y);
            ArrowParent.transform.Find("Y+").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x, 1 / ArrowParent.transform.localScale.y, 0.3f / ArrowParent.transform.localScale.z);
            ArrowParent.transform.Find("Y-").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x, 1 / ArrowParent.transform.localScale.y, 0.3f / ArrowParent.transform.localScale.z);
            ArrowParent.transform.Find("Z+").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x, 1 / ArrowParent.transform.localScale.z, 0.3f / ArrowParent.transform.localScale.y);
            ArrowParent.transform.Find("Z-").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x, 1 / ArrowParent.transform.localScale.z, 0.3f / ArrowParent.transform.localScale.y);

            Vector3 ArrowPosScale = ArrowParent.transform.position;

            ArrowPosScale.x += ArrowParent.transform.localScale.x / 2 + ArrowParent.transform.Find("X+").transform.localScale.y * ArrowParent.transform.localScale.x / 2;
            ArrowParent.transform.Find("X+").transform.position = ArrowPosScale;
            ArrowPosScale.x -= ArrowParent.transform.localScale.x + ArrowParent.transform.Find("X-").transform.localScale.y * ArrowParent.transform.localScale.x;
            ArrowParent.transform.Find("X-").transform.position = ArrowPosScale;
            ArrowPosScale = ArrowParent.transform.position;

            ArrowPosScale.y += ArrowParent.transform.localScale.y / 2 + ArrowParent.transform.Find("Y+").transform.localScale.y * ArrowParent.transform.localScale.y / 2;
            ArrowParent.transform.Find("Y+").transform.position = ArrowPosScale;
            ArrowPosScale.y -= ArrowParent.transform.localScale.y + ArrowParent.transform.Find("Y-").transform.localScale.y * ArrowParent.transform.localScale.y;
            ArrowParent.transform.Find("Y-").transform.position = ArrowPosScale;
            ArrowPosScale = ArrowParent.transform.position;

            ArrowPosScale.z += ArrowParent.transform.localScale.z / 2 + ArrowParent.transform.Find("Z+").transform.localScale.y * ArrowParent.transform.localScale.z / 2;
            ArrowParent.transform.Find("Z+").transform.position = ArrowPosScale;
            ArrowPosScale.z -= ArrowParent.transform.localScale.z + ArrowParent.transform.Find("Z-").transform.localScale.y * ArrowParent.transform.localScale.z;
            ArrowParent.transform.Find("Z-").transform.position = ArrowPosScale;
        }

        if (SmallestScale.x >= transform.localScale.x)
            Debug.Log("Smallest x scale is bigger than or equal to objects x scale.");
        if (SmallestScale.y >= transform.localScale.y)
            Debug.Log("Smallest y scale is bigger than or equal to objects y scale.");
        if (SmallestScale.z >= transform.localScale.z)
            Debug.Log("Smallest z scale is bigger than or equal to objects z scale.");
        if (BiggestScale.x <= transform.localScale.x)
            Debug.Log("Biggest x scale is smaller than or equal to objects x scale.");
        if (BiggestScale.y <= transform.localScale.y)
            Debug.Log("Biggest y scale is smaller than or equal to objects y scale.");
        if (BiggestScale.z <= transform.localScale.z)
            Debug.Log("Biggest z scale is smaller than or equal to objects z scale.");
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

        boxCollider = ArrowParent.GetComponent<BoxCollider>();
        Vector3 halfExtents = new Vector3(boxCollider.size.x * ArrowParent.transform.localScale.x * ArrowParent.transform.parent.transform.localScale.x / 2, boxCollider.size.y * ArrowParent.transform.localScale.y * ArrowParent.transform.parent.transform.localScale.y / 2, boxCollider.size.z * ArrowParent.transform.localScale.z * ArrowParent.transform.parent.transform.localScale.z / 2);
        Collider[] collidedObjects = Physics.OverlapBox(targetPosition, halfExtents);
        bool collided = false;

        foreach (Collider collisionsxd in collidedObjects)
        {
            foreach (string tags in TRANSFORMObjectTagsToCollide)
            {
                if (collisionsxd == boxCollider || collisionsxd == null)
                { }
                else if (collisionsxd.CompareTag(tags))
                {
                    collided = true;
                    break;
                }
            }

            if (collided)
                break;
        }

        if (!collided)
        {
            ArrowParent.transform.position = targetPosition;
        }
    }
}
