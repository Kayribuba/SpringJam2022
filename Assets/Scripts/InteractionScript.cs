using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType { Destroy, Enable, Disable}
public class InteractionScript : MonoBehaviour
{
    public void Interact(InteractionType type, GameObject GO)
    {
        switch(type)
        {
            case InteractionType.Destroy:
                Destroy(GO);
                break;
            case InteractionType.Disable:
                GO.SetActive(false);
                break;
            case InteractionType.Enable:
                GO.SetActive(true);
                break;
        }
    }
}
