using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepHandle : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Sound/MetalFootsteps");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStepSound()
    {
        audioSource.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Length)]);
    }
}
