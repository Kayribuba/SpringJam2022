using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HocaScript : MonoBehaviour
{
    [SerializeField] Transform Player;

    public GameObject Barrel;
    public GameObject ProjectilePrefab;

    public Slider Healthbar;
    public float MaxHealthValue = 100f;
    float currentHealthValue;

    Vector3 attackDirection;

    float currentTime;
    public float shootCoolDown = 0.5f;
    float shootAgainAt;

    void Start()
    {
        Healthbar.maxValue = MaxHealthValue;
        Healthbar.value = MaxHealthValue;
        currentHealthValue = MaxHealthValue;
    }
    void Update()
    {
        TrackPlayer();
        Shoot();
    }

    public void DamageHoca(float damage)
    {
        currentHealthValue -= damage;
        Healthbar.value = currentHealthValue;

        if(currentHealthValue <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void TrackPlayer()
    {
        transform.LookAt(Player.position, Vector3.up);

        attackDirection = Player.position - Barrel.transform.position;
    }
    void Shoot()
    {
        if (shootAgainAt <= Time.time)
        {
            shootAgainAt = Time.time + shootCoolDown;

            GameObject bulletSpawned = Instantiate(ProjectilePrefab, Barrel.transform.position, Quaternion.identity);
            bulletSpawned.transform.forward = attackDirection;
            bulletSpawned.GetComponent<Rigidbody>().AddForce(bulletSpawned.transform.forward * 50f, ForceMode.Impulse);
            Destroy(bulletSpawned, 5);
        }
    }
}
