using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    private NoiseBarUI bar;
    private PlayerMovement movement;
    private float noise = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.FindGameObjectWithTag("PlayerHUD").GetComponentInChildren<NoiseBarUI>();
        movement = GetComponent<PlayerMovement>();
    }

    public void CalculateNoise(float currSpeed)
    {
        noise = (currSpeed / 15.0f) * movement.currentStats.noiseMultiplier;
        bar.UpdateNoiseLevel(noise);
    }

    public float GetNoise()
    {
        return noise;
    }

}
