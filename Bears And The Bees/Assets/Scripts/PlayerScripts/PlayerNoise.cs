using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    private NoiseBarUI bar;
    private PlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.FindGameObjectWithTag("PlayerHUD").GetComponentInChildren<NoiseBarUI>();
        movement = GetComponent<PlayerMovement>();
    }

    public void CalculateNoise(float currSpeed)
    {
        float noise = (currSpeed / 15.0f) * movement.currentStats.noiseMultiplier;
        bar.UpdateNoiseLevel(noise);
    }
}
