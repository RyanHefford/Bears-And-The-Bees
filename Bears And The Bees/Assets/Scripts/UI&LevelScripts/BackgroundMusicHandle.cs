using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicHandle : MonoBehaviour
{
    public AudioSource stealthMusic;
    public AudioSource chaseMusic;

    private float lastTimeChased;
    private float searchDelay = 5f;
    private bool isFadingOut;
    private bool isFadingIn;
    void Update()
    {
        if (!chaseMusic.isPlaying && !stealthMusic.isPlaying)
        {
            stealthMusic.Play();
            StartCoroutine(FadeIn(stealthMusic, 2f));
        }

        if (chaseMusic.isPlaying && lastTimeChased + searchDelay < Time.time && !isFadingOut)
        {
            StartCoroutine(FadeOut(chaseMusic, 4f));
        }
    }

    public void PlayChaseMusic()
    {
        if (!chaseMusic.isPlaying)
        {
            chaseMusic.Play();
            stealthMusic.Pause();
        }
        lastTimeChased = Time.time;
    }



    private IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        isFadingOut = true;
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        isFadingOut = false;
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        isFadingIn = true;
        float startVolume = audioSource.volume;
        audioSource.volume = 0;

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        isFadingIn = false;
    }
}
