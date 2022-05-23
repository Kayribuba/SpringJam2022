using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseCollisionScript : MonoBehaviour
{
    [SerializeField] TESTScript testScript;

    BaseInfo[] infoOfEveryBaseActiveInScene;

    void Start()
    {
        infoOfEveryBaseActiveInScene = FindObjectsOfType<BaseInfo>();
    }

    public bool IsPointInAnyBase(Vector3 point)
    {
        if (testScript != null)
            testScript.CreatePoint(point);

        foreach(BaseInfo info in infoOfEveryBaseActiveInScene)
        {
            Vector3 currentVector = point - info.BaseCenter;

            float projectionX = Mathf.Abs(Vector3.Dot(currentVector, info.XLocal));
            float projectionY = Mathf.Abs(Vector3.Dot(currentVector, info.YLocal));
            float projectionZ = Mathf.Abs(Vector3.Dot(currentVector, info.ZLocal));

            if (2 * projectionX <= info.XLength && 2 * projectionY <= info.YLength && 2 * projectionZ <= info.ZLength)
                return true;
        }

        return false;
    }
}
