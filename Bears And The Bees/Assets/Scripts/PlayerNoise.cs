using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    private NoiseBarUI bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.FindGameObjectWithTag("PlayerHUD").GetComponentInChildren<NoiseBarUI>();
    }

    public void CalculateNoise(float currSpeed)
    {
        float noise = currSpeed / 15.0f;
        bar.UpdateNoiseLevel(noise);
    }
}
