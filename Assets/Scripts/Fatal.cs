using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fatal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<PlayerHealthScript>() != null && other.gameObject.CompareTag(Constants.PlayerTag))
        {
            FindObjectOfType<PlayerHealthScript>().GetDamage(FindObjectOfType<PlayerHealthScript>().MaxHealthValue);
        }
        else if (other.gameObject.CompareTag(Constants.PlayerTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
