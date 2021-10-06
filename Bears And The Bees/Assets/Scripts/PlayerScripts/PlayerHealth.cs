using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    private int currentHealth;
    private HealthBarUI healthBar;
    public int maxHealth = 4;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("PlayerHUD").GetComponentInChildren<HealthBarUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(int damage)
    { 
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("NoFinishScene");
        }

        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void ChangeMaxHealth(int change)
    {
        maxHealth += change;

        if (change > 0)
        {
            currentHealth += change;
        }else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.UpdateHealth(currentHealth, maxHealth);
    }
}
