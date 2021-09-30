using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetDifficultyText : MonoBehaviour
{
    public TMP_Text diffText;

    private void Start()
    {
        if (PlayerPrefs.GetString("DifficultyText") == default)
        {
            diffText.text = "Easy";
        }
    }

    private void FixedUpdate()
    {
        diffText.text = PlayerPrefs.GetString("DifficultyText");
    }
}
