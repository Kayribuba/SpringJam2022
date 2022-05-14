using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject BarrelR;
    [SerializeField] GameObject BarrelL;
    GameObject LastBarrel;

    Vector3 targetPoint;
    Vector3 attackDirection;

    void Start()
    {
        LastBarrel = BarrelR;
    }
    void Update()
    {
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        attackDirection = targetPoint - LastBarrel.transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletSpawned = Instantiate(BulletPrefab, LastBarrel.transform.position, Quaternion.identity);
            bulletSpawned.transform.forward = attackDirection;

            if (LastBarrel == BarrelL)
            {
                LastBarrel = BarrelR;
            }
            else
            {
                LastBarrel = BarrelL;
            }

            bulletSpawned.GetComponent<Rigidbody>().AddForce(bulletSpawned.transform.forward * 50f, ForceMode.Impulse);
            Destroy(bulletSpawned, 5);
        }
    }
}
