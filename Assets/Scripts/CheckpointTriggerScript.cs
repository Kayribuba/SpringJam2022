using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTriggerScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            CheckPointManager CPM = FindObjectOfType<CheckPointManager>();
            if (CPM != null)
            {
                CPM.SetCheckpoint(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
