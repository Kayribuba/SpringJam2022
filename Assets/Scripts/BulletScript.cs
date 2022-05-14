using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject effect;
    public float effectDuration = .5f;
    public float bulletDamage = 2f;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            if(collision.gameObject.GetComponent<HocaScript>() != null)
            {
                collision.gameObject.GetComponent<HocaScript>().DamageHoca(bulletDamage);
            }

            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
