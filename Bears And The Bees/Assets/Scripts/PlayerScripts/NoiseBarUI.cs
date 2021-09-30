using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoiseBarUI : MonoBehaviour
{
    public Image fill;

    void Start()
    {
        fill.fillAmount = 0;
    }

    public void UpdateNoiseLevel(float currNoiseLevel)
    {
        fill.fillAmount = currNoiseLevel;
    }
}


