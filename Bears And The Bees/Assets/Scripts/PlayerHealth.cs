using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    private int currentHealth;
    private HealthBarUI healthBar;
    public int maxHealth = 10;

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
            SceneManager.LoadScene("NoFinishScene");
        }

        healthBar.UpdateHealth(currentHealth, maxHealth);
    }
}
