using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCodedLookAter : MonoBehaviour // hüüüüüüüüüüüüüüüü
{
    [SerializeField] float lookSpeed = 0.1f;
    [SerializeField] GameObject player;
    [SerializeField] Camera kamera;
    [SerializeField] GameObject glitch;
    [SerializeField] MouseLook ml;

    Vector3 targetPos;
    bool rotated;

    Vector3 playerForwardPos;

    void OnEnable()
    {
        if (FindObjectOfType<MouseLook>() != null)
        {
            ml = FindObjectOfType<MouseLook>();
            ml.enabled = false;
        }

        if (player == null && GameObject.FindGameObjectWithTag(Constants.PlayerTag) != null)
            player = GameObject.FindGameObjectWithTag(Constants.PlayerTag);

        if (kamera == null && Camera.main != null)
            kamera = Camera.main;

        if (glitch == null && GameObject.FindGameObjectWithTag("Glitch") != null)
            glitch = GameObject.FindGameObjectWithTag("Glitch");
    }

    void Update()
    {
        playerForwardPos = player.transform.forward * 150;
        playerForwardPos.y += 15;

        if (ml.enabled == true)
            ml.enabled = false;

        Vector3 lookPos = glitch.transform.position - player.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, Time.deltaTime * lookSpeed);

        kamera.transform.parent = null;

        lookPos = playerForwardPos;
        kamera.transform.LookAt(lookPos);

        kamera.transform.parent = player.transform;
    }

    void OnDisable()
    {
        if (ml != null)
            ml.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(playerForwardPos, 1);
    }
}
