using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTScript : MonoBehaviour
{
    public GameObject magenta;
    public void CreatePoint(Vector3 point)
    {
        Destroy(Instantiate(magenta, point, Quaternion.identity), 0.1f);
    }
}
