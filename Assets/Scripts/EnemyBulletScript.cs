using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Constants.PlayerTag))
        {
            FindObjectOfType<PlayerHealthScript>().GetDamage(damage);
        }
        Destroy(gameObject);
    }
}
