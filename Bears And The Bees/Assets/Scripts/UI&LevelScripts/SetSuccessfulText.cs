using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetSuccessfulText : MonoBehaviour
{
    public TextMeshProUGUI successfulText;

    // Start is called before the first frame update
    void Start()
    {
        successfulText.text = PlayerPrefs.GetInt("SuccessfulRuns").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
