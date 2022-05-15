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
    public TextMeshProUGUI Gradetextmesh;


    float currentHealthValue;
    GMscripto gameMangerScriptye;

    void Start()
    {
        playerHealthBar.maxValue = MaxHealthValue;
        playerHealthBar.value = MaxHealthValue;
        currentHealthValue = MaxHealthValue;
        Healthbartextmesh.text = "Health : " + currentHealthValue;
        Gradetextmesh.text = "GRADE A";
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
        {
            Healthbartextmesh.text = "Health : " + currentHealthValue;
            Gradetextmesh.text = "GRADE " + GetGrade(currentHealthValue);
        }

        if (currentHealthValue <= 0)
        {
            Healthbartextmesh.text = "Health : 0";
            Gradetextmesh.text = "GRADE " + GetGrade(currentHealthValue);
            Die();
        }
    }
    string GetGrade(float points)
    {
        string grade = "A";

        if(points == 50)
        {
            grade = "A";
        }
        if(points >= 40)
        {
            grade = "B";
        }
        else if (points >= 30)
        {
            grade = "C";
        }
        else if (points >= 20)
        {
            grade = "D";
        }
        else if (points >= 10)
        {
            grade = "E";
        }
        else
        {
            grade = "F";
        }

        return grade;
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
