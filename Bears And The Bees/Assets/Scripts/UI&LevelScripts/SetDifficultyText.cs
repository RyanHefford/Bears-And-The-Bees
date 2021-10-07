using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetDifficultyText : MonoBehaviour
{
    public TMP_Text diffText;

    private void Start()
    {
        if (PlayerPrefs.GetString("DifficultyText").Equals(""))
        {
            PlayerPrefs.SetInt("Difficulty", 4);
            PlayerPrefs.SetString("DifficultyText", "Medium");
            PlayerPrefs.SetInt("EnemyAttack", 2);
            PlayerPrefs.SetFloat("SecTillChase", 1.5f);
            PlayerPrefs.SetFloat("AttackCD", 1.5f);
            PlayerPrefs.SetFloat("AttackRange", 3.0f);
            PlayerPrefs.SetFloat("EnemySpeed", 5.0f);
        }
    }

    private void FixedUpdate()
    {
        diffText.text = PlayerPrefs.GetString("DifficultyText");
    }
}
