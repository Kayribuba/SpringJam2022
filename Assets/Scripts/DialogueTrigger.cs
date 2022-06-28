using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueObject[] dialoguesToEnqueue;
    [SerializeField] bool destroyAfterEnqueue = true;

    DialogueMangerScript DMS;

    void Start()
    {
        DialogueMangerScript DMS = FindObjectOfType<DialogueMangerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.PlayerTag))
        {
            DMS = FindObjectOfType<DialogueMangerScript>();

            if (DMS != null)
            {
                foreach (DialogueObject DO in dialoguesToEnqueue)
                    DMS.dialogueQueue.Enqueue(DO);
            }
            else
                Debug.Log("Dialogue manager cannot be found...");

            if (destroyAfterEnqueue)
                Destroy(gameObject);
        }
    }
}
