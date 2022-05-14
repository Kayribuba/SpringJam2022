using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<PlayerHealthScript>() != null && other.gameObject.CompareTag(Constants.PlayerTag))
        {
            FindObjectOfType<PlayerHealthScript>().GetDamage(FindObjectOfType<PlayerHealthScript>().MaxHealthValue);
        }
    }
}
