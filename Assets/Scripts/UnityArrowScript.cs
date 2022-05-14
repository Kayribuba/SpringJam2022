using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityArrowScript : MonoBehaviour
{
    public LayerMask ignoreLayers;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer != ignoreLayers)
        {
           // FindObjectOfType<PlayerMovementScript>().
        }
    }
}
