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
            PlayerPrefs.SetInt("Difficulty", 2);
            PlayerPrefs.SetString("DifficultyText", "Easy");
            PlayerPrefs.SetInt("EnemyAttack", 1);
            PlayerPrefs.SetFloat("SecTillChase", 1.5f);
            PlayerPrefs.SetFloat("AttackRange", 2.5f);
            PlayerPrefs.SetFloat("EnemySpeed", 4.0f);
        }
    }

    private void FixedUpdate()
    {
        diffText.text = PlayerPrefs.GetString("DifficultyText");
    }
}
