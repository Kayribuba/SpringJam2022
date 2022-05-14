using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PlayerTag))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
