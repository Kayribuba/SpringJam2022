using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthScript : MonoBehaviour
{
    public Slider playerHealthBar;
    public float MaxHealthValue = 100f;
    public float TouchDamage = 10f;
    public TextMeshProUGUI Healthbartextmesh;


    float currentHealthValue;
    GMscripto gameMangerScriptye;

    void Start()
    {
        playerHealthBar.maxValue = MaxHealthValue;
        playerHealthBar.value = MaxHealthValue;
        currentHealthValue = MaxHealthValue;
        Healthbartextmesh.text = "Health : " + currentHealthValue;
        gameMangerScriptye = FindObjectOfType<GMscripto>();
    }
    void Update()
    {
    }

    public void GetDamage(float damage)
    {
        currentHealthValue -= damage;
        playerHealthBar.value = currentHealthValue;

        if(currentHealthValue > 0)
        Healthbartextmesh.text = "Health : " + currentHealthValue;

        if (currentHealthValue <= 0)
        {
            Healthbartextmesh.text = "Health : 0";
            Die();
        }
    }
    void Die()
    {
        gameMangerScriptye.LoadSceneSecondsAfter(SceneManager.GetActiveScene().buildIndex, 2f);
        gameObject.GetComponent<PlayerMovementScript>().enabled = false;

        if (Camera.main.GetComponent<MouseLook>() != null)
            Camera.main.GetComponent<MouseLook>().isDead = true;

        enabled = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.EnemyTag))
        {
            GetDamage(TouchDamage);
        }
    }
}
