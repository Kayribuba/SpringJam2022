using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPowers : MonoBehaviour
{
    public LayerMask unityArrowLayer;
    public GameObject safe;
    [Range(1, 250)] public float DefaultPlatformSpeed = 100f;
    [Range(1, 500)] public float DefaultScaleSpeed = 300f;

    float currentPlatformSpeed;
    float currentScaleSpeed;

    [Header("1e-10 means no snapping")]
    [Range(1e-10f, 10)]public float DefaultGridStep = 0.5f;

    float currentGridStep;
    float deltaMouseProcessX;
    float deltaMouseProcessY;

    [Header("!!! Smallest scale shouldn't be less than 0.3f !!!")]
    public Vector3 DefaultSmallestScale = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 DefaultBiggestScale = new Vector3(5f, 5f, 5f);

    Vector3 currentSmallestScale;
    Vector3 currentBiggestScale;

    [Header("Write down the tags for objects to collide")]
    public string[] TRANSFORMObjectTagsToCollide;
    public string[] SCALEObjectTagsToCollide;

    public Material glowMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material redMaterial;

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
                deltaMouseProcessX = 0;
                deltaMouseProcessY = 0;

                if(ArrowParent.GetComponent<BaseOptionsOverride>() != null && !ArrowParent.GetComponent<BaseOptionsOverride>().UseDefaultOptions)
                {
                    currentPlatformSpeed = ArrowParent.GetComponent<BaseOptionsOverride>().PlatformSpeed;
                    currentScaleSpeed = ArrowParent.GetComponent<BaseOptionsOverride>().ScaleSpeed;
                    currentGridStep = ArrowParent.GetComponent<BaseOptionsOverride>().GridStep;
                    currentSmallestScale = ArrowParent.GetComponent<BaseOptionsOverride>().SmallestScale;
                    currentBiggestScale = ArrowParent.GetComponent<BaseOptionsOverride>().BiggestScale;
                }
                else
                {
                    currentPlatformSpeed = DefaultPlatformSpeed;
                    currentScaleSpeed = DefaultScaleSpeed;
                    currentGridStep = DefaultGridStep;
                    currentSmallestScale = DefaultSmallestScale;
                    currentBiggestScale = DefaultBiggestScale;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetMouseButtonUp(0))
        {
            holdingArrows = false;
        }

        if (holdingArrows)
        {
            if (hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial != glowMaterial)
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial = glowMaterial;
            }

            if (hit.collider.tag == "UnityScaleArrowZ" || hit.collider.tag == "UnityScaleArrowZMinus" || hit.collider.tag == "UnityScaleArrowX" || hit.collider.tag == "UnityScaleArrowXMinus" || hit.collider.tag == "UnityScaleArrowY" || hit.collider.tag == "UnityScaleArrowYMinus")
            {
                ScaleMethod(hit.collider.tag);
            }
            else if (hit.collider.tag == "UnityArrowZ" || hit.collider.tag == "UnityArrowX" || hit.collider.tag == "UnityArrowY")
            {
                TransformMethod(hit.collider.tag);
            }
        }
        else if(hit.collider == null)
        { }
        else if (hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial == glowMaterial)
        {
            if (hit.collider.tag == "UnityArrowX" || hit.collider.tag == "UnityScaleArrowX" || hit.collider.tag == "UnityScaleArrowXMinus")
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial = redMaterial;
            }
            else if (hit.collider.tag == "UnityArrowY" || hit.collider.tag == "UnityScaleArrowY" || hit.collider.tag == "UnityScaleArrowYMinus")
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial = greenMaterial;
            }
            else if (hit.collider.tag == "UnityArrowZ" || hit.collider.tag == "UnityScaleArrowZ" || hit.collider.tag == "UnityScaleArrowZMinus")
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial = blueMaterial;
            }
        }
    }

    private void ScaleMethod(string tagg)
    {
        targetScale = ArrowParent.transform.lossyScale;

        float deltaMouseX = Input.GetAxis("Mouse X") * currentScaleSpeed / 250;
        float deltaMouseY = Input.GetAxis("Mouse Y") * currentScaleSpeed / 250;

        deltaMouseX -= deltaMouseX % currentGridStep;
        deltaMouseY -= deltaMouseY % currentGridStep;

        if(deltaMouseX == 0)
        {
            deltaMouseProcessX += Input.GetAxis("Mouse X") * currentScaleSpeed / 250;
        }
        if(Mathf.Abs(deltaMouseProcessX) >= currentGridStep)
        {
            deltaMouseX = deltaMouseProcessX - (deltaMouseProcessX % currentGridStep);
            deltaMouseProcessX = 0;
        }

        if (deltaMouseY == 0)
        {
            deltaMouseProcessY += Input.GetAxis("Mouse Y") * currentScaleSpeed / 250;
        }
        if (Mathf.Abs(deltaMouseProcessY) >= currentGridStep)
        {
            deltaMouseY = deltaMouseProcessY - (deltaMouseProcessY % currentGridStep);
            deltaMouseProcessY = 0;
        }

        switch (tagg)
        {
            case "UnityScaleArrowX":
                if (transform.position.z <= ArrowParent.transform.position.z)
                    targetScale.x += deltaMouseX;
                else
                    targetScale.x -= deltaMouseX;
                break;

            case "UnityScaleArrowY":
                targetScale.y += deltaMouseY;
                break;

            case "UnityScaleArrowZ":
                if (transform.position.x <= ArrowParent.transform.position.x)
                    targetScale.z -= deltaMouseX;
                else
                    targetScale.z += deltaMouseX;
                break;

            case "UnityScaleArrowXMinus":
                if (transform.position.z <= ArrowParent.transform.position.z)
                    targetScale.x -= deltaMouseX;
                else
                    targetScale.x += deltaMouseX;
                break;

            case "UnityScaleArrowYMinus":
                targetScale.y -= deltaMouseY;
                break;

            case "UnityScaleArrowZMinus":
                if (transform.position.x <= ArrowParent.transform.position.x)
                    targetScale.z += deltaMouseX;
                else
                    targetScale.z -= deltaMouseX;
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

        if (!collided && targetScale.x > currentSmallestScale.x && targetScale.y > currentSmallestScale.y && targetScale.z > currentSmallestScale.z && targetScale.x < currentBiggestScale.x && targetScale.y < currentBiggestScale.y && targetScale.z < currentBiggestScale.z)
        {
            Debug.Log("in scale");

            Vector3 arrowArranger = new Vector3(1, 1, 1);

            if(ArrowParent.transform.parent != null)
            {
                Vector3 middleman = new Vector3();

                middleman.x = targetScale.x / ArrowParent.transform.parent.lossyScale.x;
                middleman.y = targetScale.y / ArrowParent.transform.parent.lossyScale.y;
                middleman.z = targetScale.z / ArrowParent.transform.parent.lossyScale.z;

                arrowArranger.x = ArrowParent.transform.parent.lossyScale.x;
                arrowArranger.y = ArrowParent.transform.parent.lossyScale.y;
                arrowArranger.z = ArrowParent.transform.parent.lossyScale.z;

                ArrowParent.transform.localScale = middleman;
            }
            else
            {
                ArrowParent.transform.localScale = targetScale;
            }

            Vector3 ArrowPosScale = ArrowParent.transform.position;

            if (ArrowParent.transform.Find("X+") != null)
            {
                ArrowParent.transform.Find("X+").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.z / arrowArranger.z, 1 / ArrowParent.transform.localScale.x / arrowArranger.x, 0.3f / ArrowParent.transform.localScale.y / arrowArranger.y);

                ArrowPosScale.x += ArrowParent.transform.localScale.x / 2 * arrowArranger.x + ArrowParent.transform.Find("X+").transform.localScale.y * ArrowParent.transform.localScale.x / 2 * arrowArranger.y;
                ArrowParent.transform.Find("X+").transform.position = ArrowPosScale;

                ArrowPosScale = ArrowParent.transform.position;
            }
            if (ArrowParent.transform.Find("X-") != null)
            {
                ArrowParent.transform.Find("X-").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.z / arrowArranger.z, 1 / ArrowParent.transform.localScale.x / arrowArranger.x, 0.3f / ArrowParent.transform.localScale.y / arrowArranger.y);

                ArrowPosScale.x -= ArrowParent.transform.localScale.x / 2 * arrowArranger.x + ArrowParent.transform.Find("X-").transform.localScale.y * ArrowParent.transform.localScale.x / 2 * arrowArranger.y;
                ArrowParent.transform.Find("X-").transform.position = ArrowPosScale;

                ArrowPosScale = ArrowParent.transform.position;
            }

            if (ArrowParent.transform.Find("Y+") != null)
            {
                ArrowParent.transform.Find("Y+").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x / arrowArranger.x, 1 / ArrowParent.transform.localScale.y / arrowArranger.y, 0.3f / ArrowParent.transform.localScale.z / arrowArranger.z);

                ArrowPosScale.y += ArrowParent.transform.localScale.y / 2 * arrowArranger.y + ArrowParent.transform.Find("Y+").transform.localScale.y * ArrowParent.transform.localScale.y / 2 * arrowArranger.y;
                ArrowParent.transform.Find("Y+").transform.position = ArrowPosScale;

                ArrowPosScale = ArrowParent.transform.position;
            }
            if (ArrowParent.transform.Find("Y-") != null)
            {
                ArrowParent.transform.Find("Y-").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x / arrowArranger.x, 1 / ArrowParent.transform.localScale.y / arrowArranger.y, 0.3f / ArrowParent.transform.localScale.z / arrowArranger.z);

                ArrowPosScale.y -= ArrowParent.transform.localScale.y / 2 * arrowArranger.y + ArrowParent.transform.Find("Y-").transform.localScale.y * ArrowParent.transform.localScale.y / 2 * arrowArranger.y;
                ArrowParent.transform.Find("Y-").transform.position = ArrowPosScale;

                ArrowPosScale = ArrowParent.transform.position;
            }

            if (ArrowParent.transform.Find("Z+") != null)
            {
                ArrowParent.transform.Find("Z+").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x / arrowArranger.x, 1 / ArrowParent.transform.localScale.z / arrowArranger.z, 0.3f / ArrowParent.transform.localScale.y / arrowArranger.y);

                ArrowPosScale.z += ArrowParent.transform.localScale.z / 2 * arrowArranger.z + ArrowParent.transform.Find("Z+").transform.localScale.y * ArrowParent.transform.localScale.z / 2 * arrowArranger.z;
                ArrowParent.transform.Find("Z+").transform.position = ArrowPosScale;

                ArrowPosScale = ArrowParent.transform.position;
            }
            if (ArrowParent.transform.Find("Z-") != null)
            {
                ArrowParent.transform.Find("Z-").transform.localScale = new Vector3(0.3f / ArrowParent.transform.localScale.x / arrowArranger.x, 1 / ArrowParent.transform.localScale.z / arrowArranger.z, 0.3f / ArrowParent.transform.localScale.y / arrowArranger.y);

                ArrowPosScale.z -= ArrowParent.transform.localScale.z / 2 * arrowArranger.z + ArrowParent.transform.Find("Z-").transform.localScale.y * ArrowParent.transform.localScale.z / 2 * arrowArranger.z;
                ArrowParent.transform.Find("Z-").transform.position = ArrowPosScale;

                ArrowPosScale = ArrowParent.transform.position;
            }
        }

        if (currentSmallestScale.x >= transform.localScale.x)
            Debug.Log("Smallest x scale is bigger than or equal to objects x scale.");
        if (currentSmallestScale.y >= transform.localScale.y)
            Debug.Log("Smallest y scale is bigger than or equal to objects y scale.");
        if (currentSmallestScale.z >= transform.localScale.z)
            Debug.Log("Smallest z scale is bigger than or equal to objects z scale.");
        if (currentBiggestScale.x <= transform.localScale.x)
            Debug.Log("Biggest x scale is smaller than or equal to objects x scale.");
        if (currentBiggestScale.y <= transform.localScale.y)
            Debug.Log("Biggest y scale is smaller than or equal to objects y scale.");
        if (currentBiggestScale.z <= transform.localScale.z)
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
                    targetPosition.x += Input.GetAxis("Mouse X") * currentPlatformSpeed / 250;
                }
                else//platformun z olarak önündeysem
                {
                    targetPosition.x -= Input.GetAxis("Mouse X") * currentPlatformSpeed / 250;
                }
                break;

            case "UnityArrowY":
                targetPosition.y += Input.GetAxis("Mouse Y") * currentPlatformSpeed / 250;//yukarý aþaðý
                break;

            case "UnityArrowZ":
                if (transform.position.x <= ArrowParent.transform.position.x)//platformun x olarak solundaysam
                    targetPosition.z -= Input.GetAxis("Mouse X") * currentPlatformSpeed / 250; //saða çekersem geri
                else//platformun x olarak saðýndaysam
                    targetPosition.z += Input.GetAxis("Mouse X") * currentPlatformSpeed / 250; //saða çekersem ileri
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

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, targetScale);
    }
}
