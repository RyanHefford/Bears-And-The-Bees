using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunEnemyVision : MonoBehaviour
{

    private GameObject player;
    private GameObject[] enemies;
    private Light visionLight;
    public float flareDelay = 3f;
    public float flareDuration = 3f;
    public float alertTime = 2f;
    public float maxAlertDistance = 40f;
    private float lastFlareTime = 0f;
    private float warningStartTime = 0;
    private float lastSeenPlayer = -10f;
    private bool isFlaring = false;
    private bool isWarning = false;
    private bool playerVisible = false;

    public LayerMask targetMask;
    public LayerMask obtructionMask;
    public Material sunMaterial;

    //sound
    public AudioSource soundEffectSource;
    private AudioClip[] flareSoundEffects;
    private BackgroundMusicHandle backGroundMusic;
    public AudioSource alarmSound;

    // Start is called before the first frame update
    void Start()
    {
        visionLight = GetComponent<Light>();

        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        soundEffectSource = GetComponent<AudioSource>();
        flareSoundEffects = Resources.LoadAll<AudioClip>("Sound/SunFlare");
        backGroundMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusicHandle>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lastFlareTime + flareDelay < Time.time && !isFlaring)
        {
            StartCoroutine(FlareCoroutine());
        }
        else if (lastFlareTime + flareDelay - 1f < Time.time && !isWarning && !isFlaring)
        {
            StartCoroutine(FlareWarning());
        }

        if (isFlaring || lastSeenPlayer + alertTime > Time.time)
        {
            //Check if player is visible
            if (CheckCanSeePlayer() && !player.GetComponent<PlayerMovement>().currentStats.isInvisible)
            {
                AlertAllEnemies();
                SlowStatus slowStatus = ScriptableObject.CreateInstance<SlowStatus>();
                slowStatus.Init(2f, 0.35f);
                player.GetComponent<StatusEffectHandler>().AddStatus(slowStatus);
                lastFlareTime = Time.time;
                lastSeenPlayer = Time.time;
                playerVisible = true;

                backGroundMusic.PlayChaseMusic();
            }
            else
            {
                lastFlareTime = Time.time;
            }
        }
        else if(!isWarning && !isFlaring)
        {
            playerVisible = false;
            visionLight.enabled = false;
            sunMaterial.SetColor("_EmissionColor", Color.black);
        }

        if (isWarning)
        {
            float percentComplete = Time.time - warningStartTime;
            visionLight.intensity = 100 * (percentComplete / 2.0f);
            Color currColor = Color.yellow * percentComplete;
            sunMaterial.SetColor("_EmissionColor", currColor);
        }


        if (lastSeenPlayer + alertTime < Time.time)
        {
            alarmSound.Stop();
        }
        else if(!alarmSound.isPlaying)
        {
            alarmSound.Play();
        }
    }

    private void AlertAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            // check if enemy is within alert distance
            if (Vector3.Distance(transform.position, enemy.transform.position) <= maxAlertDistance)
            {
                if (enemy.GetComponentInChildren<EnemyVision>() != null)
                {
                    enemy.GetComponentInChildren<EnemyVision>().PlayerFound(player.transform.position);
                }
            }
        }
    }

    private bool CheckCanSeePlayer()
    {
        Vector3 reletiveNormalizedPos = (player.transform.position - this.transform.position).normalized;

        float angleToPlayer = Vector3.Angle(reletiveNormalizedPos, transform.forward);

        RaycastHit hit;

        //check if hit player
        if (!player.GetComponent<PlayerMovement>().currentStats.isInvisible && Physics.Raycast(transform.position, reletiveNormalizedPos, out hit, visionLight.range, targetMask) && Mathf.Abs(angleToPlayer) < visionLight.spotAngle / 2f)
        {
            Vector3 currPlayerPoint = hit.point;
            //check if wall is in the way
            if (Physics.Raycast(transform.position, reletiveNormalizedPos, out hit, visionLight.range, obtructionMask))
            {
                //check if wall is closer
                if (Vector3.Distance(transform.position, currPlayerPoint) < Vector3.Distance(transform.position, hit.point))
                {
                    visionLight.color = new Color(1f, 0f, 0);
                    sunMaterial.SetColor("_EmissionColor", new Color(1f, 0f, 0));
                    return true;
                }
            }
            else
            {
                visionLight.color = new Color(1f, 0f, 0);
                sunMaterial.SetColor("_EmissionColor", new Color(1f, 0f, 0));
                return true;
            }
        }
        visionLight.color = Color.yellow;
        sunMaterial.SetColor("_EmissionColor", Color.yellow);
        return false;

    }

    private IEnumerator FlareCoroutine()
    {
        visionLight.color = new Color(0.9f, 0.4f, 0.25f);
        sunMaterial.SetColor("_EmissionColor", new Color(0.9f, 0.4f, 0.25f));
        visionLight.intensity = 100;
        yield return new WaitForSeconds(0.2f);
        isFlaring = true;
        visionLight.color = Color.yellow;
        sunMaterial.SetColor("_EmissionColor", Color.yellow);
        yield return new WaitForSeconds(flareDuration);

        isFlaring = false;
        if (!playerVisible)
        {
            visionLight.enabled = false;
            lastFlareTime = Time.time;
            sunMaterial.SetColor("_EmissionColor", Color.black);
        }

    }

    private IEnumerator FlareWarning()
    {
        soundEffectSource.PlayOneShot(flareSoundEffects[UnityEngine.Random.Range(0, 2)]);

        visionLight.color = Color.yellow;
        isWarning = true;
        visionLight.enabled = true;
        warningStartTime = Time.time;
        yield return new WaitForSeconds(2f);
        isWarning = false;
    }
}
