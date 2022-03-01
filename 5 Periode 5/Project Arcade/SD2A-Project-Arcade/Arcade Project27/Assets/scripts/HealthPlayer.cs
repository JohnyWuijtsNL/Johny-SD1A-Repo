using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField]
    AudioScript ac;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage()
    {
        TakeDamage(5);
        //Debug.Log("Hit");
        ac.playSound(1);
        if (currentHealth <= 0)
        {
            //Debug.Log("Player Dood");
            SceneManager.LoadScene("Deathscreen");

        }
    }


void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
