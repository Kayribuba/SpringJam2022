using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    GMscripto gameManagerScriptye;

    void Start()
    {
        Healthbar.maxValue = MaxHealthValue;
        Healthbar.value = MaxHealthValue;
        currentHealthValue = MaxHealthValue;
        gameManagerScriptye = FindObjectOfType<GMscripto>();
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
        gameManagerScriptye.LoadSceneSecondsAfter(SceneManager.GetActiveScene().buildIndex + 1, 3f);
        Destroy(gameObject);
    }
    void TrackPlayer()
    {
        transform.LookAt(Player.position, Vector3.up);
        transform.Rotate(new Vector3(-90 , 0 , 0));

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
