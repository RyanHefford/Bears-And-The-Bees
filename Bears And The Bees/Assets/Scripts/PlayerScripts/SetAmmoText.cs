using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetAmmoText : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("CurrAmmoAmount") == 0)
        {
            PlayerPrefs.SetInt("CurrAmmoAmount", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "x" + PlayerPrefs.GetInt("CurrAmmoAmount").ToString();
    }
}
