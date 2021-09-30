using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fill;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int newHealth, int maxHealth)
    {
        fill.fillAmount = (float)newHealth / (float)maxHealth;
        text.text = newHealth.ToString() + '/' + maxHealth.ToString();
    }
}
